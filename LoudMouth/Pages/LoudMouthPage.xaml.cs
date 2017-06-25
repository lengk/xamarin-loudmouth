using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoudMouth.Controllers;
using LoudMouth.Models;
using LoudMouth.Pages;
using LoudMouth.Services;
using Xamarin.Forms;

namespace LoudMouth {
    public partial class LoudMouthPage : ContentPage {
        IAudioPlayer audioService = DependencyService.Get<IAudioPlayer>();
        RecordingController recorder;
        DataAccessController db = new DataAccessController();
        bool recording;
        public ObservableCollection<AudioFile> Recordings;
        NetworkController nc = new NetworkController();
        IEnumerable<Attendee> people;


        public LoudMouthPage() {
            people = db.GetAll<Attendee>();
            recorder = new RecordingController();
            db.removeAll<AudioFile>();
            Recordings = new ObservableCollection<AudioFile>();
            InitializeComponent();
            RecordingsList.ItemsSource = Recordings;
        }

        public void ToggleRecording(object sender, EventArgs args) {
            recording = !recording;
            RecordButton.Text = recording ? "Stop Recording" : "Record";
            if (recording) {
                Task.Run(() => StartIdentifying());
            } else {
                Task.Run(() => Anaylze());
            }
        }

        bool finished = false;
        int count = 0;

        private async Task StartIdentifying() {
            finished = false;
            int seconds = 7;

            try { seconds = int.Parse(SecondsEntry.Text); } catch (Exception e) { Debug.WriteLine("Failed parse int"); }

            count = 0;
            while (recording) {
                var filename = string.Format("audio{0}", count);
                Device.BeginInvokeOnMainThread(() => recorder.StartRecording(filename, seconds));
                AudioFile file = new AudioFile {
                    Seconds = seconds,
                    FileName = filename,
                    CreatedAt = new DateTimeOffset(DateTime.Now),
                };
                await Task.Delay(seconds * 1000);

                recorder.StopRecording();
                file.FinishedAt = new DateTimeOffset(DateTime.Now);
                while (recorder.audio.IsRecording) { }

                processFile(file);

                count++;
            }
            finished = true;
        }

        private void processFile(AudioFile file) {
            Device.BeginInvokeOnMainThread(async () => {
                file.ResolvedName = await getName(file);
                    CurrentTalker.Text = file.ResolvedName;
                db.Save(file);
            });
        }

        private async Task Anaylze() {
            var files = db.GetAll<AudioFile>().Where((arg) => {
                return arg.FileName.Contains("audio");
            });

            foreach (var file in files) {
                var talker = people.FirstOrDefault(t => t.Name == file.ResolvedName);
                if (talker != null) {
                    talker.DurationTalked += file.Seconds;
                }
            }
            db.SaveAll(people);
            await Navigation.PushAsync(new ResultsPage());
        }

        async Task<string> getName(AudioFile file) {
            var talking = await nc.IdentifyProfile(people, file);
            return talking.Name;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            audioService.OnDestroy();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            AudioFile file = e.SelectedItem as AudioFile;
            audioService.PlayAudio(file.FileName);
        }
    }
}
