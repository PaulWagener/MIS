using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Data
{
    /// <summary>
    /// Any and All arguments to filter and sort anything.
    /// </summary>
    public class ModuleFilterSorterArguments
    {
        ICollection<string> _competentieFilters;
        ICollection<string> _competentieNiveauFilters;
        ICollection<string> _tagFilters;
        ICollection<string> _leerlijnFilters;
        ICollection<string> _faseFilters;
        ICollection<int> _blokFilters;
        string _statusFilter;
        string _zoektermFilter;
        int? _leerjaarFilter;


        public bool IsEmpty
        {
            get
            {
                return (SortBy == null) && (ZoektermFilter == null) && (CompetentieFilters == null) &&
                       (CompetentieNiveauFilters == null) && (TagFilters == null) && (LeerlijnFilters == null) &&
                       (BlokFilters == null) && (FaseFilters == null) && (LeerjaarFilter == null) && (ECfilters == null) && (StatusFilter == null);
            }
        }

        /// <summary>
        /// Geeft aan op welk veld gesorteerd meot worden.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Geeft aan of sorting oplopend of aflopen gebeurt. true = desc,  false = asc
        /// </summary>
        public bool SortDesc { get; set; }

        /// <summary>
        /// Geselecteerde/mogelijke competentie(s) om op te filteren
        /// </summary>
        public ICollection<string> CompetentieFilters 
        {
            get { return _competentieFilters; }
            set 
            { 
                if (value != null) 
                { 
                    _competentieFilters = value.Where(element => element.Length > 0).ToList(); 
                } 
            } 
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke competentieniveau(s) om op te filteren
        /// </summary>
        public ICollection<string> CompetentieNiveauFilters 
        {
            get { return _competentieNiveauFilters; }
            set
            {
                if (value != null)
                {
                    _competentieNiveauFilters = value.Where(element => element.Length > 0).ToList();
                }
            }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke tag(s) om op te filteren
        /// </summary>
        public ICollection<string> TagFilters
        { 
            get { return _tagFilters; }
            set 
            {
                if (value != null)
                { 
                    _tagFilters = value.Where(element => element.Length > 0).ToList(); 
                }
            }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke leerlijn(en) om op te filteren
        /// </summary>
        public ICollection<string> LeerlijnFilters 
        {
            get { return _leerlijnFilters; }
            set
            {
                if (value != null)
                {
                    _leerlijnFilters = value.Where(element => element.Length > 0).ToList();
                }
            }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke fasenaam(namen) om op te filteren
        /// </summary>
        public ICollection<string> FaseFilters 
        {
            get { return _faseFilters; }
            set
            {
                if (value != null)
                {
                    _faseFilters = value.Where(element => element.Length > 0).ToList();
                }
            }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke blok(ken) om op te filteren
        /// </summary>
        public ICollection<int> BlokFilters 
        {
            get { return _blokFilters; }
            set { _blokFilters = value; }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke EC(s) om op te filteren
        /// </summary>
        public ICollection<int> ECfilters { get; set; }
        
        /// <summary>
        /// Geselecteerde/mogelijke status om op te filteren
        /// </summary>
        public string StatusFilter 
        {
            get { return _statusFilter; }
            set 
            {
                if (value != null && value.Length > 0)
                {
                    _statusFilter = value;
                }
            }
        }
        
        /// <summary>
        /// Algemene zoekterm
        /// </summary>
        public string ZoektermFilter 
        {
            get { return _zoektermFilter; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _zoektermFilter = value;
                }
            }
        }
        
        /// <summary>
        /// Geselecteerde/mogelijke Leerjaar om op te filteren
        /// </summary>
        public int? LeerjaarFilter 
        {
            get { return _leerjaarFilter; }
            set
            {
                if (value != null && value > 0)
                {
                    _leerjaarFilter = value;
                }
            }
        }
    }
}
