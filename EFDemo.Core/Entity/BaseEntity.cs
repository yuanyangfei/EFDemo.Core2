using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.Core.Entity
{
    public class BaseEntity
    {

    }
    [Serializable]
    public class BaseEntity<TKey> : BaseEntity
    {
        public BaseEntity()
        {
            this.AddTime = DateTime.Now;
        }

        [Key]
        [Display(Name = "编号")]
        public TKey ID { get; set; }
        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}是必填项"), Range(0, int.MaxValue, ErrorMessage = "{0}的范围是{1}到{2}")]
        [DefaultValue(0)]
        public int Sort { get; set; }
        [Display(Name = "备注")]
        [MaxLength(256, ErrorMessage = "{0}最大长度{1}")]
        public string Remark { get; set; }
        [Display(Name = "是否删除")]
        [Required]
        public bool Deleted { get; set; }
        public int AddUser { get; set; }
        [Display(Name = "添加时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd HH mm}", HtmlEncode = false, NullDisplayText = "数据无效")]
        public DateTime AddTime { get; set; }
        public int ModUser { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd  HH mm}", HtmlEncode = false, NullDisplayText = "数据无效")]
        public DateTime? ModTime { get; set; }

    }
  

}
