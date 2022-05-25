using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Translate;
using Amazon.Translate.Model;
using AWSTranslatePost.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSTranslatePost.Controllers
{
    public class AWSTranslateController : Controller
    {
        AWSCredentials credenciales;

        public AWSTranslateController()
        {
            /*CredentialProfileStoreChain perfil = new CredentialProfileStoreChain();
            if (!perfil.TryGetAWSCredentials("Mar", out credenciales)){
                throw new Exception("Credenciales no idsponibles.");
            }*/
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string texto, string lenguaje)
        {
            if (texto == null || lenguaje == null)
            {
                ViewBag.Mensaje = "Rellene todos los campos.";
            }
            else
            {
                using(IAmazonTranslate client = new AmazonTranslateClient())
                {
                    TranslateTextRequest request = new TranslateTextRequest()
                    {
                        SourceLanguageCode = "auto",
                        TargetLanguageCode = lenguaje,
                        Text = texto
                    };
                    TranslateTextResponse resultado = await client.TranslateTextAsync(request);
                    if (resultado.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.Mensaje = "Se ha producido un error en la traducción.";
                    }
                    else
                    {
                        ViewBag.Traduccion = resultado.TranslatedText;
                    }
                }
            }
            return View();
        }
    }
}
