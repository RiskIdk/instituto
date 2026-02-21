using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class RendimientoController : Controller
    {
        private readonly DBHelper _db;
        public RendimientoController(DBHelper db) => _db = db;

        public IActionResult Index()
        {
            var vm = new RendimientoViewModel();

            
            var dt = _db.EjecutarSP("sp_rendimiento_academico");
            foreach (System.Data.DataRow r in dt.Rows)
                vm.Resultados.Add(new RendimientoRow
                {
                    Curso        = r["curso"].ToString()!,
                    Periodo      = r["periodo"].ToString()!,
                    Evaluados    = Convert.ToInt32(r["evaluados"]),
                    PromedioNota = Convert.ToDecimal(r["promedio_nota"]),
                    Aprobados    = Convert.ToInt32(r["aprobados"]),
                    Reprobados   = Convert.ToInt32(r["reprobados"])
                });

            return View(vm);
        }
    }
}
