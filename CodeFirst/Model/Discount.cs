using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Model
{
    [DisplayName("Бонусная карта")]
    public class Discount
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Бонусы")]
        public Double Bonus { get; set; }

        [DisplayName("Номер Карты")]
        public string Card_num { get; set; }

        public  User Account { get; set; }

        [DisplayName("Дата оформления")]
        public DateTime Date { get; set; }

    }
}
