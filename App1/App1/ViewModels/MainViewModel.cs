using Android.Content;
using Android.Content.PM;
using App1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class MainViewModel: BaseViewModel
    {

        public const string REQUEST_URI = "https://publicstorage.hb.bizmrg.com/sirius/result.json";

        public ICommand OnLoadedCommand { get; }
        public ICommand OnItemClickCommand { get; }
        readonly IList<ApplicationInfo> apps;

        public MainViewModel()
        {
            OnItemClickCommand = new Command<Service>(OnItemClicked);

            Load();
        }

        private void OnItemClicked(Service obj)
        {


            try
            {
                var aUri = Android.Net.Uri.Parse(obj.link);
                var intent = new Intent(Intent.ActionView, aUri);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
            }

            //Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(obj.link);


            //if (intent != null)
            //{
            //    intent.AddFlags(ActivityFlags.NewTask);
            //    Android.App.Application.Context.StartActivity(intent);

            //}
            //else
            //{
            //    await Launcher.OpenAsync(obj.link);
            //}
                
        }

        private async void Load()
        {
            HttpClient cl = new HttpClient();

            var json = await cl.GetStringAsync(REQUEST_URI);

            Rootobject rootobject = JsonConvert.DeserializeObject<Rootobject>(json);

            for (int i = 0; i < rootobject.body.services.Length; i++)
            {
                var serv = rootobject.body.services[i];
                serv.Icon = ImageSource.FromUri(new Uri(serv.icon_url));
            }

            Items = new ObservableCollection<Service>(rootobject.body.services);

        }

        private ObservableCollection<Service> items;

        public ObservableCollection<Service> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(); }
        }


    }
}
