using System;
using Realms;

namespace LoudMouth.Models {
    
    public class Attendee : RealmObject{

        [PrimaryKey]
        public string Name;
        public string ProfileID;
        public int DurationTalked = 0;
    }
}
