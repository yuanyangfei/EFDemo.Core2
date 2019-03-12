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
    /// 学生
    /// </summary>
    public class T_Student : BaseEntity<int>
    {

        /// <summary>
        /// 外键
        /// </summary>
        [ForeignKey("ClassesID")]
        public virtual T_Classes T_Classes { get; set; }
        [Display(Name = "班级ID")]
        public int ClassesID { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(8, ErrorMessage = "{0}最大长度{1}")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Sex { get; set; }

        [Display(Name = "年龄")]
        public int Age { get; set; }

        [Display(Name = "电话")]
        [Required(ErrorMessage = "{0}是必填项")]
        [RegularExpression(@"^(13[0-9]|15[0-9]|18[0-9])\d{8}$", ErrorMessage = "不是手机号格式")]
        [MaxLength(11, ErrorMessage = "{0}最大长度{1}")]
        public string Phone { get; set; }


        /// <summary>
        /// 省市县外键
        /// </summary>

        [ForeignKey("ProvinceID")]
        public virtual Public_Area Public_Area_Province { get; set; }
        [Display(Name = "省ID")]
        public Guid ProvinceID { get; set; }

        [ForeignKey("CityID")]
        public virtual Public_Area Public_Area_City { get; set; }
        [Display(Name = "市ID")]
        public Guid CityID { get; set; }

        [ForeignKey("CountyID")]
        public virtual Public_Area Public_Area_County { get; set; }
        [Display(Name = "县ID")]
        public Guid CountyID { get; set; }
    }
}
