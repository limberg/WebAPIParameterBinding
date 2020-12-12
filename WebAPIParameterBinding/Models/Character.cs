using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIParameterBinding.Models
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}