using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebAPIParameterBinding.Models
{
    public class ChararacterV3ModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            if (bindingContext.ModelType != typeof(CharacterV3) )
                return false;

            var objValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (objValue == null)
                return false;

            var value = objValue.RawValue as string;
            if (value == null)
                return false;

            CharacterV3 c;
            if (!CharacterV3.TryParse((string)value, out c))
                return false;

            bindingContext.Model = c;

            return true;
        }
    }
}