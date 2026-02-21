using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class InstructoresController : Controller
    {
        private readonly DBHelper _db;
        public InstructoresController(DBHelper db) => _db = db;

        public IActionResult Index(int idInstructor = 0)
        {
            var vm = new InstructoresViewModel { IdInstructorSel = idInstructor };

            
            var dt = _db.EjecutarSP("sp_carga_instructores");
            foreach (System.Data.DataRow r in dt.Rows)
                vm.Instructores.Add(new InstructorRow
                {
                    IdInstructor     = Convert.ToInt32(r["id_instructor"]),
                    Instructor       = r["instructor"].ToString()!,
                    Especialidad     = r["especialidad"].ToString()!,
                    CursosAsignados  = Convert.ToInt32(r["cursos_asignados"]),
                    TotalEstudiantes = Convert.ToInt32(r["total_estudiantes"])
                });

            
            if (idInstructor > 0)
            {
                var dtD = _db.EjecutarSP("sp_detalle_instructor",
                    new { id_instructor = idInstructor });

                foreach (System.Data.DataRow r in dtD.Rows)
                    vm.Detalle.Add(new DetalleInstructor
                    {
                        Curso     = r["curso"].ToString()!,
                        Periodo   = r["periodo"].ToString()!,
                        Modalidad = r["modalidad"].ToString()!,
                        Inscritos = Convert.ToInt32(r["inscritos"]),
                        Cupo      = Convert.ToInt32(r["cupo"])
                    });
            }

            return View(vm);
        }
    }
}
