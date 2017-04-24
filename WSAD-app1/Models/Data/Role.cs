using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.Data
{
    [Table("TblRoles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public bool IsAdmin { get; set; }
    }
}