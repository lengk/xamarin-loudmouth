using Xamarin.Forms;
using LoudMouth.Pages;
using LoudMouth.Controllers;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services.Media;
using System.Threading.Tasks;
using LoudMouth.Services;

namespace LoudMouth
{
    public partial class App : Application
    {
        public static App instance { get; set; }
        public static SimpleContainer container { get; set; }

        public App()
        {
            instance = this;
            InitializeComponent();
            container = new SimpleContainer();
            MainPage = new NavigationPage(new EnrollPage());
            var item = new ToolbarItem();
            item.Text = "Settings";
            MainPage.ToolbarItems.Add(new ToolbarItem());
        }

        protected override void OnStart()
        {
            //AudioProcessor audio = new AudioProcessor("file.wav");
            //new DataAccessController().removeAll();
            //audio.Record.Execute(null);
            //Task.Run(async () => {
            //    await Task.Delay(2000);
            //    audio.Stop.Execute(null);
            //});
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
