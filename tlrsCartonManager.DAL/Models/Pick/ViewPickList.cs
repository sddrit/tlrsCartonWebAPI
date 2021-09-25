using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewPickList
    {
        [Required]
        [Column("pickListNo")]
        [StringLength(50)]
        public string PickListNo { get; set; }
        [Column("lastSentDeviceId")]
        [StringLength(50)]
        public string LastSentDeviceId { get; set; }
        [Column("assignedUser")]
        [StringLength(100)]
        public string AssignedUser { get; set; }
        [Column("noOfCartons")]
        public int? NoOfCartons { get; set; }
        [Column("printed")]
        public bool Printed { get; set; }
        [Column("noOfCartonsPicked")]
        public int? NoOfCartonsPicked { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("createdDate")]
        [StringLength(4000)]
        public string CreatedDate { get; set; }
    }
}
