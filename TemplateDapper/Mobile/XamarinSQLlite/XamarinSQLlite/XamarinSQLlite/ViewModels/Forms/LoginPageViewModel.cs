using Prism.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XamarinSQLlite.Core.Logic;
using XamarinSQLlite.Model;

namespace XamarinSQLlite.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    internal class LoginPageViewModel : BaseViewModel
    {
        #region Constructor
        private static Lc_LoginLogic _LoginLogic;
        public LoginPageViewModel(InitParam initParam, Lc_LoginLogic lc_Login) : base(initParam)
        {
            TitleBindProp = "Giới Thiệu / Liên Hệ";
            _LoginLogic = lc_Login;
        }

        #endregion


        #region Fields

        private string password;

        #endregion

        #region Property



        #region EmailBindProp

        private string _EmailBindProp = null;
        public string EmailBindProp
        {
            get
            {
                return _EmailBindProp;
            }
            set
            {
                SetProperty(ref _EmailBindProp, value);
            }
        }

        #endregion





        #region PasswordBindProp

        private string _PasswordBindProp = null;
        public string PasswordBindProp
        {
            get
            {
                return _PasswordBindProp;
            }
            set
            {
                SetProperty(ref _PasswordBindProp, value);
            }
        }

        #endregion

        #endregion

        #region Command



        #region LoginCommand

        public DelegateCommand<object> LoginCommand { get; private set; }
        private async void OnLogin(object obj)
        {
            if (IsBusyBindProp)
            {
                return;
            }

            IsBusyBindProp = true;

            var result = await _LoginLogic.LoginAsyns(EmailBindProp, PasswordBindProp);


            IsBusyBindProp = false;
        }

        [Initialize]
        private void InitLoginommand()
        {
            LoginCommand = new DelegateCommand<object>(OnLogin);
            LoginCommand.ObservesCanExecute(() => IsNotBusyBindProp);
        }

        #endregion




        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}