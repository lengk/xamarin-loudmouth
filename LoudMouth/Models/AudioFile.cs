using System;
using Realms;

namespace LoudMouth.Models {

    public class AudioFile : RealmObject {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset FinishedAt { get; set; }

        public string FilePath { get; set; }
        public int Seconds { get; set; }
        public string ResolvedName {get; set;}

        public override string ToString() {
            return string.Format("[Name={0}FilePath={1}, seconds={2}]", ResolvedName, FilePath, Seconds);
        }
    }
}
