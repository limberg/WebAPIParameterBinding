using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebAPIParameterBinding.Models
{
    public class CharacterV2Converter : TypeConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string)
            {
                CharacterV2 c;
                if (CharacterV2.TryParse((string)value, out c))
                    return c;
            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}