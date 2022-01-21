using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Shop
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        public string Coord_N { get; set; }

        public string Coord_E { get; set; }

        public List<Shop_Prod> Shop_Prods { get; set; } = new List<Shop_Prod>();
    }
}
