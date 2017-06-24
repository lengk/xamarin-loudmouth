using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LoudMouth.Controllers;
using LoudMouth.Models;
using LoudMouth.Services;
using Xamarin.Forms;

namespace LoudMouth {
    public partial class LoudMouthPage : ContentPage {
        IRecordingService audioService = DependencyService.Get<IRecordingService>();
        DataAccessController db = new DataAccessController();
        bool recording;
        ObservableCollection<AudioFile> recordings;

        public LoudMouthPage() {
            InitializeComponent();
            recordings = new ObservableCollection<AudioFile>();
            RecordingsList.ItemsSource = recordings;
        }

        public void ToggleRecording(object sender, EventArgs args) {
            recording = !recording;
            RecordButton.Text = recording ? "Stop Recording" : "Record";
            if (recording) {
                Task.Run(()=>beginRecordings());
            }
        }

        private async Task beginRecordings() {
            int seconds = 3;
            try {
                seconds = int.Parse(SecondsEntry.Text);
            } catch (Exception){}
            var count = 0;
            while (recording) {
                var filename = string.Format("audio{0}.3pgg", count);
                audioService.StartRecording(filename, seconds);
                AudioFile file = new AudioFile {
                    FilePath = filename,
                    Seconds = seconds,
                    CreatedAt = new DateTimeOffset(DateTime.Now)
                };
                recordings.Add(file);
                Device.BeginInvokeOnMainThread(()=>db.Save(file));
                await Task.Delay(3000);
                audioService.StopRecording();
                count++;
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            audioService.OnDestroy();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            AudioFile file = e.SelectedItem as AudioFile;
            audioService.PlayAudio(file.FilePath);
        }
    }
}
