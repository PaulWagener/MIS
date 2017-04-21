using System.Linq;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;
using AutoMapper;
using ModuleManager.Web.ViewModels.EntityViewModel.Competenties;
using ModuleManager.Web.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ModuleManager.Web
{
    public static class AutoMapperConfiguration
    {
        const string Delimiter = ", ";
        public static void Configure()
        {
            Mapper.CreateMap<Module, ModuleLockViewModel>();
            Mapper.CreateMap<Module, ModuleViewModel>()
                .ForMember(dest => dest.Voorkennis, opt => opt.MapFrom(src => src.Voorkennismodules));
            Mapper.CreateMap<Module, ModuleVoorkennisViewModel>();
            Mapper.CreateMap<StudiePunt, StudiePuntenViewModel>();
            Mapper.CreateMap<StudieBelasting, StudieBelastingViewModel>().ReverseMap();
            Mapper.CreateMap<ModuleWerkvorm, ModuleWerkvormViewModel>().ReverseMap();
            Mapper.CreateMap<Weekplanning, WeekplanningViewModel>().ReverseMap();
            Mapper.CreateMap<Beoordeling, BeoordelingenViewModel>();
            Mapper.CreateMap<Leermiddel, LeermiddelenViewModel>().ReverseMap();
            Mapper.CreateMap<Leerdoel, LeerdoelenViewModel>().ReverseMap();
            Mapper.CreateMap<Docent, DocentViewModel>();
            Mapper.CreateMap<Leerlijn, LeerlijnViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>().ReverseMap();
            Mapper.CreateMap<Werkvorm, WerkvormViewModel>();
            Mapper.CreateMap<Toetsvorm, ToetsvormViewModel>();
            Mapper.CreateMap<FaseType, FaseTypeViewModel>();
            Mapper.CreateMap<Status, StatusViewModel>();

            Mapper.CreateMap<Competentie, CompetentieViewModel>()
                .ForMember(dest => dest.Onderdelen, opt => opt.MapFrom(src => src.CompetentieOnderdelen));
            Mapper.CreateMap<CompetentieOnderdeel, CompetentieOnderdeelViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Competentie.Naam.Substring(0, 1) + src.Volgnummer));
            Mapper.CreateMap<Kwaliteitskenmerk, KwaliteitskenmerkViewModel>()
                .ForMember(dest => dest.Omschrijving, opt => opt.MapFrom(src => $"{src.CompetentieOnderdeel.Competentie.Naam}/{src.CompetentieOnderdeel.Naam}: {src.Omschrijving}"));

            Mapper.CreateMap<Module, ModulePartialViewModel>()
                .ForMember(dest => dest.TotalEc, opt => opt.MapFrom(
                    src => src.StudiePunten
                        .Select(sp => sp.EC).Sum()))
                .ForMember(dest => dest.Docenten, opt => opt.MapFrom(
                    src => string.Join(Delimiter, src.Docenten.Select(inSrc => inSrc.Naam))))
                .ForMember(dest => dest.Verantwoordelijke, opt => opt.MapFrom(
                    src => src.Verantwoordelijke.Naam))
                .ForMember(dest => dest.Blokken, opt => opt.MapFrom(src => src.Blok));

            Mapper.CreateMap<Module, ModuleTabelViewModel>()
                .ForMember(dest => dest.Cursuscode, opt => opt.MapFrom(
                    src => src.CursusCode))
                .ForMember(dest => dest.Omschrijving, opt => opt.MapFrom(
                    src => src.Naam))
                .ForMember(dest => dest.Werkvormen, opt => opt.MapFrom(
                    src => string.Join(Delimiter, src.ModuleWerkvormen
                        .Select(inSrc => inSrc.WerkvormType))))
                .ForMember(dest => dest.Studiepunten, opt => opt.MapFrom(
                    src => src.StudiePunten))
                .ForMember(dest => dest.Contacturen, opt => opt.MapFrom(
                    src => src.StudieBelastingen.Sum(inSrc => inSrc.ContactUren)));

            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Naam, opt => opt.MapFrom(
                    src => src.naam))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(
                    src => src.email))
                .ForMember(dest => dest.GebruikersNaam, opt => opt.MapFrom(
                    src => src.UserNaam));
        }

        public static IEnumerable<TEntity> DbReferences<TEntity>(
            Func<DomainEntities, DbSet<TEntity>> set, Expression<Func<TEntity, bool>> where)
            where TEntity : class
        {
            using (var entities = new DomainEntities())
            {
                return set(entities).Where(where);
            }
        }
    }
}