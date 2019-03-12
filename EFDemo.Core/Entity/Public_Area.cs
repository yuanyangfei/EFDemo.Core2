using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Core.Entity
{
    /// <summary>
    /// 省市区
    /// </summary>
    public class Public_Area
    {        

        [Key]
        public Guid ID { get; set; }

        [Display(Name = "父亲ID")]
        public Guid ParentID { get; set; }

        [Display(Name = "名称")]
        [MaxLength(32, ErrorMessage = "{0}最大长度{1}")]
        public String Name { get; set; }
    }
}
