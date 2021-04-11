using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuRightFormDto
    {
        public MenuRightFormDto()
        {

        }

        [Key]
        public int TrackingId { get; set; }
        public int MenuId { get; set; }
        public int FormRight { get; set; }
        public virtual MenuRightsDto Menu { get; set; }
        public virtual ICollection<MenuRightFormUserDto> MenuRightFormUsers { get; set; }
    }
}
