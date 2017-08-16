using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class MasterDetailNavigationPage : MasterDetailPage
    {
        public MasterDetailNavigationPage()
        {
            InitializeComponent();
            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new MainPage());
            App.MasterDetail = this;
        }   
    }
}
