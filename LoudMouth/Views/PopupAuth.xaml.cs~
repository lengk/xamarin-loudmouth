using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using LoudMouth.Models;
using LoudMouth.Services;
using LoudMouth.Controllers;

namespace LoudMouth.Views {
    public partial class PopupAuth : PopupPage {
        Attendee person;
        NetworkController network;
        RecordingController recorder = new RecordingController();
        IAudioPlayer player = DependencyService.Get<IAudioPlayer>();
        bool enroling = false;
        Action<bool> callback;

        public PopupAuth(Attendee person, Action<bool> callback) {
            this.callback = callback;
            this.person = person;
            network = new NetworkController();
            InitializeComponent();
        }

        void Close(object sender, EventArgs e) {
            PopupNavigation.PopAsync();
        }

        void Enroll(object sender, EventArgs a) {
            enroling = !enroling;

            if (!enroling || string.IsNullOrEmpty(person.Name))
                return;

            ProgressIndicator.IsVisible = true;
            var startTime = DateTime.Now;
            recorder.StartRecording(person.Name);
            AudioFile file = new AudioFile {
                CreatedAt = new DateTimeOffset(DateTime.Now),
                FileName = person.Name,
            };

            Task.Run(async () => {
                
                while (enroling) {
                    var timespan = (DateTime.Now - startTime).Seconds;
                    Device.BeginInvokeOnMainThread(() => {
                        EnrollButton.Text = timespan + " seconds";
                    });
                }

                recorder.StopRecording();
                while(recorder.audio.IsRecording){}
                file.FinishedAt = DateTime.Now;
                file.Seconds = (DateTime.Now - startTime).Seconds;
                if (await network.EnrolProfile(person, file)) {
                    callback.Invoke(true);
                    await PopupNavigation.PopAsync();
                    recorder = null;
                }
                Device.BeginInvokeOnMainThread(() => {
                    ProgressIndicator.IsVisible = false;
                });
            });
        }

        protected override Task OnDisappearingAnimationBegin() {
            return Content.FadeTo(1);
        }
    }
}
