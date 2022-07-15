using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Models
{

    public class Rootobject
    {
        public Body body { get; set; }
        public int status { get; set; }
    }

    public class Body
    {
        public Service[] services { get; set; }
    }

    public class Service
    {
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string icon_url { get; set; }

        [JsonIgnore]
        public ImageSource Icon { get; set; }
    }

}
