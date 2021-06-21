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


    public class ExcelParseDestructioDateUpdateViewModel
    {
        public int CartonNo { get; set; }
        public string DestructionTimeFrame { get; set; }
        public string DestructionDate { get; set; }
    }
    public class DestructionDateUpdateDto
    {
        public List<ExcelParseDestructioDateUpdateViewModel> DestructionDateUpdateList { get; set; }
    }

}
