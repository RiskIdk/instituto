namespace InstitutoAcademico.Models
{
    
    public class DashboardViewModel
    {
        public int TotalEstudiantes  { get; set; }
        public int TotalCursos       { get; set; }
        public int TotalInstructores { get; set; }
        public int TotalMatriculas   { get; set; }
        public List<ParticipacionRow> Participacion { get; set; } = new();
        public List<OcupacionRow>     Ocupacion     { get; set; } = new();
    }

    
    public class ParticipacionViewModel
    {
        public string Categoria   { get; set; } = "";
        public string Estado      { get; set; } = "";
        public List<string>          Categorias { get; set; } = new();
        public List<ParticipacionRow> Resultados { get; set; } = new();
    }

    public class ParticipacionRow
    {
        public string NombreCurso     { get; set; } = "";
        public string Categoria       { get; set; } = "";
        public int    TotalMatriculas { get; set; }
        public int    Finalizados     { get; set; }
        public int    Retirados       { get; set; }
        public int    EnCurso         { get; set; }
    }

    
    public class InstructoresViewModel
    {
        public int    IdInstructorSel { get; set; }
        public List<InstructorRow>  Instructores { get; set; } = new();
        public List<DetalleInstructor> Detalle   { get; set; } = new();
    }

    public class InstructorRow
    {
        public int    IdInstructor    { get; set; }
        public string Instructor      { get; set; } = "";
        public string Especialidad    { get; set; } = "";
        public int    CursosAsignados { get; set; }
        public int    TotalEstudiantes{ get; set; }
    }

    public class DetalleInstructor
    {
        public string Curso     { get; set; } = "";
        public string Periodo   { get; set; } = "";
        public string Modalidad { get; set; } = "";
        public int    Inscritos { get; set; }
        public int    Cupo      { get; set; }
    }

    
    public class HistorialViewModel
    {
        public string BusquedaCedula  { get; set; } = "";
        public int    IdEstudiante    { get; set; }
        public string NombreEst       { get; set; } = "";
        public string CedulaEst       { get; set; } = "";
        public string CorreoEst       { get; set; } = "";
        public List<EstudianteItem> Estudiantes { get; set; } = new();
        public List<HistorialRow>   Resultados  { get; set; } = new();
    }

    public class EstudianteItem
    {
        public int    IdEstudiante { get; set; }
        public string Nombre       { get; set; } = "";
        public string Cedula       { get; set; } = "";
    }

    public class HistorialRow
    {
        public string  Curso      { get; set; } = "";
        public string  Periodo    { get; set; } = "";
        public string  Modalidad  { get; set; } = "";
        public string  Estado     { get; set; } = "";
        public decimal? NotaFinal { get; set; }
        public string  Resultado  { get; set; } = "";
    }

    
    public class TendenciasViewModel
    {
        public List<TendenciaRow> Resultados { get; set; } = new();
    }

    public class TendenciaRow
    {
        public string   Periodo         { get; set; } = "";
        public DateTime FechaInicio     { get; set; }
        public DateTime FechaFin        { get; set; }
        public int      TotalMatriculas { get; set; }
        public int      Finalizados     { get; set; }
        public int      Retirados       { get; set; }
        public int      EnCurso         { get; set; }
    }

    
    public class OcupacionViewModel
    {
        public string Periodo   { get; set; } = "";
        public string Modalidad { get; set; } = "";
        public List<string>      Periodos   { get; set; } = new();
        public List<OcupacionRow> Resultados { get; set; } = new();
    }

    public class OcupacionRow
    {
        public string  Curso               { get; set; } = "";
        public string  Instructor          { get; set; } = "";
        public string  Periodo             { get; set; } = "";
        public string  Modalidad           { get; set; } = "";
        public int     Cupo                { get; set; }
        public int     Inscritos           { get; set; }
        public int     Disponibles         { get; set; }
        public decimal PorcentajeOcupacion { get; set; }
    }

    
    public class RendimientoViewModel
    {
        public List<RendimientoRow> Resultados { get; set; } = new();
    }

    public class RendimientoRow
    {
        public string  Curso        { get; set; } = "";
        public string  Periodo      { get; set; } = "";
        public int     Evaluados    { get; set; }
        public decimal PromedioNota { get; set; }
        public int     Aprobados    { get; set; }
        public int     Reprobados   { get; set; }
    }
}
