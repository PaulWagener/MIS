using ModuleManager.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    /// <summary>
    /// Bevat informatie over de filters zodat de betreffende view weet welke filter mogelijkheden een bepaald overzicht heeft
    /// <remarks>Als een property null is zal dit geen filter optie zijn voor het betreffende overzicht.</remarks>
    /// </summary>
    public class FilterOptionsViewModel
    {
        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Competentie'-objecten alleen de namen en zet deze in het 'CompetentieFilter' Property
        /// </summary>
        /// <param name="competentieList">Lijst van alle 'Competentie'-objecten</param>
        public void AddCompetenties(IEnumerable<Competentie> competentieList)
        {
            CompetentieFilter = competentieList
            .Select(comp => comp.Naam)
            .ToList();
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Tag'-objecten alleen de namen en zet deze in het 'TagFilter' Property
        /// </summary>
        /// <param name="tagList">Lijst van alle 'Tag'-objecten</param>
        public void AddTags(IEnumerable<Tag> tagList)
        {
            TagFilter = tagList
            .Select(tag => tag.Naam)
            .ToList();
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Leerlijn'-objecten alleen de namen en zet deze in het 'TagFilter' Property
        /// </summary>
        /// <param name="leerlijnenList">Lijst van alle 'Leerlijn'-objecten</param>
        public void AddLeerlijnen(IEnumerable<Leerlijn> leerlijnenList)
        {
            LeerlijnFilter = leerlijnenList
            .Select(comp => comp.Naam)
            .ToList();
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Blok'-objecten alleen de BlokNummer en zet deze in het 'Blokken' Property
        /// </summary>
        /// <param name="blokList">Lijst van alle 'Blok'-objecten</param>
        public void AddBlokken(IEnumerable<Blok> blokList)
        {
            Blokken = blokList
                .Select(blok => blok.BlokId)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Fase'-objecten alleen de namen en zet deze in het 'FaseNamen' Property
        /// </summary>
        /// <param name="faseList">Lijst van alle 'Fase'-objecten</param>
        public void AddFases(IEnumerable<Fase> faseList)
        {
            FaseNamen = faseList.ToList();
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Schooljaar'-objecten alleen de jaren en zet deze in het 'Leerjaren' Property
        /// </summary>
        /// <param name="schooljaarList">Lijst van alle 'Schooljaar'-objecten</param>
        public void AddLeerjaren(IEnumerable<Schooljaar> schooljaarList)
        {
            Leerjaren = schooljaarList
                .Select(jaar => jaar.JaarId)
                .ToList();
        }

        /// <summary>
        /// vult de EC-property met waarden
        /// </summary>
        public void AddECs()
        {
            ECs = new List<double>
            {
                0.5,
                1,
                1.5,
                2,
                2.5,
                3,
                3.5,
                4,
                4.5,
                5
            };
        }

        /// <summary>
        /// Verkrijgt uit een lijst van alle 'Status'-objecten alleen de status en zet deze in het 'Statussen' Property
        /// </summary>
        /// <param name="statusList">Lijst van alle 'Status'-objecten</param>
        public void AddStatuses(IEnumerable<Status> statusList)
        {
            Statussen = statusList
            .Select(status => status.Status1)
            .ToList();
        }

        /// <summary>
        /// Mogelijke competentie(s) om op te filteren
        /// </summary>
        public IEnumerable<string> CompetentieFilter { get; set; }
        /// <summary>
        /// Mogelijke tag(s) om op te filteren
        /// </summary>
        public IEnumerable<string> TagFilter { get; set; }
        /// <summary>
        /// Mogelijke leerlijn(en) om op te filteren
        /// </summary>
        public IEnumerable<string> LeerlijnFilter { get; set; }
        /// <summary>
        /// Mogelijke blok(ken) om op te filteren
        /// </summary>
        public IEnumerable<int> Blokken { get; set; }
        /// <summary>
        /// Mogelijke fasenaam(namen) om op te filteren
        /// </summary>
        public List<Fase> FaseNamen { get; set; }
        /// <summary>
        /// Mogelijke Leerjaar om op te filteren
        /// </summary>
        public IEnumerable<int> Leerjaren { get; set; }
        /// <summary>
        /// Mogelijke EC(s) om op te filteren
        /// </summary>
        public IEnumerable<double> ECs { get; set; }
        /// <summary>
        /// Mogelijke status om op te filteren
        /// </summary>
        public IEnumerable<string> Statussen { get; set; }
    }
}