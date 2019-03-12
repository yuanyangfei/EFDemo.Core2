using EFDemo.Core.Entity;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EFDemo.Core.EF;
using System.Linq.Expressions;

namespace EFDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Core.EF.MyDbContext())
            {


                //添加测试数据
                //for (int i = 0; i < 1000; i++)
                //{
                //    Public_Area area1 = new Public_Area()
                //    {
                //        ID = Guid.NewGuid(),
                //        Name = "上海"+i,
                //        ParentID = Guid.NewGuid()

                //    };
                //    db.Public_Area.Add(area1);
                //    Public_Area area2 = new Public_Area()
                //    {
                //        ID = Guid.NewGuid(),
                //        Name = "市区" + i,
                //        ParentID = area1.ID

                //    };
                //    db.Public_Area.Add(area2);
                //    Public_Area area3 = new Public_Area()
                //    {
                //        ID = Guid.NewGuid(),
                //        Name = "嘉定区" + i,
                //        ParentID = area2.ID

                //    };
                //    db.Public_Area.Add(area3);

                //    T_Classes classes = new T_Classes()
                //    {
                //        Name = "高中二班" + i,
                //        Money = 2000
                //    };
                //    db.T_Classes.Add(classes);
                //    T_Student student = new T_Student()
                //    {
                //        ClassesID = classes.ID,
                //        Name = "李四" + i,
                //        Phone = "18236265820",
                //        Sex = true,
                //        ProvinceID = area1.ID,
                //        CityID = area2.ID,
                //        CountyID = area3.ID,
                //    };
                //    db.T_Student.Add(student);
                //    db.SaveChanges();

                //}

                //T_Classes classes = new T_Classes()
                //{
                //    Name = "高中t班",
                //    Money = 2000
                //};
                //db.T_Classes.Add(classes);
                //db.SaveChanges();

            }

            var profiler = MiniProfiler.Current;
            using (profiler.Step("查询数据"))
            {
                using (var db = new Core.EF.MyDbContext())
                {
                    ////等值连接Lambda写法
                    //var result1 = db.T_Classes.Where(t => t.Money == 2000).Join(db.T_Student, c => c.ID, s => s.ClassesID, (c, s) => new
                    //{
                    //    CName = c.Name,
                    //    SName = s.Name
                    //}).ToList();
                    ////等值连接Linq写法
                    //var result2 = (from c in db.T_Classes
                    //               join s in db.T_Student
                    //               on c.ID equals s.ClassesID
                    //               where c.Money == 2000
                    //               select new
                    //               {
                    //                   CName = c.Name,
                    //                   SName = s.Name
                    //               }).ToList();

                    ////左外连接的写法
                    //var result3 = (from c in db.T_Classes.Where(a=>a.Money==2000)
                    //             join s in db.T_Student on c.ID equals s.ClassesID into temp //临时表
                    //             from t in temp.DefaultIfEmpty()
                    //             select new
                    //             {
                    //                 CName = c.Name,
                    //                 SName = t.Name
                    //             }).ToList();

                    ////单条件排序
                    //var r1= Repository.GetPagedEntitys<T_Classes, int>(db, 1, 20, c => c.Deleted == false, false, c => c.ID);
                    ////多条件排序
                    //var r2 = Repository.GetPagedEntitys<T_Classes, int,bool>(db, 1, 20, c => c.Deleted == false, false, c => c.ID, true, c =>c.Deleted);
                    ////sql查询转强类型
                    //var r3 = Repository.GetPagedEntitysBySqlWhere<T_Classes>(db, 1, 20, "and Deleted=0", "ID DESC");
                    ////纯sql操作
                    //var r4 = Repository.GetPagedTable(db, 1, 20, "select * from T_Classes where Deleted=0", "ID DESC");

                    //模拟一系列条件
                    string name = "高中t班";
                    int age = 0;
                    int PageSize = 20;
                    int pageIndex = 1;
                    //原始的写法如下：
                    var query = db.T_Student.Where(c => c.Deleted == false);
                    if (!string.IsNullOrEmpty(name))
                    {
                        query = query.Where(c => c.Name.Contains(name));
                    }
                    if (age != 0)
                    {
                        query = query.Where(c => c.Age == age);
                    }
                    var list = query.OrderByDescending(c => c.ID).Skip(PageSize * (pageIndex - 1)).Take(PageSize).ToList();

                    //利用扩展的Linq的写法
                    Expression<Func<T_Student, bool>> exp = c => true;
                    if (!string.IsNullOrEmpty(name))
                    {
                        exp = exp.And(c => c.Name.Contains(name));
                    }
                    if (age != 0)
                    {
                        exp = exp.And(c => c.Age == age);
                    }
                    var r1= Repository.GetPagedEntitys<T_Student, int>(db, 1, 20,exp, false, c => c.ID);


                }
            }
            return View();
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}