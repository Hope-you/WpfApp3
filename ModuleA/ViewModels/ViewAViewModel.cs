using Prism.Regions;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware,IConfirmNavigationRequest
    {

        public ViewAViewModel()
        {

        }
        private string title;

        public string Title
        {
            get { return title; }
            //通知更新 基于 BindableBase
            set { title = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 每次重新导航，该实例是否重用原来的实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 用来接收传递过来的值。
        /// NavigationParameters keys = new NavigationParameters();
        /// keys.Add("Title", "Hello");
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Title"))
            {
                Title = navigationContext.Parameters.GetValue<string>("Title");
            }
            
        }

        /// <summary>
        /// 从当前View切出去之后会执行这个 continuationCallback为false则不会切换，
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool res = true;
            if (MessageBox.Show("是否切换？","询问",MessageBoxButton.YesNo)==MessageBoxResult.No)
            {
                res = false;

            }
            continuationCallback(res);
        }
    }
}
