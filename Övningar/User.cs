using System;
using System.ComponentModel.DataAnnotations;

namespace Övningar
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool CrimeCommitted { get; set; }
        public int? SportID { get; set; } 
        public DateTime? CrimeDate { get; set; } = DateTime.Now;
        public Sport Sport { get; set; }
    }
}
