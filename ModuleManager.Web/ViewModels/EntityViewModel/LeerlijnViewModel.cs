namespace ModuleManager.Web.ViewModels.EntityViewModel
{
    public class LeerlijnViewModel
    {
        public string Naam { get; set; }

        internal Domain.Leerlijn ToPoco()
        {
            return new Domain.Leerlijn()
            {
                Naam = this.Naam
            };
        }
    }
}