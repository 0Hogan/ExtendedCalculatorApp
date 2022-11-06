using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Calculator
{
    public class MathExcercise
    {
        [JsonPropertyName("operand1")]
        public double operand1 { get; set; }


        [JsonPropertyName("operand2")]
        public double operand2 { get; set; }

        [JsonPropertyName("operation")]
        public char operation { get; set; }

        [JsonPropertyName("result")]
        public double result { get; set; }

        [JsonPropertyName("fakeResult1")]
        public double fakeResult1 { get; set; }

        [JsonPropertyName("fakeResult2")]
        public double fakeResult2 { get; set; }
    }
}
