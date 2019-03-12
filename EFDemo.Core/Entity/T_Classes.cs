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
    /// 班级
    /// </summary>
   public class T_Classes : BaseEntity<int>
    {
        public T_Classes() {
            this.T_Student = new List<Entity.T_Student>();
        }


        [InverseProperty("T_Classes")]
        public virtual List<T_Student> T_Student { get; set; }


        [Display(Name = "班级名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(8, ErrorMessage = "{0}最大长度{1}")]
        public string Name { get; set; }

        [Display(Name = "人数")]
        public int Count { get; set; }

        [Display(Name = "班级经费")]
        public decimal Money { get; set; }
    }
}
