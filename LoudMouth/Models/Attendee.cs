﻿using System;
using Realms;

namespace LoudMouth.Models {

    public class Attendee : RealmObject {

        [PrimaryKey]
        public string Name { get; set; }
        public string ProfileID { get; set; }
        public int DurationTalked { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
