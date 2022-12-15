using ModuleC.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleC.ViewModels
{
    internal class ViewDialogViewModel : IDialogAware
    {
        private readonly IEventAggregator eventAggregator;

        public string Title { get; set; }
        public string CallBackIno { get; set; }

        public event Action<IDialogResult> RequestClose;
        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public ViewDialogViewModel(IEventAggregator eventAggregator)
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            this.eventAggregator = eventAggregator;
        }

        private void Cancel()
        {
            //发送一个MessageEvent类型的消息，内容是CallBackIno
            eventAggregator.GetEvent<MessageEvent>().Publish(CallBackIno ?? "");
            //RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            DialogParameters keys = new DialogParameters();
            keys.Add("value", CallBackIno);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }

        public bool CanCloseDialog()
        {
            return true;
        }


        public void OnDialogClosed()
        {
            //RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");

        }
    }
}
