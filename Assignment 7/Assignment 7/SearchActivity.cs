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

namespace Assignment_7
{
    [Activity(Label = "Activity1")]
    public class SearchActivity : Activity
    {
        databaseManager dbMan;
        EditText et_business, et_tax, et_gst, et_productName, et_productPrice, et_tag;
        Button btn_search;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SearchLyt);

            et_business = FindViewById<EditText>(Resource.Id.et_business);
            et_tax = FindViewById<EditText>(Resource.Id.et_tax);
            et_gst = FindViewById<EditText>(Resource.Id.et_gst);
            et_productName = FindViewById<EditText>(Resource.Id.et_productName);
            et_productPrice = FindViewById<EditText>(Resource.Id.et_productPrice);
            et_tag = FindViewById<EditText>(Resource.Id.et_tag);

            btn_search = FindViewById<Button>(Resource.Id.btn_search);

            btn_search.Click += Btn_search_Click;

        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            string[] transfer = new string[6]{ et_business.Text, et_tax.Text, et_gst.Text, et_productName.Text, et_productPrice.Text, et_tag.Text };

            Intent i = new Intent(this, typeof(ResultActivity));
            //i.PutStringArrayListExtra("transfer", transfer);
            i.PutExtra("business", et_business.Text);
            i.PutExtra("tax", et_tax.Text);
            i.PutExtra("gst", et_gst.Text);
            i.PutExtra("productName", et_productName.Text);
            i.PutExtra("productPrice", et_productPrice.Text);
            i.PutExtra("tag",et_tag.Text);
            StartActivity(i);


        }
    }
}