using System.Linq;
using ModuleManager.Domain;
using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;
using AutoMapper;

namespace ModuleManager.Web
{
    public static class AutoMapperConfiguration
    {
        const string Delimiter = ", ";
        public static void Configure()
        {

            Mapper.CreateMap<Module, ModuleLockViewModel>();

            Mapper.CreateMap<Module, ModuleViewModel>();

            Mapper.CreateMap<Module, ModuleVoorkennisViewModel>();

            Mapper.CreateMap<ModuleCompetentie, ModuleCompetentieViewModel>();

            Mapper.CreateMap<Competentie, CompetentieViewModel>();

            Mapper.CreateMap<StudiePunt, StudiePuntenViewModel>();

            Mapper.CreateMap<StudieBelasting, StudieBelastingViewModel>();

            Mapper.CreateMap<ModuleWerkvorm, ModuleWerkvormViewModel>();

            Mapper.CreateMap<Weekplanning, WeekplanningViewModel>();

            Mapper.CreateMap<Beoordeling, BeoordelingenViewModel>();

            Mapper.CreateMap<Leermiddel, LeermiddelenViewModel>();

            Mapper.CreateMap<Leerdoel, LeerdoelenViewModel>();

            Mapper.CreateMap<Docent, DocentViewModel>();

            Mapper.CreateMap<Leerlijn, LeerlijnViewModel>();

            Mapper.CreateMap<Tag, TagViewModel>();

            Mapper.CreateMap<Werkvorm, WerkvormViewModel>();

            Mapper.CreateMap<Toetsvorm, ToetsvormViewModel>();

            Mapper.CreateMap<FaseType, FaseTypeViewModel>();

            Mapper.CreateMap<Niveau, NiveauViewModel>();

            Mapper.CreateMap<Status, StatusViewModel>();

            Mapper.CreateMap<Module, ModulePartialViewModel>()
                .ForMember(dest => dest.TotalEc, opt => opt.MapFrom(
                    src => src.StudiePunten
                        .Select(sp => sp.EC).Sum()))
                .ForMember(dest => dest.Docenten, opt => opt.MapFrom(
                    src => string.Join(Delimiter, src.Docenten.Select(inSrc => inSrc.Naam))));

            Mapper.CreateMap<Module, ModuleTabelViewModel>()
                .ForMember(dest => dest.Onderdeel, opt => opt.MapFrom(
                    src => src.OnderdeelCode)) // TODO:
                .ForMember(dest => dest.Cursuscode, opt => opt.MapFrom(
                    src => src.CursusCode)) // TODO:
                .ForMember(dest => dest.Omschrijving, opt => opt.MapFrom(
                    src => src.Naam)) // TODO:
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
    }
}