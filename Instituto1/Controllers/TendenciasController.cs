using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class TendenciasController : Controller
    {
        private readonly DBHelper _db;
        public TendenciasController(DBHelper db) => _db = db;

        public IActionResult Index()
        {
            var vm = new TendenciasViewModel();

            
            var dt = _db.EjecutarSP("sp_tendencias_matricula");
            foreach (System.Data.DataRow r in dt.Rows)
                vm.Resultados.Add(new TendenciaRow
                {
                    Periodo         = r["periodo"].ToString()!,
                    FechaInicio     = Convert.ToDateTime(r["fecha_inicio"]),
                    FechaFin        = Convert.ToDateTime(r["fecha_fin"]),
                    TotalMatriculas = Convert.ToInt32(r["total_matriculas"]),
                    Finalizados     = Convert.ToInt32(r["finalizados"]),
                    Retirados       = Convert.ToInt32(r["retirados"]),
                    EnCurso         = Convert.ToInt32(r["en_curso"])
                });

            return View(vm);
        }
    }
}
