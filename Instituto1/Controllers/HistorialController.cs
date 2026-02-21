using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class HistorialController : Controller
    {
        private readonly DBHelper _db;
        public HistorialController(DBHelper db) => _db = db;

        public IActionResult Index(string cedula = "", int idEstudiante = 0)
        {
            var vm = new HistorialViewModel
            {
                BusquedaCedula = cedula,
                IdEstudiante   = idEstudiante
            };

            // SP6 - lista de estudiantes para el dropdown
            var dtEst = _db.EjecutarSP("sp_listar_estudiantes");
            foreach (System.Data.DataRow r in dtEst.Rows)
                vm.Estudiantes.Add(new EstudianteItem
                {
                    IdEstudiante = Convert.ToInt32(r["id_estudiante"]),
                    Nombre       = r["nombre"].ToString()!,
                    Cedula       = r["cedula"].ToString()!
                });

            // SP5 - historial del estudiante (doble result set)
            bool buscar = !string.IsNullOrWhiteSpace(cedula) || idEstudiante > 0;
            if (buscar)
            {
                var ds = _db.EjecutarSPMultiple("sp_historial_estudiante",
                    new { cedula, id_estudiante = idEstudiante });

                // Tables[0] = datos personales
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var info = ds.Tables[0].Rows[0];
                    vm.NombreEst = info["estudiante"].ToString()!;
                    vm.CedulaEst = info["cedula"].ToString()!;
                    vm.CorreoEst = info["correo"].ToString()!;
                }

                // Tables[1] = historial de cursos
                if (ds.Tables.Count > 1)
                {
                    foreach (System.Data.DataRow r in ds.Tables[1].Rows)
                        vm.Resultados.Add(new HistorialRow
                        {
                            Curso     = r["curso"].ToString()!,
                            Periodo   = r["periodo"].ToString()!,
                            Modalidad = r["modalidad"].ToString()!,
                            Estado    = r["estado"].ToString()!,
                            NotaFinal = r["nota_final"] == DBNull.Value
                                        ? null
                                        : Convert.ToDecimal(r["nota_final"]),
                            Resultado = r["resultado"].ToString()!
                        });
                }
            }

            return View(vm);
        }
    }
}
