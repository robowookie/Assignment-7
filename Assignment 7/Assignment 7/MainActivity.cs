using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using KinveyXamarin;

namespace Assignment_7
{
    [Activity(Label = "Assignment_7", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        EditText edttxtUser, edttxtPass;
        Button btn_Login, btn_Register;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            databaseManager dbMan = new databaseManager();
            edttxtUser = FindViewById<EditText>(Resource.Id.edttxtUser);
            edttxtPass = FindViewById<EditText>(Resource.Id.edttxtPass);
            btn_Login = FindViewById<Button>(Resource.Id.btn_Login);
            btn_Register = FindViewById<Button>(Resource.Id.btn_Register);

            dbMan.buildClient();

            btn_Login.Click += Btn_Login_Click;
            btn_Register.Click += Btn_Register_Click;
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private async void Btn_Login_Click(object sender, EventArgs e)
        {
            databaseManager dbMan = new databaseManager();
            bool success = await dbMan.loginUser(edttxtUser.Text, edttxtPass.Text);
            if (success == true)
            {
                StartActivity(typeof(ListActivity));
            }
        }
    }
}

