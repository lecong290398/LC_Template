using Prism.AppModel;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using XamarinSQLlite.Model;

namespace XamarinSQLlite.ViewModels
{
    /// <summary>
    /// This viewmodel extends in another viewmodels.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    internal class BaseViewModel : BindableBase, INotifyPropertyChanged, INavigationAware, IDestructible, IApplicationLifecycleAware
    {

        #region Contructor

        private BaseViewModel()
        {
            var thisType = GetType();

            do
            {
                if (thisType == null)
                {
                    break;
                }

                var methods = thisType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(h => h.GetCustomAttributes<InitializeAttribute>().Any())
                    .ToList();

                foreach (var method in methods)
                {
                    method.Invoke(this, null);
                }

                if (thisType == typeof(BindableBase))
                {
                    break;
                }

                thisType = thisType.BaseType;
            } while (true);
        }


        public BaseViewModel(InitParam initParam) : this()
        {
            NavigationService = initParam.NavigationService;
            DialogService = initParam.DialogService;
            EventAggregator = initParam.EventAggregator;
            PageDialogService = initParam.PageDialogService;
            initParam.CleanUp();
        }


        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected IPageDialogService PageDialogService { get; }

        public virtual void Destroy()
        {
            NavigationService = null;
            DialogService = null;
            EventAggregator = null;
        }



        #endregion


        #region Event handler


        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }


        protected Task HandlError(Exception ex)
        {
            return PageDialogService.DisplayAlertAsync("Thông báo", "Đã có lỗi xảy ra trong quá trình xử lý!", "Đóng");
        }


        #endregion

        #region Methods

        protected Task HandlApiFail(IRestResponse response)
        {
            return PageDialogService.DisplayAlertAsync(
                "Thông báo",
                "Kết nối với máy chủ không thành công!" + Environment.NewLine
                + $"Status code: {response.StatusCode}" + Environment.NewLine
                + $"Error message: {response.ErrorMessage}" + Environment.NewLine
                + $"Exception: {response.ErrorException?.Message}" + Environment.NewLine
                + $"Content: {response.Content}",
                "Đóng");
        }


        public virtual void OnResume()
        {

        }

        public virtual void OnSleep()
        {

        }

        #endregion

        #region BindProp

        #region TitleBindProp

        private string _titleBindProp = "";

        public string TitleBindProp
        {
            get => _titleBindProp;
            set => SetProperty(ref _titleBindProp, value);
        }

        #endregion

        #region IsBusyBindProp

        private bool _isBusyBindProp;

        public bool IsBusyBindProp
        {
            get => _isBusyBindProp;
            set
            {
                SetProperty(ref _isBusyBindProp, value);
                IsNotBusyBindProp = !IsBusyBindProp;
            }
        }

        #endregion

        #region IsNotBusyBindProp

        private bool _isNotBusyBindProp = true;

        public bool IsNotBusyBindProp
        {
            get => _isNotBusyBindProp;
            private set => SetProperty(ref _isNotBusyBindProp, value);
        }

        #endregion

        #endregion
    }
}
