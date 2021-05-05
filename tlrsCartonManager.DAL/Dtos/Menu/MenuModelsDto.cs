using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Menu
{
    public class MenuModelsDto
    {
        public int ModelID { get; set; }

        public string ModelName { get; set; }

        public ICollection<MenuModelOptionsDto> ModelOptions { get; set; }

    }
}
