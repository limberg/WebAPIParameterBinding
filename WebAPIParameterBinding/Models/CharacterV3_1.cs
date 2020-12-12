using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebAPIParameterBinding.Models
{
    [ModelBinder(typeof(ChararacterV3_1_ModelBinder))]
    public class CharacterV3_1 
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static bool TryParse(string value, out CharacterV3_1 c)
        {
            c = new CharacterV3_1();

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

    public class ChararacterV3_1_ModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            if (bindingContext.ModelType != typeof(CharacterV3_1))
                return false;

            var objValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (objValue == null)
                return false;

            var value = objValue.RawValue as string;
            if (value == null)
                return false;

            CharacterV3_1 c;
            if (!CharacterV3_1.TryParse((string)value, out c))
                return false;

            bindingContext.Model = c;

            return true;
        }
    }
}