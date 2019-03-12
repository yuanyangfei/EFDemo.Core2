using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Core.EF
{
    public class TableList
    {

        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public DataTable DataTable { get; set; }
    }
}
