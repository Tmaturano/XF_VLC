using System;
using VLCSample.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VLCSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CustomNavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
