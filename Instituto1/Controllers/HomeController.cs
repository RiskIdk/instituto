using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBHelper _db;
        public HomeController(DBHelper db) => _db = db;

        public IActionResult Index()
        {
            var vm = new DashboardViewModel();

            
            var dtStats = _db.EjecutarSP("sp_dashboard_estadisticas");
            if (dtStats.Rows.Count > 0)
            {
                var r = dtStats.Rows[0];
                vm.TotalEstudiantes  = Convert.ToInt32(r["total_estudiantes"]);
                vm.TotalCursos       = Convert.ToInt32(r["total_cursos"]);
                vm.TotalInstructores = Convert.ToInt32(r["total_instructores"]);
                vm.TotalMatriculas   = Convert.ToInt32(r["total_matriculas"]);
            }

            
            var dtP = _db.EjecutarSP("sp_participacion_por_curso");
            foreach (System.Data.DataRow r in dtP.Rows)
                vm.Participacion.Add(new ParticipacionRow
                {
                    NombreCurso     = r["nombre_curso"].ToString()!,
                    Categoria       = r["categoria"].ToString()!,
                    TotalMatriculas = Convert.ToInt32(r["total_matriculas"]),
                    Finalizados     = Convert.ToInt32(r["finalizados"]),
                    Retirados       = Convert.ToInt32(r["retirados"]),
                    EnCurso         = Convert.ToInt32(r["en_curso"])
                });

            
            var dtO = _db.EjecutarSP("sp_ocupacion_cursos");
            foreach (System.Data.DataRow r in dtO.Rows)
                vm.Ocupacion.Add(new OcupacionRow
                {
                    Curso               = r["curso"].ToString()!,
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
