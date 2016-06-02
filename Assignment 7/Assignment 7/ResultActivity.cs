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
using KinveyXamarin;

namespace Assignment_7
{
    [Activity(Label = "ResultActivity")]
    public class ResultActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        string business, tax, gst, productName, productPrice, tag;
        double productTotal;

        ListView listView1;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SearchResult);

            listView1 = FindViewById<ListView>(Resource.Id.listView1);

            dbMan = new databaseManager();
            kinveyClient = dbMan.buildClient();

            string[] input = new string[6];
            List<ReceiptClass> templist = new List<ReceiptClass>();

            business = Intent.GetStringExtra("business");
            tax = Intent.GetStringExtra("tax");
            gst = Intent.GetStringExtra("gst");
            productName = Intent.GetStringExtra("productName");
            productPrice = Intent.GetStringExtra("productPrice");
            tag = Intent.GetStringExtra("tag");

            if (productPrice != "")
            {
                productTotal = Convert.ToDouble(productPrice);
            }
            
            //input = Intent.GetStringArrayExtra("transfer").ToArray();

            //double price = Convert.ToDouble(input[4]);

            //templist = await dbMan.searchItems(input[0].ToString(), input[1].ToString(), input[2].ToString(), input[3].ToString(), price, input[5].ToString());
            templist = await dbMan.searchItems(business, tax, gst, productName, productTotal, tag);

            if (templist.Count == 0)
            {
                Toast.MakeText(this, "There are no records for this account.", ToastLength.Short).Show();
            }

            listView1.Adapter = new dataAdapter(this, templist);
        }
    }
}