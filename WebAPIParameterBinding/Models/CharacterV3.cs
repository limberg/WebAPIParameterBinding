using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIParameterBinding.Models
{
    public class CharacterV3:Character
    {
        public static bool TryParse(string value, out CharacterV3 c)
        {
            c = new CharacterV3();

            if (!value.Contains("_"))
                return false;

            string[] values = value.Split('_');

            if (values.Length <= 1)
                return false;


            c.ID = Convert.ToInt32(values[0]);
            c.Name = values[1];

            return true;
        }
    }
}