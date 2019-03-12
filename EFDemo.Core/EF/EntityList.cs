using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Core.EF
{
    public class EntityList<TEntity> where TEntity : class, new()
    {
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<TEntity> List { get; set; }
    }
}
