using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Monitoring_WPF
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Dispatcher dispatcher;
        private FileSystemWatcher watcher;
        private byte[] standartImage;
        public ObservableCollection<PersonInfo> Collection { get; private set; } 

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyCahnged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public MainViewModel() 
        {
            standartImage = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandartImage", "reviews-icon.jpg"));
            Collection = new ObservableCollection<PersonInfo>();
            dispatcher = Dispatcher.CurrentDispatcher;
            if (Directory.Exists(Properties.Settings.Default.PathToFolder))
            {
                AddOldInfo();
                StartWatch();
            }          
        }

        private void AddToCollection(FileInfo pathFile)
        {
            int MaxLenthFileToLoad = 15 * 1024*1024;

            if(WaitForFile(pathFile.FullName))
            {
                if (Collection.Count > Properties.Settings.Default.MaxCount)
                    DeleteLastElement();
              
                    dispatcher.Invoke(new Action(() =>
                    {
                        Collection.Insert(0, new PersonInfo(
                            pathFile.Length > MaxLenthFileToLoad ? standartImage : LoadImage(pathFile.FullName),
                            GetName(pathFile.Name, pathFile.LastWriteTime),
                            GetTemp(pathFile.Name),
                            pathFile.FullName
                        ));
                    }));
            }                                       
        }

        private void StartWatch()
        {
            watcher = new FileSystemWatcher(Properties.Settings.Default.PathToFolder)
            {
                NotifyFilter = NotifyFilters.CreationTime
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size
            };

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;

            watcher.Filter = @"*.jpg";
            watcher.EnableRaisingEvents = true;
        }
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            RemoveFromColection(e.FullPath);
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                if (CheckFile(e.Name))
                    AddToCollection(new FileInfo(e.FullPath));
        }
        private void OnRenamed(object sender, FileSystemEventArgs e)
        {
            try
            {
                var renamedEvent = e as RenamedEventArgs;
                var fileInfo = new FileInfo(e.FullPath);
                if (CheckFile(e.Name))
                {
                    PersonInfo person = Collection.Where(x => x.Path.Equals(renamedEvent.OldFullPath)).Select(q => q).First();
                    person.NameAndDate = GetName(fileInfo.Name, fileInfo.LastWriteTime);
                    person.Temp = GetTemp(e.Name);
                    person.Path = fileInfo.FullName;
                }
            }
            catch
            {
            }

        }
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            new Task(new Action(() =>
            {
                if (CheckFile(e.Name))
                    AddToCollection(new FileInfo(e.FullPath));
            })).Start();
        }

        private bool IsFileReady(string filename)
        {           
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WaitForFile(string filename)
        {
            bool suc = false;
            int timeToLoad = 10000;
            var time = Stopwatch.StartNew();
            while (!suc && time.ElapsedMilliseconds < timeToLoad) 
            {
                suc = IsFileReady(filename);
            }
            return suc;
        }       
        private byte[] LoadImage(string Path)
        {
            Bitmap image = new Bitmap(Path, true);

            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Bmp);
            image.Dispose();
            return  memoryStream.ToArray();

        }
             
        private string GetTemp(string fileName)
        {
            return Regex.Match(fileName, @"\d{2}[\.\,]{0,1}\d*").Value.Replace('.', ',');
        }
        private string GetName(string fileName, DateTime dateTime)
        {
            return Regex.Match(fileName, @".*?_.*?_.*?(?=_)").Value.Replace('_', ' ') +
                                Environment.NewLine + Environment.NewLine +
                                dateTime.ToString("dd.MM.yyyy hh:mm:ss");
        }

        private void DeleteLastElement()
        {
            dispatcher.Invoke(new Action(() =>
            {
                Collection.RemoveAt(Collection.Count - 1);
            }));
        }
        private void AddOldInfo()
        {
            var files = new DirectoryInfo(Properties.Settings.Default.PathToFolder).GetFiles()
                .Where
                (x => Regex.IsMatch(x.Name, @".*?_.*?_.*?_\d{2}[\.\,]{0,1}\d*\.jpg"))
                .OrderByDescending(x => x.LastWriteTime)
                .Take(Properties.Settings.Default.MaxCount).ToList();

            foreach (var file in files)
            {
                AddToCollection(file);
            }
        }
        private bool CheckFile(string name)
        {
            return Regex.IsMatch(name, @".*?_.*?_.*?_\d{2}[\.\,]{0,1}\d*\.jpg");
                
        }
        private void RemoveFromColection(string fullPath) 
        {
            try
            {
                dispatcher.Invoke(new Action(() =>
                {
                    var t = Collection.Where(x => x.Path.Equals(fullPath)).Select(q => q).First();
                    Collection.Remove(Collection.Where(x => x.Path.Equals(fullPath)).Select(q => q).First());
                }));
            }
            catch(InvalidOperationException iEx)
            {
            }      
        }
    }
}
