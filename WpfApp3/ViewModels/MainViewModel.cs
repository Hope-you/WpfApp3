using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
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
        public readonly IDialogService dialogService;
        private string dialogCallBack;

        public string DialogCallBack
        {
            get { return dialogCallBack; }
            set { dialogCallBack = value; RaisePropertyChanged(); }
        }


        //包含上步和下步的导航，在调用Open()时候更新这个navigationJournal
        private IRegionNavigationJournal navigationJournal;
        public DelegateCommand<string> OpenCommand { get; private set; }

        public DelegateCommand BackCommand { get; private set; }


        public DelegateCommand<string> DialogCommand { get; private set; }
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public MainViewModel(IRegionManager regionManager, IDialogService dialogService)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {
            OpenCommand = new DelegateCommand<string>(Open);
            BackCommand = new DelegateCommand(Back);
            DialogCommand = new DelegateCommand<string>(OpenDialog);
            this.regionManager = regionManager;
            this.dialogService = dialogService;
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
                if (callback.Result != null && (bool)callback.Result)
                {
                    navigationJournal = callback.Context.NavigationService.Journal;
                }
            }, keys);
        }

        private void OpenDialog(string obj)
        {

            DialogParameters keys = new DialogParameters();
            keys.Add("Title", "加载数据中。。。");
            dialogService.ShowDialog(obj, keys, callback =>
            {
                switch (callback.Result)
                {
                    case ButtonResult.Abort:
                        break;
                    case ButtonResult.Cancel:
                        break;
                    case ButtonResult.Ignore:
                        break;
                    case ButtonResult.No:
                    case ButtonResult.None:
                        DialogCallBack = "无数据";
                        break;
                    case ButtonResult.OK:
                        DialogCallBack = callback.Parameters.GetValue<string>("value");
                        break;
                    case ButtonResult.Retry:
                        break;
                    case ButtonResult.Yes:
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
