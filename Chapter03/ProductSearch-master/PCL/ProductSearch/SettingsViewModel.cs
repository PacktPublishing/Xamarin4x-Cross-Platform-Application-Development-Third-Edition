using System;

namespace ProductSearch
{
    public class SettingsViewModel
    {
        private readonly ISettings settings;

        public SettingsViewModel()
        {
            settings = ServiceContainer.Resolve<ISettings>();
        }

        public bool IsSoundOn
        {
            get;
            set;
        }

        public void Save()
        {
            settings.IsSoundOn = IsSoundOn;
        }
    }

}

