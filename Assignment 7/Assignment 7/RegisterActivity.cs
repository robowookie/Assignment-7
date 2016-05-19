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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        EditText et_username, et_email, et_password;
        Button btn_register;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RegisterLyt);

           
            et_username = FindViewById<EditText>(Resource.Id.edttxtUser);
            et_email = FindViewById<EditText>(Resource.Id.edttxtMail);
            et_password = FindViewById<EditText>(Resource.Id.edttxtPass);
            btn_register = FindViewById<Button>(Resource.Id.btn_register);

            
            
            btn_register.Click += Btn_register_Click;
        }

        private async void Btn_register_Click(object sender, EventArgs e)
        {
            // databaseManager dbMan = new databaseManager();
            databaseManager dbMan = new databaseManager();
            kinveyClient = dbMan.buildClient();
            bool success = await dbMan.registerUser(et_username.Text, et_password.Text);
            if (success == true)
            {
                StartActivity(typeof(MainActivity));
            }
        }
    }
}