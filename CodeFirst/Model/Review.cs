using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Review
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Оценка")]
        public short Rate { get; set; }

        [DisplayName("Текст комментария")]
        public string Text { get; set; }

        public  User Account { get; set; }

        public  Product Product { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

    }
}
