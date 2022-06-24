using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Logica
{
    public sealed class LogicaNegocio
    {
        // SINGLETON

        private static LogicaNegocio instance = null;
        private LogicaNegocio()
        {

        }
        public static LogicaNegocio Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogicaNegocio();
                }
                return instance;
            }
        }
        //
        public void CargarAnimal(string nombre, string especie)
        { 

        }
        //public List<Animal> GetListaAnimales(int id)
        //{
           
        //}
    }
}
