using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix
{
    public class StoreModel
    {
        public string title { get; set; }
        public List<Product> products { get; set; }

    }

    public class Product
    {
        public string text { get; set; }
        public string image { get; set; }
    }
}
