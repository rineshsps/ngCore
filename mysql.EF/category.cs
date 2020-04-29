using System;
using System.ComponentModel.DataAnnotations;

namespace mysql.EF
{
    public class category
    {
        [Key]
        public int category_id { get; set; }
        public string name { get; set; }
        public DateTime last_update { get; set; }
    }

}