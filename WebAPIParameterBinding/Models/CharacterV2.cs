using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPIParameterBinding.Models
{
    [TypeConverter(typeof(CharacterV2Converter))]
    public class CharacterV2
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static bool TryParse(string value, out CharacterV2 c)
        {
            try
            {
                c = new CharacterV2();

                if (!value.Contains("_"))
                    return false;

                string[] values = value.Split('_');

                if (values.Length <= 1)
                    return false;


                c.ID = Convert.ToInt32(values[0]);
                c.Name = values[1];

                return true;

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}