namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerlijnViewModel
    {
        public string Naam { get; set; }
        public string Schooljaar { get; set; }

        internal DomainDAL.Leerlijn ToPoco()
        {
            return new DomainDAL.Leerlijn()
            {
                Naam = this.Naam,
                Schooljaar = this.Schooljaar
            };
        }
    }
}