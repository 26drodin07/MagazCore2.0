using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата заказа")]
        public DateTime Date { get; set; }
        [DisplayName("Товар")]
        public  Product Product { get; set; }
        [Required]
        [DisplayName("Магазин")]
        public  Shop Shop { get; set; }
        [DisplayName("Пользователь")]
        public User Account { get; set; }

    }
}
