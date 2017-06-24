using System;
using System.Collections.Generic;
using LoudMouth.Controllers;
using LoudMouth.Models;
using Xamarin.Forms;

namespace LoudMouth.Pages {
    public partial class EnrollPage : ContentPage {
        DataAccessController db;

        public EnrollPage() {
            InitializeComponent();
            db = new DataAccessController();

            SubmitButton.Clicked += OnSubmitClicked;
        }

        void OnSubmitClicked(object sender, EventArgs e) {
            string name = NameEntry.Text;
            Attendee person = new Attendee();
            person.Name = name;
        }
    }
}
