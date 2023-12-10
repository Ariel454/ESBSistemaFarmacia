using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFarmacia.Domain
{
    public class FarmaciaService
    {
        public void ProcesarRecetaEnFarmacia(RecetaMedica receta)
        {
            Console.WriteLine($"Procesando receta en Farmacia para el paciente: {receta.Paciente}");
        }
    }
}
