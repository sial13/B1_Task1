using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_Task1.Model
{
    public class TextFromFile
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string? LatinString { get; set; }
        public string? RussianString { get; set; }
        public int IntegerNum { get; set; }
        public float FloatNum { get; set; }
    }
}
