using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Core.EF
{
    public static class Repository
    {
        public static EntityList<TEntity> GetPagedEntitys<TEntity, Sort>(DbContext db, int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambds, bool isAsc, Expression<Func<TEntity, Sort>> orderByLambds) where TEntity : class, new()
        {
            var temp = db.Set<TEntity>().Where<TEntity>(whereLambds);
            var rows = temp.Count();
            var totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;
            temp = isAsc ? temp.OrderBy<TEntity, Sort>(orderByLambds) : temp.OrderByDescending<TEntity, Sort>(orderByLambds);
            temp = temp.Skip<TEntity>(pageSize * (pageIndex - 1)).Take<TEntity>(pageSize);

            var list = temp.ToList<TEntity>();
            var dataList = Activator.CreateInstance(typeof(EntityList<TEntity>)) as EntityList<TEntity>;
            dataList.List = list;
            dataList.TotalRows = rows;
            dataList.TotalPages = totalPage;
            return dataList;
        }

        public static EntityList<TEntity> GetPagedEntitys<TEntity, Sort1, Sort2>(DbContext db, int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambds, bool isAsc1, Expression<Func<TEntity, Sort1>> orderByLambd1, bool isAsc2, Expression<Func<TEntity, Sort2>> orderByLambd2) where TEntity : class, new()
        {
            var temp = db.Set<TEntity>().Where<TEntity>(whereLambds);
            var rows = temp.Count();
            var totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;

            IOrderedQueryable<TEntity> temp1 = isAsc1 ? temp.OrderBy<TEntity, Sort1>(orderByLambd1) : temp.OrderByDescending<TEntity, Sort1>(orderByLambd1);
            temp1 = isAsc2 ? temp1.ThenBy<TEntity, Sort2>(orderByLambd2) : temp1.ThenByDescending<TEntity, Sort2>(orderByLambd2);

            var temp2 = temp1.AsQueryable<TEntity>().Skip<TEntity>(pageSize * (pageIndex - 1)).Take<TEntity>(pageSize);

            var list = temp2.ToList<TEntity>();
            var dataList = Activator.CreateInstance(typeof(EntityList<TEntity>)) as EntityList<TEntity>;
            dataList.List = list;
            dataList.TotalRows = rows;
            dataList.TotalPages = totalPage;
            return dataList;
        }

        //拼接sqlWhere返回单表分页Entity数据,paramss格式为 p={0}
        public static EntityList<TEntity> GetPagedEntitysBySqlWhere<TEntity>(DbContext db,  int pageIndex, int pageSize, string where, string orderKey, params object[] paramss) where TEntity : class, new()
        {

            string sqls = "";
            string tableName = typeof(TEntity).Name;//获取表名
            string sql = string.Format("select *, row_number() over (order by {0} ) as row_number from {1}", string.IsNullOrEmpty(orderKey) ? "Id" : orderKey, tableName);
            string where1 = !string.IsNullOrEmpty(where) ? " where 1=1 " + where : "";
            int tag = (pageIndex - 1) * pageSize;
            sqls = string.Format(@"select top ({0}) * from 
                         ( 
                           {1}
	                       {2}
                          )  as t
                         where t.row_number > {3}", pageSize, sql, where1, tag);

            //获取数据         
            var list = db.Database.SqlQuery<TEntity>(sqls, paramss).ToList<TEntity>();

            //通过自定义的class R 取得总页码数和记录数
            string sqlCount = string.Format("select count(1) as Rows from {0} {1}", tableName, where1);
            var rows = (int)db.Database.SqlQuery<int>(sqlCount, paramss).ToList()[0];
            var totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;

            var dataList = Activator.CreateInstance(typeof(EntityList<TEntity>)) as EntityList<TEntity>;
            dataList.List = list;
            dataList.TotalRows = rows;
            dataList.TotalPages = totalPage;
            return dataList;
        }
        //ADO.net方式返回连表查询Table数据
        public static TableList GetPagedTable(DbContext db, int pageIndex, int pageSize, string sql, string orderKey, params SqlParameter[] paramss)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"select * from
                        (
                        select *, row_number() over (order by {0} ) as row from
                        (
                        ", orderKey));
            sb.Append(sql);
            sb.Append(@"
                        )as t
                        )
                        as s
                        where s.row  between " + (pageIndex * pageSize - pageSize + 1) + " and " + (pageIndex * pageSize));

            sb.Append(";select count(1)from(" + sql + ") as t;");          
            var con = db.Database.Connection as SqlConnection;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(paramss);
                DbDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                var rows = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                var totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;
                cmd.Parameters.Clear();
                var tableList = Activator.CreateInstance(typeof(TableList)) as TableList;
                tableList.DataTable = ds.Tables[0];
                tableList.TotalRows = rows;
                tableList.TotalPages = totalPage;
                return tableList;
            }
        }

    }
}
