﻿using System;
using System.Collections.Generic;
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
        public ObservableCollection<AudioFile> Recordings;
        NetworkController nc = new NetworkController();
        IEnumerable<Attendee> people; 


        public LoudMouthPage() {
            people = db.GetAll<Attendee>();
            Recordings = new ObservableCollection<AudioFile>();
            InitializeComponent();
        }

        public void ToggleRecording(object sender, EventArgs args) {
            recording = !recording;
            RecordButton.Text = recording ? "Stop Recording" : "Record";
            if (recording) {
                Task.Run(()=>StartIdentifying());
            }
        }

        private async Task StartIdentifying() {
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
                    CreatedAt = new DateTimeOffset(DateTime.Now),
                };
                await Task.Delay(seconds * 1000);
                file.FinishedAt = new DateTimeOffset(DateTime.Now);
                //file.ResolvedName = await getName(file);
                file = db.Save(file);
                Recordings.Add(file);
                Device.BeginInvokeOnMainThread(()=>db.Save(file));

                audioService.StopRecording();
                count++;
            }
        }

        async Task<string> getName(AudioFile file){
            var talking = await nc.IdentifyProfile(people, file);
            return talking.Name;
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
