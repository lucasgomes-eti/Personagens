using PersonagensApp.Data;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(PersonagensApp.Droid.Config))]

namespace PersonagensApp.Droid
{
    public class Config : IConfig
    {
        private string _diretorioDB;
        public string DiretorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(_diretorioDB))
                {
                    _diretorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _diretorioDB;
            }
        }

        private ISQLitePlatform _plataforma;
        public ISQLitePlatform Plataforma
        {
            get
            {
                if (_plataforma == null)
                {
                    _plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return _plataforma;
            }
        }
        public Config()
        {

        }
    }
}