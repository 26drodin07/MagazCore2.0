using CodeFirst.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace MagazCore2._0.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        [DisplayName("Название")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Цена")]
        [Required]
        public double Price { get; set; }

       
        public IFormFile Picture { get; set; }
        public string PictureId { get; set; }

        [DisplayName("Категория")]
        public Category Category { get; set; }
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
