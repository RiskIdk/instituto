using InstitutoAcademico.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoAcademico.Controllers
{
    public class ParticipacionController : Controller
    {
        private readonly DBHelper _db;
        public ParticipacionController(DBHelper db) => _db = db;

        public IActionResult Index(string categoria = "", string estado = "")
        {
            var vm = new ParticipacionViewModel { Categoria = categoria, Estado = estado };

            
            var dtCat = _db.EjecutarSP("sp_listar_categorias");
            foreach (System.Data.DataRow r in dtCat.Rows)
                vm.Categorias.Add(r["categoria"].ToString()!);

            
            var dt = _db.EjecutarSP("sp_participacion_por_curso",
                new { categoria, estado });

            foreach (System.Data.DataRow r in dt.Rows)
                vm.Resultados.Add(new ParticipacionRow
                {
                    NombreCurso     = r["nombre_curso"].ToString()!,
                    Categoria       = r["categoria"].ToString()!,
                    TotalMatriculas = Convert.ToInt32(r["total_matriculas"]),
                    Finalizados     = Convert.ToInt32(r["finalizados"]),
                    Retirados       = Convert.ToInt32(r["retirados"]),
                    EnCurso         = Convert.ToInt32(r["en_curso"])
                });

            return View(vm);
        }
    }
}
