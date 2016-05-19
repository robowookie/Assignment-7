using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using KinveyXamarin;
using System.Drawing;


namespace Assignment_7
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ReceiptClass
    {
        [JsonProperty("_id")]
        public string id { get; set; }
        [JsonProperty]
        public string user { get; set; }
        [JsonProperty]
        public string business { get; set; }
        [JsonProperty]
        public DateTime dateAndTime { get; set; }
        [JsonProperty]
        public string taxInvoice { get; set; }
        [JsonProperty]
        public string gstNum { get; set; }
        [JsonProperty]
        public string serverName { get; set; }
        [JsonProperty]
        public string productName { get; set; }
        [JsonProperty]
        public double productTotal { get; set; }
        [JsonProperty]
        public string phone { get; set; }
        [JsonProperty]
        public string address { get; set; }
        [JsonProperty]
        public string tag { get; set; }
        [JsonProperty]
        public byte[] image { get; set; }
    }
}