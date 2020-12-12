using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using WebAPIParameterBinding.Models;

namespace WebAPIParameterBinding.Configuration
{
    public class MyConfiguration
    {
        public static void Register(HttpConfiguration config)
        {
            var provider = new SimpleModelBinderProvider(typeof(CharacterV3), new ChararacterV3ModelBinder());

            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
        }
    }
}