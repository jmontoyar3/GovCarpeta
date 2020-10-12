using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace GovCarpeta.Controllers
{
    [ApiController]
    [Route("ciudadanos")]
    public class CiudadanoController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private const string GET = "GET";
        private const string POST = "POST";

        public CiudadanoController(IConfiguration config)
        {
            Configuration = config;
        }

        [HttpGet]
        [Route("validarCiudadano")]
        public ActionResult<string> ValidarCiudadano(int id)
        {
            string url = $"{Configuration["GovCarpeta:URL"]}apis/validateCitizen/{id}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = GET;
            request.ContentType = Configuration["GovCarpeta:ContentType"];
            request.Accept = Configuration["GovCarpeta:ContentType"];

            try
            {
                string responseBody = string.Empty;

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return string.Empty;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                        }
                    }
                }

                return responseBody;
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
    }
}
