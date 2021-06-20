using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Import
{
    public class AlternativeNoUpdateDto
    {
        public List<ExcelParseAlternativeNoUpdateViewModel> AlternativeNoUpdateList { get; set; }
    }

    public class ExcelParseAlternativeNoUpdateViewModel
    {
        public int CartonNo { get; set; }
        public string AlternativeNo { get; set; }
    }
   
}
