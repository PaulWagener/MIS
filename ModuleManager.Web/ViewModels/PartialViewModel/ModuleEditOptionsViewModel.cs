using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ModuleManager.Web.ViewModels.EntityViewModel;
using System.Web.Mvc;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Domain;
using AutoMapper;

namespace ModuleManager.Web.ViewModels.PartialViewModel
{
    public class ModuleEditOptionsViewModel
    {
        public ICollection<TagViewModel> Tags { get; set; }

        public MultiSelectList Competenties { get; set; }
        //public ICollection<CompetentieViewModel> Competenties { get; set; }

        public ICollection<LeerlijnViewModel> Leerlijnen { get; set; }
        public ICollection<WerkvormViewModel> Werkvormen { get; set; }
        public ICollection<ToetsvormViewModel> Toetsvormen { get; set; }
        public ICollection<ModuleVoorkennisViewModel> VoorkennisModules { get; set; }
        public ICollection<NiveauViewModel> Niveaus { get; set; }
        public ICollection<DocentViewModel> Docenten { get; set; }

        public ICollection<StatusViewModel> Statussen { get; set; }
        public void Filter(ModuleViewModel vm)
        {

        }

        public ModuleEditOptionsViewModel(){

        }

        public ModuleEditOptionsViewModel(IUnitOfWork _unitOfWork)
        {
            var competenties = _unitOfWork.GetRepository<Competentie>().GetAll();
            var tags = _unitOfWork.GetRepository<Tag>().GetAll();
            var leerlijnen = _unitOfWork.GetRepository<Leerlijn>().GetAll();
            var werkvormen = _unitOfWork.GetRepository<Werkvorm>().GetAll();
            var toetsvormen = _unitOfWork.GetRepository<Toetsvorm>().GetAll();
            var modules = _unitOfWork.GetRepository<Module>().GetAll();
            var niveaus = _unitOfWork.GetRepository<Niveau>().GetAll();
            var docenten = _unitOfWork.GetRepository<Docent>().GetAll();
            var status = _unitOfWork.GetRepository<Status>().GetAll();

            MultiSelectList competetentieVM = new MultiSelectList(competenties, "Code", "Naam");

            this.Competenties = competetentieVM;
            this.Leerlijnen = leerlijnen.Select(Mapper.Map<Leerlijn, LeerlijnViewModel>).ToList();
            this.Tags = tags.Select(Mapper.Map<Tag, TagViewModel>).ToList();
            this.Toetsvormen = toetsvormen.Select(Mapper.Map<Toetsvorm, ToetsvormViewModel>).ToList();
            this.VoorkennisModules = modules.Select(Mapper.Map<Module, ModuleVoorkennisViewModel>).ToList();
            this.Werkvormen = werkvormen.Select(Mapper.Map<Werkvorm, WerkvormViewModel>).ToList();
            this.Niveaus = niveaus.Select(Mapper.Map<Niveau, NiveauViewModel>).ToList();
            this.Docenten = docenten.Select(Mapper.Map<Docent, DocentViewModel>).ToList();
            Statussen = status.Select(Mapper.Map<Status, StatusViewModel>).ToList();

        }
    }
}