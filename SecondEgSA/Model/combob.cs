using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondEgSA.Model
{
    public class combob
    {
        public int id { get; set; }
        public string name { get; set; }

        public combob(int Id, string Name)
        {
            id = Id;
            name = Name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
