using System;
namespace LoudMouth.Services {
    public interface IRecordingService {
        void StartRecording(string path = Constants.DEFAULT_AUDIO_PATH);
        void StopRecording();
        void OnDestroy();
        void PlayAudio(string path);
    }
}
