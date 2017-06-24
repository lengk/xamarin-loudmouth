using System;
using Realms;

namespace LoudMouth.Models {

    public class AudioFile : RealmObject {
        public DateTimeOffset CreatedAt { get; set; }
        public string FilePath { get; set; }
        public int seconds { get; set; }


        public override string ToString() {
            return string.Format("[AudioFile: CreatedAt={0}, FilePath={1}, seconds={2}]", CreatedAt, FilePath, seconds);
        }
    }
}
