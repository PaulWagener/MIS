namespace ModuleManager.Domain.Utility
{
    public class DomainConstants
    {
        public const string DbErrorStandard = "Kan actie niet voltooien: Probeer het later nog eens.";
        public const string DbErrorPkInvalid = "Kan actie niet voltooien: Een opgegeven veld is niet correct ingevuld.";
        public const string DbErrorFkConstraint = "Kan actie niet voltooien: Een veranderd veld mag niet veranderd worden.";
        public const string DbErrorPkDuplicate = "Kan actie niet voltooien: De ingevoerde Naam en/of Code bestaat al.";
        public const string DbErrorNoSelection = "Kan actie niet voltooien: Een opgegeven veld is niet correct ingevuld.";
        public const string DbErrorPkChanged = "Kan actie niet voltooien: Een van de velden mag niet aangepast worden.";

        public const string ExportErrorStandard = "Kan actie niet voltooien.";
        public const string ExportErrorAccessDenied = "Kan actie niet voltooien: Bestandssysteem weigerd toegang tot bestand of bestandslocatie.";
    }
}