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
using Android.Util;
using System.Net;
using System.IO;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Assignment_7
{
    public class dataAdapterSearch : BaseAdapter<ReceiptClass>
    {
        List<ReceiptClass> items;

        Activity context;
        public dataAdapterSearch(Activity context, List<ReceiptClass> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override ReceiptClass this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomSearch, null);
            view.FindViewById<TextView>(Resource.Id.tv_business).Text = item.business;
            view.FindViewById<TextView>(Resource.Id.tv_price).Text = item.productTotal.ToString();
            view.FindViewById<TextView>(Resource.Id.tv_name).Text = item.productName;
            view.FindViewById<TextView>(Resource.Id.tv_id).Text = item.id.ToString();
            view.FindViewById<TextView>(Resource.Id.tv_gstNum).Text = item.gstNum;
            view.FindViewById<TextView>(Resource.Id.tv_taxNum).Text = item.taxInvoice;
            view.FindViewById<TextView>(Resource.Id.tv_tagEntry).Text = item.tag;

            //view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(item.image);

            //var imageBitmap = GetImageBitmapFromUrl(item.image);

            //view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageBitmap(imageBitmap);

            return view;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            if (!(url == "null"))
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

            return imageBitmap;
        }
    }
}