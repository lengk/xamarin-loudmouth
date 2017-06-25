using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using LoudMouth.Models;
using LoudMouth.Services;

namespace LoudMouth.Views {
    public partial class PopupAuth : PopupPage {
        Attendee person;
        NetworkController network;
        IRecordingService recorder = DependencyService.Get<IRecordingService>();
        bool enroling = false;
        public PopupAuth(Attendee person) {
            this.person = person;
            network = new NetworkController();
            InitializeComponent();
        }

        void Close(object sender, EventArgs e) {
            PopupNavigation.PopAsync();
        }

        void Enroll(object sender, EventArgs a) {
            enroling = !enroling;
            if (!enroling) return;
            ProgressIndicator.IsVisible = true;
            var startTime = DateTime.Now;
            Task.Run(async () => {
                recorder.StartRecording(person.Name);
                AudioFile file = new AudioFile {
                    CreatedAt = new DateTimeOffset(DateTime.Now),
                    FilePath = person.Name,

                };
                while (enroling) {
                    var timespan = (DateTime.Now - startTime).Seconds;
                    Device.BeginInvokeOnMainThread(()=>{
                        EnrollButton.Text = timespan + " seconds";    
                    });

                }
                
                recorder.StopRecording();
                file.FinishedAt = DateTime.Now;
                file.Seconds = (DateTime.Now - startTime).Seconds;
                Device.BeginInvokeOnMainThread(()=>ProgressIndicator.IsVisible = false);
                if (await network.EnrolProfile(person, file))
                    await PopupNavigation.PopAsync();
            });
        }

        protected override Task OnDisappearingAnimationBegin() {
            return Content.FadeTo(1);
        }
    }
}
