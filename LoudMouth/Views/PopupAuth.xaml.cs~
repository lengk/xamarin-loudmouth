using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using LoudMouth.Models;

namespace LoudMouth.Views {
    public partial class PopupAuth : PopupPage {
        Attendee person;
        NetworkController network;

        public PopupAuth(Attendee person) {
            this.person = person;
            network = new NetworkController();
            InitializeComponent();
        }

        void Close(object sender, EventArgs e) {
            PopupNavigation.PopAsync();
        }

        void Enroll(object sender, EventArgs a) {
            var result = network.CreateProfileFor(person);
        }

        protected override Task OnDisappearingAnimationBegin() {
            return Content.FadeTo(1);
        }
    }
}
