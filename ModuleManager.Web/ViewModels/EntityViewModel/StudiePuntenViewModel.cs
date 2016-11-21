namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class StudiePuntenViewModel
    {
        public string CursusCode { get; set; }
        public int Schooljaar { get; set; }
        public string ToetsCode { get; set; }
        public string Toetsvorm { get; set; }
        public decimal EC { get; set; }
        public string Minimum { get; set; }
    }
}