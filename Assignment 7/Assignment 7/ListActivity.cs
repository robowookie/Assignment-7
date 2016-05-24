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
        Button btn_addNew;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ListLyt);

            btn_addNew = FindViewById<Button>(Resource.Id.button2);

            btn_addNew.Click += Btn_addNew_Click;
        }

        private void Btn_addNew_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddActivity));
        }
    }
}