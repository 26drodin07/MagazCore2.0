using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    /*Свойства конкретного товара (Имя/Значение)*/
    public class PropProd
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Значение")]
        public string Value { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public PropCat Parent { get; set; }
        public int ParentId { get; set; }
    }
}
