using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Model
{
    public class News
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        [Column(TypeName = "image")]
        [DisplayName("Превью")]
        public byte[] Picture { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Комменты")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
