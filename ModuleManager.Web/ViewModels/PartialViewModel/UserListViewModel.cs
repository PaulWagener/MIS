using System.Collections.Generic;
using System.Linq;
using ModuleManager.UserDAL;
using AutoMapper;
using ModuleManager.Domain;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class UserListViewModel
    {

        public ICollection<UserViewModel> Data { get; set; }

        public int RecordsFiltered { get { return Data.Count; } }

        public int RecordsTotal { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recordsTotal">Totaal aantal users in de datasource"</param>
        public UserListViewModel(int recordsTotal)
        {
            RecordsTotal = recordsTotal;
        }

        public void AddUsers(IEnumerable<User> userList)
        {
            Data = userList
                .Select(Mapper.Map<User, UserViewModel>)
                .ToList();

        }
    }
}