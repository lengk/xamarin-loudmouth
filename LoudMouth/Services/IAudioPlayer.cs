using System;
namespace LoudMouth.Services {
    public interface IAudioPlayer {
        void OnDestroy();
        void PlayAudio(string path);
    }
}
