using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class OcupacionController : Controller
    {
        private readonly DBHelper _db;
        public OcupacionController(DBHelper db) => _db = db;

        public IActionResult Index(string periodo = "", string modalidad = "")
        {
            var vm = new OcupacionViewModel { Periodo = periodo, Modalidad = modalidad };

            
            var dtPer = _db.EjecutarSP("sp_listar_periodos");
            foreach (System.Data.DataRow r in dtPer.Rows)
                vm.Periodos.Add(r["nombre"].ToString()!);

            
            var dt = _db.EjecutarSP("sp_ocupacion_cursos",
                new { periodo, modalidad });

            foreach (System.Data.DataRow r in dt.Rows)
                vm.Resultados.Add(new OcupacionRow
                {
                    Curso               = r["curso"].ToString()!,
                    Instructor          = r["instructor"].ToString()!,
                    Periodo             = r["periodo"].ToString()!,
                    Modalidad           = r["modalidad"].ToString()!,
                    Cupo                = Convert.ToInt32(r["cupo"]),
                    Inscritos           = Convert.ToInt32(r["inscritos"]),
                    Disponibles         = Convert.ToInt32(r["disponibles"]),
                    PorcentajeOcupacion = Convert.ToDecimal(r["porcentaje_ocupacion"])
                });

            return View(vm);
        }
    }
}
