using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;
using WebAPIParameterBinding.Models;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web.Http.ModelBinding;

namespace WebAPIParameterBinding.Controllers
{
    [RoutePrefix("api/parameterbinding")]
    public class ParameterBindingController : ApiController
    {
        #region Query String
        //QueryString
        //Uri: api/parameterbinding/a/character?id=1
        [HttpGet]
        [Route("a/character")]
        public string CharacterA(int id)
        {
            return id.ToString();
        }

        //QueryString Multiple parameters
        //URI: api/parameterbinding/Multi/Character?id=1&name=abc
        //URI: api/parameterbinding/Multi/Character?name=abc&id=2
        [HttpGet]
        [Route("multi/character")]
        public string CharacterMulti(int id, string name)
        {
            return $"Id: {id.ToString()}, name: {name}";
        }

        //QueryString multiple paramaters for HTTP POST
        //URI: api/parameterbinding/Multi/Character?id=1&name=abc
        //URI: api/parameterbinding/Multi/Character?name=abc&id=2
        [HttpPost]
        [Route("multipost/character")]
        public string CharacterMultiPost(int id, string name)
        {
            return $"Id: {id.ToString()}, name: {name}";
        }
        #endregion

        #region Attribute Routing
        //Attribute Routing for HTTP GET/POST
        //URI: api/parameterbinding/d/character/{id}/{name}
        [AcceptVerbs("get", "post")]
        [Route("d/character/{id}/{name}")]
        public string CharacterD(int id, string name)
        {
            return $"Id: {id}, name: {name}";
        }

        //Know Object
        //URI: api/parameterbinding/e1/character
        //JSON: {"id":1, "name":"abc"}
        [HttpPost]
        [Route("jsonparameter/character")]
        public string CharacterJsonParameter(Character c)
        {
            return $"Id: {c.ID}, name: {c.Name}";
        }

        //Unknow Object (dynamic)
        //URI: api/parameterbinding/e1/character
        //JSON: {"id":1, "name":"abc", "xpto":"etc"}
        [HttpPost]
        [Route("dynamic/character")]
        public string CharacterDynamic(dynamic c)
        {
            //Character c1 = (Character)c;
            return $"Id: {c.ID}, name: {c.Name}, xpto:{c.xpto}";
        }

        //Mixing parameter Query
        //URI: api/parameterbinding/mixingbyquery/character?id=23
        //JSON: {"id":1, "name":"abc"}
        [HttpPost]
        [Route("mixingbyquery/character")]
        public string CharacterMixing(int id, Character c)
        {
            return $"Id: {id}, name: {c.Name}";
        }

        //Mixing parameter Attribute Routing  + Json
        //URI: api/parameterbinding/mixingbyroute/character?id=23
        //JSON: {"id":1, "name":"abc"}
        [HttpPost]
        [Route("mixingbyroute/character/{id}")]
        public string CharacterMixingRoute(int id, Character c)
        {
            return $"Id: {id}, name: {c.Name}";
        }
        #endregion

        #region From URI

        //From URI - Query String
        //URI: api/parameterbinding/fromuri/character?id=23&name=abc
        [HttpPost]
        [Route("fromuri/character")]
        public string CharacterFromUri([FromUri] Character c)
        {
            return $"Id: {c.ID}, name: {c.Name}";
        }

        //From Uri -Query Sring - dynamic
        //URI: api/parameterbinding/fromuridynamic/character?id=23&name=abc
        //Doesn't work because dynamic doesn't have properties
        //URI: api/parameterbinding/fromuridynamic/character?c=23 -> Does WORK! is like a primitive type
        [HttpPost]
        [Route("fromuridynamic/character")]
        public string CharacterFromUriDynamic([FromUri] dynamic c)
        {
            return $"{c}";
        }

        //From URI Attribute Routing 
        //URI: api/parameterbinding/fromurirouting/character/1/abc
        [HttpPost]
        [Route("fromurirouting/character/{id}/{name}")]
        //[Route("fromurirouting/character/{c.id}/{c.name}")] is valid too!
        public string CharacterFromUriRouting([FromUri] Character c)
        {
            return $"Id: {c.ID}, name: {c.Name}";
        }

        //From URI Multi Paramters - Attribute Routing V1
        //URI: api/parameterbinding/fromurimultirouting/character/1/abc --> does make to copy
        [HttpPost]
        [Route("fromurimultirouting/character/{c1.ID}/{c1.Name}/{c2.ID}/{c2.Name}")]
        public string CharacterFromUriRouting([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Id: {c1.ID}, name: {c2.Name}";
        }

        //From URI Multi Paramters - Attribute Routing V2
        //URI: api/parameterbinding/fromurimultirouting/character/1/cristal/2/ juancho
        [HttpPost]
        [Route("fromurimultirouting/character/{c1.ID}/{c1.Name}/{c2.ID}/{c2.Name}")]
        public string CharacterFromUriMultiRouting([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Id: {c1.ID}, name: {c2.Name}";
        }

        //From URI Multi Paramters - Query string
        //URI: api/parameterbinding/fromurimultiquery/character?c1.ID=1&c1.Name=Pedro&c2.ID=2&c2.Name=Rolo
        [HttpPost]
        [Route("fromurimultiquery/character")]
        public string CharacterFromUriQuery([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Id: {c1.ID}, name: {c2.Name}";
        }
        #endregion

        #region From Body

        //URI: api/parameterbinding/frombody/character
        [HttpPost]
        [Route("frombody/character")]
        public string CharacterFromBody([FromBody] int c)
        {
            return c.ToString();
        }

        //From Body 2 parameter
        //URI: api/parameterbinding/frombody2/character
        //ERROR: Only one parameter is allowed to have [FromBody]
        [HttpPost]
        [Route("frombody2/character")]
        public string CharacterFromBody2([FromBody] int c, [FromBody] int d)
        {
            return "ExceptionMessage: Can't bind multiple parameter to the request content";
        }

        //From Body - Raw JSON
        //URI: api/parameterbinding/frombodyrawjson/character
        //String: "{\"id\":1, \"name\":\"abc\"}"
        [HttpPost]
        [Route("frombodyrawjson/character")]
        public string CharacterFromBodyRawJson([FromBody] string s)
        {
            Character c = JsonConvert.DeserializeObject<Character>(s);
            return $"Character Id:{c.ID}, Name:{c.Name}";
        }
        #endregion

        #region Arrays and List - From URI

        //URI: api/parameterbinding/arrayfromuri/character?ids=1&ids=4&ids=6
        [HttpGet]
        [Route("arrayfromuri/character")]
        public string CharacterArrayFromUri([FromUri] int[] ids)
        {
            string result;
            result = string.Join(", ", ids);

            return result;
        }

        //List from Uri
        //URI: api/parameterbinding/listfromuri/character?ids=1&ids=4&ids=6
        [HttpGet]
        [Route("listfromuri/character")]
        public string CharacterListFromUri([FromUri] List<int> ids)
        {
            string result;
            result = string.Join(", ", ids);

            return result;
        }

        #endregion

        #region Array List From Body

        //URI: api/parameterbinding/listfrombody/character
        //hier is FromBody redundant, httpost get automatish
        [HttpPost]
        [Route("listfrombody/character")]
        public string CharacterListFromBody([FromBody] List<Character> c)
        {
            return string.Join(", ", c);
        }

        //URI: api/parameterbinding/listfrombody2/character
        [HttpPost]
        [Route("listfrombody2/character")]
        public string CharacterListFromBody2([FromBody] List<List<Character>> clist)
        {
            string result = "";
            //foreach(var c in clist)
            //     string.Join(", ", c);
            IEnumerable<object> ids = clist.Select(cl => cl.Select(c => c.ID)).ToList();

            foreach (var name in clist.Select(cl => cl.Select(c => c.Name)).ToList())
            {
                result += string.Join(", ", name)  + ", ";    
            }

            return result;
        }

        #endregion

        #region Form Data

        //URI: api/parameterbinding/formdata/character
        [HttpPost]
        [Route("formdata/character")]
        public string CharacterFormData()
        {
            var name = HttpContext.Current.Request.Form["name"];
            return $"Character name: {name}";

        }

        //URI: api/parameterbinding/formdata2/character
        [HttpPost]
        [Route("formdata2/character")]
        public string CharacterFormData2()
        {
            var form = HttpContext.Current.Request.Form;

            string res = "";
            foreach(string key in form.Keys)
            {
                res += $"{key} ({form[key]}) ";
            }

            return res;
        }


        #endregion

        #region URL Encoded

        //URI: api/parameterbinding/urlencoded/character
        //x-www-form-urlencoded
        [HttpPost]
        [Route("urlencoded/character")]
        public async Task<string> CharacterUrlEncoded(HttpRequestMessage request)
        {
            var form = await request.Content.ReadAsFormDataAsync();

            var response = "";

            foreach (string key in form.Keys)
                response += $"{key} ({form[key]}) ";

            return response;
        }


        #endregion

        #region XML

        //URI: api/parameterbinding/xml/character
        // <Character>
        //    <ID>123</ID>
        //    <Name>Sonya</Name>
        //</Character>
        [HttpPost]
        [Route("xml/character")]
        public string CharacterXML(HttpRequestMessage request)
        {
            var xmlStream = request.Content.ReadAsStreamAsync().Result;

            var doc = new XmlDocument();

            doc.Load(xmlStream);

            var xmlString = doc.DocumentElement.OuterXml;

            var serializer = new XmlSerializer(typeof(Character));

            Character c = new Character();
            using(TextReader reader = new StringReader(xmlString))
            {
                c = (Character)serializer.Deserialize(reader);
            }

            return $"Character id({c.ID})  name({c.Name})";
        }


        //WEb API allow us to do it in a simple way by activating XML Serialization
        //Global.asax
        //public class WebApiApplication : System.Web.HttpApplication
        //{
        //    protected void Application_Start()
        //    {
        //        var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
        //        xml.UseXmlSerializer = true;
        //    }
        //}

        //URI: api/parameterbinding/xmlglobalserialization/character
        // <Character>
        //    <ID>123</ID>
        //    <Name>Sonya</Name>
        //</Character>
        [HttpPost]
        [Route("xmlglobalserialization/character")]
        public string CharacterXMLGlobalSerialization(Character c) 
        {
            return $"Character id({c.ID})  name({c.Name})";
        }

        //URI: api/parameterbinding/xmlarrayglobalserialization/character
        // <Character>
        //    <ID>123</ID>
        //    <Name>Sonya</Name>
        //</Character>
        [HttpPost]
        [Route("xmlarrayglobalserialization/character")]
        public string CharacterXMLArrayGlobalSerialization(List<Character> clist)
        {
            string[] res = clist.Select(c => c.Name).ToArray();

            return string.Join(", ", res);
        }

        #endregion

        #region Type Converter primitive Type

        //URI: api/parameterbinding/typeconverter/character?c=123_abc
        //QueryString
        [HttpGet]
        [Route("typeconverter/character")]
        public string CharacterTypeConverter(CharacterV2 c)
        {
            return $"ID: {c.ID}, Name: {c.Name}";
        }

        //URI: api/parameterbinding/typeconverter/character/123_abc
        //Atribute Routing
        [HttpGet]
        [Route("typeconverter/character/{c}")]
        public string CharacterTypeConverterWithRoute(CharacterV2 c)
        {
            return $"ID: {c.ID}, Name: {c.Name}";
        }

        #endregion

        #region ModelBinder

        //URI: api/parameterbinding/modelbinder/character/123_abc
        [HttpGet]
        [Route("modelbinder/character/{c}")]
        public string CharacterModelBinder([ModelBinder(typeof(ChararacterV3ModelBinder))] CharacterV3 c)
        {
            return $"ID: {c.ID}, Name: {c.Name}";
        }

        //With ModelBinder in Configuration (MyConfiguration)
        //URI: api/parameterbinding/modelbinder/character/123_abc
        [HttpGet]
        [Route("modelbinderconfigregister/character/{c}")]
        public string CharacterModelBinderConfigRegister([ModelBinder] CharacterV3 c)
        {
            return $"ID: {c.ID}, Name: {c.Name}";
        }

        //With ModelBinder in Configuration (MyConfiguration) and Attribute to class type
        //URI: api/parameterbinding/modelbinder/character/123_abc
        [HttpGet]
        [Route("modelbinderconfigregisterattribute/character/{c}")]
        public string CharacterModelBinderConfigAttributeClass(CharacterV3_1 c)
        {
            return $"ID: {c.ID}, Name: {c.Name}";
        }

        #endregion
    }
}
