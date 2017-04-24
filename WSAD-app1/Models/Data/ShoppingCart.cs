using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.Data
{
    [Table("tblShoppingCart")]
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        [Key]
        public int ID { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [Column("Course_Id")]
        public int CourseId { get; set; }


        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        [ForeignKey("UserId")]
        public virtual User Username { get; set; }
    }
}