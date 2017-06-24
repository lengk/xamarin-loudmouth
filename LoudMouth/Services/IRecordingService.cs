using System;
namespace LoudMouth.Services {
    public interface IRecordingService {
        void StartRecording();
        void StopRecording();
        void OnDestroy();
        void PlayAudio(string path);
    }
}
