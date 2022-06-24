using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Lista
    {
        private static List<Animal> ListaAnimales = new List<Animal>()
        {
            new Animal(){ID=0, Nombre="Morita", Especie="Caniche", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new Animal(){ID=1, Nombre="Poroto", Especie="Gato", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new Animal(){ID=2, Nombre="Moro", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=true},
            new Animal(){ID=3, Nombre="Oli", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false},
            new Animal(){ID=4, Nombre="Samantha", Especie="Perro", FechaCreacion=DateTime.Parse("20/06/2022"), FechaModif=DateTime.Parse("23/06/2022"), Eliminado=false}
        };
    }
}
