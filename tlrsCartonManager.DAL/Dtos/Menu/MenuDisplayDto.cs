using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class MenuDisplayDto
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDisplayString { get; set; }
        public string MenuRoute { get; set; }
        public int MenuLevel { get; set; }
        public int ParantId { get; set; }
        public int DivisionId { get; set; }
        public virtual ICollection<MenuRightFormDto> MenuRightForms { get; set; }
    }
}
