using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirst.Model
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Цена")]
        [Required]
        public double Price { get; set; }
        
        [Column(TypeName = "image")]
        public string PictureId { get; set; }

        [DisplayName("Категория")]
        public  Category Category { get; set; }
        [JsonIgnore]
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public List<Cart> Carts { get; set; } = new List<Cart>();
        [JsonIgnore]
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<PropProd> Props { get; set; } = new List<PropProd>();
    }
}
