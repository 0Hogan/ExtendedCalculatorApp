using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator.Models
{
    public class PreviousCalculation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Calculation { get; set; }

        public PreviousCalculation()
        {
            ID = 0;
            Calculation = "";
        }
        public PreviousCalculation(int iD, string calculation)
        {
            ID = iD;
            Calculation = calculation;
        }
    }

}
