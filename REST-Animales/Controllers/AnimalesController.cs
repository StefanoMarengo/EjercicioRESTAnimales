using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Logica;

namespace REST_Animales.Controllers
{
    /*
     Construir un ABM simple de Animales.

    En la capa de negocio conocemos:
    ID; Nombre, Especie, Fecha Creación, Fecha modificación, Eliminado = true / false.

    Crear los métodos de administración (alta, baja y modificación).
    en el alta se debe crear un ID autonumérico.
    el nombre y la especie son obligatorios
    la fecha de creación se carga por sistema.
    la fecha de modificación se carga por sistema.
    la baja es lógica.

    Crear método de obtención de animales por especie, nombre (aproximacion).
    Crear método de obtención de registros por ID
*/
    public class AnimalServicio
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria")]
        public string Especie { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria"), Range(18, 60, ErrorMessage = "Fuera de rango de edad")]
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModif { get; set; }
        public bool Eliminado { get; set; }
    }
    public static class Conversores
    {
        public static AnimalServicio ConvertirAServicio(this Animal animal)
        {
            AnimalServicio animalServicio = new AnimalServicio()
            {
                ID = animal.ID,
                Nombre = animal.Nombre,
                Especie = animal.Especie,
                Edad = animal.Edad,
                FechaCreacion = animal.FechaCreacion,
                FechaModif=animal.FechaModif,
                Eliminado=animal.Eliminado

            };
            return animalServicio;
        }

        public static Animal ConvertirALogica(this AnimalServicio animalservicio)
        {
            Animal animal = new Animal()
            {
                ID = animalservicio.ID,
                Nombre = animalservicio.Nombre,
                Especie = animalservicio.Especie,
                Edad = animalservicio.Edad,
                FechaCreacion = animalservicio.FechaCreacion,
                FechaModif = animalservicio.FechaModif,
                Eliminado = animalservicio.Eliminado
            };
            return animal;
        }
    }
    [RoutePrefix("Animales")]
    public class AnimalesController : ApiController
    {
        private static List<AnimalServicio> ListaAnimales = new List<AnimalServicio>()
        {
            new AnimalServicio(){ID=0, Nombre="Morita", Especie="Caniche", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new AnimalServicio(){ID=1, Nombre="Poroto", Especie="Gato", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new AnimalServicio(){ID=2, Nombre="Moro", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=true},
            new AnimalServicio(){ID=3, Nombre="Oli", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new AnimalServicio(){ID=4, Nombre="Samantha", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false}
        };

        [Route("Lista")]
        public IHttpActionResult GetLista()
        {
            List<AnimalServicio> lista = ListaAnimales.Where(x=>x.Eliminado==false).ToList();
            if (lista == null)
                return BadRequest("La lista se encuentra vacía");
            return Ok(lista);
        }
        [Route("Obtener/{id}")]
        public IHttpActionResult Get(int id)
        {
            AnimalServicio animal = ListaAnimales.Where(x => x.Eliminado == false && x.ID==id).FirstOrDefault();
            if (animal == null)
                return BadRequest("El animal no existe o ha sido eliminado");
            return Ok(animal);
        }

        [Route("Obtener/Especie/{especie}")]
        public IHttpActionResult GetEspecie(string especie)
        {
            List<AnimalServicio> listaFiltrada = ListaAnimales.Where(x => x.Eliminado == false && x.Especie.Contains(especie)).ToList();
            if (listaFiltrada == null)
                return BadRequest("No existen animales de esa especie");
            return Ok(listaFiltrada);
        }

        [Route("Obtener/Nombre/{nombre}")]
        public IHttpActionResult GetNombre(string nombre)
        {
            List<AnimalServicio> listaFiltrada = ListaAnimales.Where(x => x.Eliminado == false && x.Nombre.Contains(nombre)).ToList();
            if (listaFiltrada == null)
                return BadRequest("No existen animales con ese nombre");
            return Ok(listaFiltrada);
        }
        [Route("Nuevo")]
        public IHttpActionResult Post([FromBody]AnimalServicio animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            animal.FechaCreacion = DateTime.Now;
            animal.ID = ListaAnimales.Count;
            animal.Eliminado = false;

            ListaAnimales.Add(animal);
            return Created("", animal);
        }
        [Route("Eliminar/{id}")]
        public IHttpActionResult Delete(int id)
        {
            AnimalServicio animal = ListaAnimales.Where(x => x.Eliminado == false && x.ID == id).FirstOrDefault();
            if (animal == null)
                return BadRequest("El animal no existe o ya sido eliminado previamente");
            animal.Eliminado = true;
            return Ok(animal);
        }

        [Route("Modificar/{id}")]
        public IHttpActionResult Put(int id, AnimalServicio animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AnimalServicio animalModificado = ListaAnimales.Where(x => x.ID == id && x.Eliminado==false).FirstOrDefault();
            if (animalModificado == null)
                return BadRequest("No existe un animal con esa ID o ya ha sido eliminado");
            animalModificado.FechaModif = DateTime.Now;
            animalModificado.Nombre = animal.Nombre;
            animalModificado.Especie=animal.Especie;

            return Ok(animalModificado);
        }
        //// GET: Animales/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Animales/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Animales/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Animales/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Animales/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Animales/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
