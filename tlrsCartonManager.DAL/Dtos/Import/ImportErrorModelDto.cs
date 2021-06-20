using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Import
{
    public class ImportErrorModelItemDto
    {
        public string KeyValue { get; set; }
        public string Description { get; set; }

    }
    public class ImportResultDto
    {
        public int NoOfImportedRecords { get; set; }
        public int NoOfFailedRecords { get; set; }
        public List<ImportErrorModelItemDto> FailedList { get; set; }


    }
    
}
