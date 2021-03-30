using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuRightFormUserAttachedDto
    {
        [Key]
        public int TrackingId { get; set; }
        public int UserMenuTrackingId { get; set; }
        public int UserFormTrackingId { get; set; }
        public int? FormRightId { get; set; }

    }
}
