using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    //Свойства характерные категории товаров
    public class PropCat
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        public  Category Category { get; set; }
        public int CategoryId { get; set; }

        public List<PropProd> Childrens { get; set; } = new List<PropProd>();
    }
}
