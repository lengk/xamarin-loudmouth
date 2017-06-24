using System;
using System.Threading.Tasks;
using LoudMouth.Models;
using LoudMouth.Services;
using Xamarin.Forms;

namespace LoudMouth {
    public partial class LoudMouthPage : ContentPage {
        bool recording;
        IRecordingService audioService = DependencyService.Get<IRecordingService>();

        public LoudMouthPage() {
            InitializeComponent();
        }

        public void ToggleRecording(object sender, EventArgs args) {
            recording = !recording;
            RecordButton.Text = recording ? "Stop Recording" : "Record";
            if (!recording) {
                Task.Run(() => audioService.StopRecording());
            } else {
                Task.Run(() => {
                    var filename = audioService.StartRecording();
                    AudioFile file = new AudioFile() {
                        FilePath = filename,
                        CreatedAt = new DateTimeOffset(DateTime.Now)
                    };
                });
            }

        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            audioService.OnDestroy();
        }
    }
}
