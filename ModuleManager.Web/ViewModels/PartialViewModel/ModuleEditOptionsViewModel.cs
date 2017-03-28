using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System.Web.Mvc;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Domain;
using AutoMapper;
using System.Data.Entity;
using ModuleManager.Web.ViewModels.EntityViewModel.Competenties;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class ModuleEditOptionsViewModel
    {
        public ICollection<TagViewModel> Tags { get; set; }

        public ICollection<LeerlijnViewModel> Leerlijnen { get; set; }
        public ICollection<WerkvormViewModel> Werkvormen { get; set; }
        public ICollection<ToetsvormViewModel> Toetsvormen { get; set; }
        public ICollection<ModuleVoorkennisViewModel> VoorkennisModules { get; set; }
        public ICollection<DocentViewModel> Docenten { get; set; }

        public ICollection<StatusViewModel> Statussen { get; set; }
        public ICollection<KwaliteitskenmerkViewModel> Kwaliteitskenmerken { get; set; }

        public void Filter(ModuleViewModel vm)
        {

        }

        public ModuleEditOptionsViewModel(){

        }

        public ModuleEditOptionsViewModel(IUnitOfWork _unitOfWork)
        {
            var tags = _unitOfWork.GetRepository<Tag>().GetAll();
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll();
            var werkvormen = _unitOfWork.GetRepository<Werkvorm>().GetAll();
            var toetsvormen = _unitOfWork.GetRepository<Toetsvorm>().GetAll();
            var modules = _unitOfWork.GetRepository<Module>().GetAll();
            var docenten = _unitOfWork.GetRepository<Docent>().GetAll();
            var status = _unitOfWork.GetRepository<Status>().GetAll();

            this.Leerlijnen = leerlijnen.Select(Mapper.Map<Leerlijn, LeerlijnViewModel>).ToList();
            this.Tags = tags.Select(Mapper.Map<Tag, TagViewModel>).ToList();
            this.Toetsvormen = toetsvormen.Select(Mapper.Map<Toetsvorm, ToetsvormViewModel>).ToList();
            this.VoorkennisModules = modules.Select(Mapper.Map<Module, ModuleVoorkennisViewModel>).ToList();
            this.Werkvormen = werkvormen.Select(Mapper.Map<Werkvorm, WerkvormViewModel>).ToList();
            this.Docenten = docenten.Select(Mapper.Map<Docent, DocentViewModel>).ToList();
            this.Statussen = status.Select(Mapper.Map<Status, StatusViewModel>).ToList();

            this.Kwaliteitskenmerken = _unitOfWork.Context.Kwaliteitskenmerken
                                            .Include(kk => kk.CompetentieOnderdeel)
                                            .Include(kk => kk.CompetentieOnderdeel.Competentie)
                                            .OrderBy(kk => kk.CompetentieOnderdeel.Competentie.Volgnummer)
                                            .ThenBy(kk => kk.CompetentieOnderdeel.Volgnummer)
                                            .ThenBy(kk => kk.Volgnummer)
                                            .ToList()
                                            .Select(kk => new KwaliteitskenmerkViewModel()
                                            {
                                                Omschrijving = $"{kk.CompetentieOnderdeel.Competentie.Naam} / {kk.CompetentieOnderdeel.Naam}: {kk.Omschrijving}",
                                                Id = kk.Id
                                            }).ToList();
        }
    }
}