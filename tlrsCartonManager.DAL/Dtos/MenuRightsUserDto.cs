using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuRightsUserDto
    {
        public MenuRightsUserDto()
        {
            
        }

        [Key]        
        public int TrackingId { get; set; }        
        public int MenuId { get; set; }
        public int UserId { get; set; }        
        public virtual MenuRightsDto Menu { get; set; }
        public virtual UserDto User { get; set; }        
        public virtual ICollection<MenuRightFormUserDto> MenuRightFormUsers { get; set; }
    }
}
