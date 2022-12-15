using DryIoc;
using ModuleC.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModuleC.Views
{
    /// <summary>
    /// ViewDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ViewDialog : UserControl
    {
        public ViewDialog(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<MessageEvent>().Subscribe(showMsg);
            eventAggregator.GetEvent<MessageEvent>().Unsubscribe(showMsg);
        }

        public   void showMsg(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
