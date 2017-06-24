using System;
using System.Threading.Tasks;
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
            recordButton.Text = recording ? "Stop Recording" : "Record";
            if (!recording) {
                Task.Run(() => audioService.StopRecording());
            } else {
                Task.Run(() => audioService.StartRecording());
            }

        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            audioService.OnDestroy();
        }
    }
}
