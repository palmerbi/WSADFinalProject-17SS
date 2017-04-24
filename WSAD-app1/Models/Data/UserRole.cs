using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.Data
{
    [Table("TblUser_Roles")]
    public class UserRole
    {
        [Key]
        [Column("User_Id", Order = 0)]
        public int UserId { get; set; }

        [Key, Column("Role_Id", Order = 1)]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}