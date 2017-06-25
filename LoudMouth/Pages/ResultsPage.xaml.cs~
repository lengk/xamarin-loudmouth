using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LoudMouth.Controllers;
using LoudMouth.Models;
using Xamarin.Forms;

namespace LoudMouth.Pages {
    public partial class ResultsPage : ContentPage {
        ObservableCollection<Attendee> data;
        IEnumerable<Attendee> people;
        DataAccessController db = new DataAccessController();
        int totalseconds = 0;

        public ResultsPage() {
            InitializeComponent();
            people = db.GetAll<Attendee>();
            foreach(var p in people){
                totalseconds += p.DurationTalked;
            }

            foreach (var p in people) {
                double percent = p.DurationTalked / totalseconds;
                p.Name = string.Format("{0}: {1}s {2}%", p.Name, p.DurationTalked, Math.Round(percent));
            }

            data = new ObservableCollection<Attendee>(people);
            ResultsList.ItemsSource = data;
        }

        void StartAgain(object sender, EventArgs a){
            Navigation.PopToRootAsync();
        }
    }
}
