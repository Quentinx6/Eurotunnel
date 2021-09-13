using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;
using API.Data;
using API.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class newController:ControllerBase
    {
        [HttpGet]
        public JArray TestApi()
        {
            string disp = "";
            JArray coulante = new JArray();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                string[] sArray = Directory.GetFiles("./Json/");
                foreach (var files in sArray)
                {
                    
                    using (StreamReader sr = new StreamReader(files))
                    {
                        // Read and display lines from the file until the end of
                        // the file is reached.
                    
                        var Json = sr.ReadToEnd();
                        string array = "["+ Json + "]";
                        dynamic deserialize = JsonConvert.DeserializeObject(array);
                        foreach (var json in deserialize)
                        {
                            disp = "[{" + "\n" +
                                   "'PERNR' : '"+ json.PERNR.ToString() + "',"+ "\n" +
                                   " 'ObjectSID' : '" + json.ObjectSID.ToString() + "',"+ "\n" +
                                   " 'Current_Step' : [" +"\n" +
                                   "\t'"+ json.Current_Step.Name.ToString() + "'\n" +" ]"+"\n" +
                                   "}]";
                            coulante.Add(JArray.Parse(disp));
                        }
                    }
                }

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("Le fichier ne peut pas être lu.");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(coulante);
            return coulante;
        }

        [HttpPost]
        public async void PostApi()
        {
            var recup = TestApi();
            string array = "[" + recup + "]";
            dynamic deserialize = JsonConvert.DeserializeObject(array);
            foreach (var json in deserialize)
            {
                for (int i = 0; i < recup.Count; i++)
                {  
                    
                    // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
                    // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
                    
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:5000/api/v1/ITAccount"))
                        {
                            request.Headers.TryAddWithoutValidation("accept", "*/*"); 

                            request.Content = new StringContent("{\"matricule\":\"" + json[i][0].PERNR.ToString() +
                                                                "\",\"itAccountID\":\"" + json[i][0].ObjectSID.ToString() +
                                                                "\",\"currentStep\":\"" +
                                                                json[i][0].Current_Step[0].ToString() + "\"}");
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json"); 

                            var response = await httpClient.SendAsync(request);
                        }
                    }
                }
            }
        }

    }
}
