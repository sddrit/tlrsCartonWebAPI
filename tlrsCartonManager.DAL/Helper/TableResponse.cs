using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Helper
{
    public class TableResponse<T>
    {
        public TableResponse() { }

        public TableResponse(IEnumerable<T> erroList)
        {
            ErroList = erroList;
        }
        public string Message { get; set; }
        public IEnumerable<T> ErroList { get; set; }


     

    }
}
