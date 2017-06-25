using Xamarin.Forms;
using LoudMouth.Droid;
using LoudMouth.Services;
using System;
using System.IO;

[assembly: Dependency(typeof(LoudMouth.Droid.Services.FileLoader))]
namespace LoudMouth.Droid.Services {
    public class FileLoader : IFileLoader {
        string AbsolutePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public byte[] LoadFile(string filename) {
            return File.ReadAllBytes(Path.Combine(AbsolutePath, filename));
        }

        public Stream LoadWriteStream(string filename) {
            return File.OpenWrite(Path.Combine(AbsolutePath, filename));
        }
    }
}