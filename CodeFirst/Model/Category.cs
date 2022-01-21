using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeFirst.Model
{
    public class Category
    {
        
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        
        public List<PropCat> Props { get; set; } = new List<PropCat>();
      
    }
}
