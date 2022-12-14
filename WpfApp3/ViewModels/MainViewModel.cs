using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        //包含上步和下步的导航，在调用Open()时候更新这个navigationJournal
        private IRegionNavigationJournal navigationJournal;
        public DelegateCommand<string> OpenCommand { get; private set; }

        public DelegateCommand BackCommand { get; private set; }
        public MainViewModel(IRegionManager regionManager)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            BackCommand = new DelegateCommand(Back);
            this.regionManager = regionManager;
        }

        private void Back()
        {
            if (navigationJournal.CanGoBack)
                navigationJournal.GoBack();
        }

        private void Open(string obj)
        {

            NavigationParameters keys = new NavigationParameters();
            keys.Add("Title", "Hello");
            regionManager.Regions["ContentRegion"].RequestNavigate(obj, callback =>
            {
                if ((bool)callback.Result)
                {
                    navigationJournal = callback.Context.NavigationService.Journal;
                }
            }, keys);
        }
    }
}
