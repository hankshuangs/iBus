using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.iAAA
{
    internal class User
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動編號
        [DisplayName("學號")]
        public int ID { get; set; }

        [DisplayName("姓名")]
        [MaxLength(5)]
        [Column(TypeName = "nvarchar")]
        [Required] //不為空
        public string Name { get; set; }

        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        //浮點數
        [DisplayName("視力")]
        public decimal Eye { get; set; }
    }
}