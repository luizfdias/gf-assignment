using System;

namespace HolidayOptimizer.Api.Domain.Models
{
    public class Holiday 
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }        

        public Country Country { get; set; }
    }
}
