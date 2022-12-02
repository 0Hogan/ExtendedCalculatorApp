using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Calculator.Models
{
    [Table("Calculations")]
    public class DBCalculations
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Calculation { get; set; }

    }
}
