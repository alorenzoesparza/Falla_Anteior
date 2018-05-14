namespace Falla.ViewModels
{
    using Models;

    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        //public ActsViewModel Acts { get; set; }
        //public ActViewModel Act { get; set; }
        #endregion

        #region Propiedades
        public string BaseUrl { get; set; }
        public string ApiUrl { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;

            BaseUrl = "http://api.antoniole.com";
            ApiUrl = "/api";
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
