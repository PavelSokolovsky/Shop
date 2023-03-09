using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSokolovsky.Helpers
{
   public partial class PostNewProduct
    {
        public int Id { get; set; }

        public int IdProduct { get; set; }
        public int IdStock { get; set; }
        public int AmountCurent { get; set; }
        public int AmountMin { get; set; }
        public int AmountMax { get; set; }
    }
}
