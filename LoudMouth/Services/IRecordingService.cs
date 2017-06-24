using System;
namespace LoudMouth.Services {
    public interface IRecordingService {
        string StartRecording(string path = Constants.DEFAULT_AUDIO_PATH, int seconds = 3);
        void StopRecording();
        void OnDestroy();
        void PlayAudio(string path);
    }
}
