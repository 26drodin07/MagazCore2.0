using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirst.Model
{
    public class User
    {
        [Key]
        public string Id=Guid.NewGuid().ToString();
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        public string username;
        public List<Discount> Discounts { get; set; } = new List<Discount>();
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

       
        public List<Review> Reviews { get; set; } = new List<Review>();
       
        public List<Order> Orders { get; set; } = new List<Order>();

        [DisplayName("Комменты")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Cart Cart { get; set; } = new Cart();

       

    }
}
