using Xamarin.Forms;
using LoudMouth.Droid;
using LoudMouth.Services;
using System;
using System.IO;

[assembly: Dependency(typeof(LoudMouth.Droid.Services.FileLoader))]
namespace LoudMouth.Droid.Services {
    public class FileLoader : IFileLoader {
        
        public byte[] LoadFile(string filename) {
            return File.ReadAllBytes(Android.App.Application.Context.FilesDir.AbsolutePath + filename);
        }

        public Stream LoadWriteStream(string filename){
            string path = Android.App.Application.Context.FilesDir.AbsolutePath + filename;
            if (!File.Exists(path)){
                File.Create(path);
            }
            return File.OpenRead(Android.App.Application.Context.FilesDir.AbsolutePath + filename);
        }
    }
}