using System;
using System.Diagnostics;
using Android.Media;
using Android.App;
using Android.Content;
using LoudMouth.Droid.Services;
using LoudMouth.Services;
using LoudMouth;

[assembly: Xamarin.Forms.Dependency(typeof(Recorder))]
namespace LoudMouth.Droid.Services {
    public class Recorder : IRecordingService {
        Context context;

        MediaRecorder _recorder;
        MediaPlayer _player;

        public Recorder() {
            context = Application.Context;
            _player = new MediaPlayer();
            _recorder = new MediaRecorder();
        }

        string filePath;

        public void StartRecording(string filename = Constants.DEFAULT_AUDIO_PATH) {
            StopAudio();

            saveFile(filename);
            filePath = context.FilesDir.AbsolutePath + filename;
            try {
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetAudioEncoder(AudioEncoder.Default);
                _recorder.SetOutputFile(filePath);
                _recorder.Prepare();
                _recorder.Start();
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
        }

        public void StopRecording() {
            _recorder.Stop();
            _recorder.Reset();
            PlayAudio(filePath);
        }

        public void PlayAudio(string path = Constants.DEFAULT_AUDIO_PATH) {
            StopAudio();
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void StopAudio() {
            _player.Stop();
            _player.Reset();
        }

        public void OnDestroy() {
            _player.Release();
            _recorder.Release();
            _player.Dispose();
            _recorder.Dispose();
            _player = null;
            _recorder = null;
        }

        public void saveFile(string filename) {
            System.IO.Stream outputStream;
            try {
                outputStream = context.OpenFileOutput(filename, FileCreationMode.Private);
                outputStream.Close();
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
    }
}
