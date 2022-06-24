using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace Logica
{
    public class LogicaNegocio
    {
        public void CargarAnimal(string nombre, string especie)
        {
            Animal animal = new Animal() {Nombre=nombre, Especie = especie };

            var url = "http://localhost:44373/Animales/Nuevo";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST"; //???
            
            //No sé cómo sigue
        }
        public List<Animal> GetListaAnimales(int id)
        {
            List<Animal> lista = new List<Animal>();
            var url = "http://localhost:44373/Animales/Obtener/"+id;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var respuesta = JsonConvert.DeserializeObject<dynamic>(responseFromServer);

                foreach (var item in respuesta)
                {
                    lista.Add(new Animal()
                    {
                        ID = item.ID,
                        Nombre = item.Nombre,
                        Especie = item.Especie
                    });
                }
            }
            response.Close();
            return lista;
        }
    }
}
