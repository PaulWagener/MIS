using ModuleManager.Web.ViewModels.EntityViewModel;
using ModuleManager.Web.ViewModels.PartialViewModel;

namespace ModuleManager.Web.ViewModels
{
    public class ModuleEditViewModel
    {
        public ModuleViewModel Module { get; set; }
        public ModuleEditOptionsViewModel Options { get; set; }

        /// <summary>
        /// Used by the form to show how the form was submitted
        /// possible values: close, stay
        /// </summary>
        public string AfterSubmit { get; set; }

        public ModuleEditViewModel()
        {

        }

        public ModuleEditViewModel(ModuleViewModel _module)
        {
            this.Module = _module;
        }

    }
}