using App1.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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


        public MainViewModel()
        {
            OnItemClickCommand = new Command<Service>(OnItemClicked);
            Load();
        }

        private async void OnItemClicked(Service obj)
        {
            await Launcher.OpenAsync(obj.link);
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
