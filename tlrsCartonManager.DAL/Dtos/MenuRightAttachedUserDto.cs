using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuRightAttachedUserDto
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDisplayString { get; set; }
        public string MenuRoute { get; set; }
        public int MenuLevel { get; set; }
        public int ParantId { get; set; }
        public int DivisionId { get; set; }
        public int? FinalMenu { get; set; }
        [Key]
        public int UserMenuId { get; set; }
        public int? UserRoleId { get; set; }
        public virtual ICollection<MenuRightFormUserAttachedDto> MenuRightFormUsers { get; set; }
    }
}
