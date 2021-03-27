using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuRightsDto
    {
        public MenuRightsDto()
        {
            
        }

        [Key]        
        public int MenuId { get; set; }
        [Required]
        [StringLength(50)]
        public string MenuName { get; set; }
        [Required]        
        [StringLength(50)]
        public string MenuDisplayString { get; set; }        
        [StringLength(100)]
        public string MenuRoute { get; set; }        
        public int MenuLevel { get; set; }
        public int ParantId { get; set; }
        public int DivisionId { get; set; }
        public virtual ICollection<MenuRightFormDto> MenuRightForms { get; set; }
        public virtual ICollection<MenuRightsUserDto> MenuRightUsers { get; set; }
    }
}
