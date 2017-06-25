using System;
using System.Diagnostics;
using Android.Media;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Forms;
using XLabs.Platform.Device;
using XLabs.Ioc;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Mvvm;

namespace LoudMouth.Droid {
    [Activity(Label = "LoudMouth.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid {
        protected override void OnCreate(Bundle bundle) {

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            try {
                LoadApplication(new App());

                var container = App.container;
                var device = AndroidDevice.CurrentDevice;

                if (!Resolver.IsSet) {
                    var app = new XFormsAppDroid();
                    app.Init(this);

                    container.Register<IDevice>((arg) => device)
                        .Register<IAudioStream>(t => device.Microphone)
                        .Register<IDisplay>((t) => t.Resolve<IDevice>().Display)
                             .Register<ISoundService>(t => t.Resolve<ISoundService>())
                             .Register<IXFormsApp>(app);

                    Resolver.SetResolver(container.GetResolver());
                }

            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
