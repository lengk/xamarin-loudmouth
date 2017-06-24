using System;
namespace LoudMouth.Services {
    public interface IRecordingService {
        void StartRecording(string path);
        void StopRecording();
        void OnDestroy();
        void PlayAudio(string path);
    }
}
