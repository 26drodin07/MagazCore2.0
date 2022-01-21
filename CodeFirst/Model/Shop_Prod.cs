using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Shop_Prod
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Магазин")]
        public  Shop Shop { get; set; }
        public int ShopId { get; set; }

        [DisplayName("Товар")]
        public  Product Product { get; set; }
        public int ProductId { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }
    }
}
