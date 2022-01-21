using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Текст комментария")]
        public string Text { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public  User Account { get; set; }
        public  News News { get; set; }
    }
}
