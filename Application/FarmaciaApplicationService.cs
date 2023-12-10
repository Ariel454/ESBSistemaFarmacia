using SistemaFarmacia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFarmacia.Application
{
    public class FarmaciaApplicationService
    {
        private readonly FarmaciaService _farmaciaService;

        public FarmaciaApplicationService(FarmaciaService farmaciaService)
        {
            _farmaciaService = farmaciaService;
        }

        public void ProcesarRecetaEnFarmacia(RecetaMedica receta)
        {
            _farmaciaService.ProcesarRecetaEnFarmacia(new RecetaMedica
            {
                Paciente = receta.Paciente,
                Medicamento = receta.Medicamento,
                Dosis = receta.Dosis,
                FechaEmision = receta.FechaEmision
            });
        }
    }
}
