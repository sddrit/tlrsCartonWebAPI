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
        public ImportResultDto() { }
        public ImportResultDto(int _NoOfImportedRecords, int _NoOfFailedRecords, int _NoOfTotalRecords, List<ImportErrorModelItemDto> _FailedList)
        {
            NoOfImportedRecords = _NoOfImportedRecords;
            NoOfFailedRecords = _NoOfFailedRecords;
            NoOfTotalRecords = _NoOfTotalRecords;
            FailedList = _FailedList;
        }
        public int NoOfImportedRecords { get; set; }
        public int NoOfFailedRecords { get; set; }
        public int NoOfTotalRecords { get; set; }
        public List<ImportErrorModelItemDto> FailedList { get; set; }


    }

}
