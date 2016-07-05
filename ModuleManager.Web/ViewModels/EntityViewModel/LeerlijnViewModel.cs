namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerlijnViewModel
    {
        public string Naam { get; set; }
        public string Schooljaar { get; set; }

        internal Domain.Leerlijn ToPoco()
        {
            return new Domain.Leerlijn()
            {
                Naam = this.Naam,
                Schooljaar = this.Schooljaar
            };
        }
    }
}