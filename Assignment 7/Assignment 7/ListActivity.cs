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
using System.Threading.Tasks;
using KinveyXamarin;

namespace Assignment_7
{
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        Button btn_addNew, btn_search;
        ListView listView1;
        List<ReceiptClass> temp;
        TextView tv_TotalValue;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListLyt);

            dbMan = new databaseManager();
            kinveyClient = dbMan.buildClient();

            btn_addNew = FindViewById<Button>(Resource.Id.button2);
            btn_search = FindViewById<Button>(Resource.Id.button1);
            listView1 = FindViewById<ListView>(Resource.Id.listView1);
            tv_TotalValue = FindViewById<TextView>(Resource.Id.tv_TotalValue);

            temp = await dbMan.getAllItems();

            if (temp.Count == 0)
            {
                Toast.MakeText(this, "There are no records for this account.", ToastLength.Short).Show();
            }

            listView1.Adapter = new dataAdapter(this, temp);

            double total = temp.Sum(x => x.productTotal);

            tv_TotalValue.Text = total.ToString();

            btn_addNew.Click += Btn_addNew_Click;
            btn_search.Click += Btn_search_Click;
            listView1.ItemClick += ListView1_ItemClick;
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SearchActivity));
        }

        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this,typeof(ViewActivity));
            i.PutExtra("id", temp[e.Position].id);
            StartActivity(i);
        }

        private void Btn_addNew_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddActivity));
        }

        protected override void OnDestroy()
        {
            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }
        }

        public override void OnBackPressed()
        {
            bool stopbackkey = true;
            if (stopbackkey)
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Are sure you wish to leave?");
                alert.SetPositiveButton("Yes", (s, ea) =>
                 {
                     StartActivity(typeof(MainActivity));
                 });
                alert.SetNegativeButton("No", (s, ea) =>
                {
                    return;
                });
                alert.Show();
            }
        }
    }
}