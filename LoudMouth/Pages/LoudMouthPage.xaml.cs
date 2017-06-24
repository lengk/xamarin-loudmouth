using System;
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
            if (recording) audioService.StopRecording();
            else audioService.StartRecording();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            audioService.OnDestroy();
        }
    }
}
