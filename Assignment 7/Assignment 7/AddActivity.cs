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
using System.Drawing;
using Android.Graphics;
using System.IO;
using Android.Provider;

namespace Assignment_7
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        Button btnSave, btnAddPht;
        ImageView imageView1;
        DatePicker dpicker;
        EditText editxtUsNam, editxtBsinss, editText1, editxtGST, editxtServ, 
            editxtProdcNam, editxtCost, editxtPhNum, editxtAddrs, editxtTag;
        //string _imageUri;
        Intent intent;
        Bitmap bitmap;
        byte[] bitmapData;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SaveRecordLyt);

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnAddPht = FindViewById <Button>(Resource.Id.btnAddPht);
            dpicker = FindViewById<DatePicker>(Resource.Id.datePicker1);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            editxtBsinss = FindViewById<EditText>(Resource.Id.editxtBsinss);
            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            editxtGST = FindViewById<EditText>(Resource.Id.editxtGST);
            editxtServ = FindViewById<EditText>(Resource.Id.editxtServ);
            editxtProdcNam = FindViewById<EditText>(Resource.Id.editxtProdcNam);
            editxtCost = FindViewById<EditText>(Resource.Id.editxtCost);
            editxtPhNum = FindViewById<EditText>(Resource.Id.editxtPhNum);
            editxtAddrs = FindViewById<EditText>(Resource.Id.editxtAddrs);
            editxtTag = FindViewById<EditText>(Resource.Id.editxtTag);

            btnSave.Click += BtnSave_Click;
            btnAddPht.Click += BtnAddPht_Click;

        }

        private void BtnAddPht_Click(object sender, EventArgs e)
        {
            var uri = ContentResolver.Insert(isMounted
                                             ? MediaStore.Images.Media.ExternalContentUri
                                             : MediaStore.Images.Media.InternalContentUri, new ContentValues());
            //_imageUri = uri.ToString();
            intent = new Intent(MediaStore.ActionImageCapture);
            intent.PutExtra(MediaStore.ExtraOutput, uri);
            StartActivityForResult(intent, 1001);
        }

        private Boolean isMounted
        {
            get
            {
                return Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted);
            }
        }

            protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 1001)
            {
                var bitmap = (Bitmap)data.Extras.Get("data");
                //string _imageUri = Intent.GetStringExtra("test");
                //Android.Net.Uri _currentImageUri = Android.Net.Uri.Parse(_imageUri);
                //bitmap = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(_currentImageUri));

                imageView1.SetImageBitmap(bitmap);

                bitmapData = null;

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            dbMan = new databaseManager();

            //Bitmap b = BitmapFactory.DecodeResource(imageView1.Context.Resources, Resource.Drawable.Icon);

            //imageView1.BuildDrawingCache(true);
            //Bitmap myb = Bitmap.CreateBitmap(b.Width, b.Height, b.GetConfig());

            kinveyClient = dbMan.buildClient();

            double price;

            if (editxtCost.Text == "")
            {
                price = 0;
            }
            else
            {
                price = Convert.ToDouble(editxtCost.Text);
            }

            dbMan.addItem(editxtBsinss.Text, dpicker.DateTime, editText1.Text, editxtGST.Text, editxtServ.Text,
            editxtProdcNam.Text, price, editxtPhNum.Text, editxtAddrs.Text, editxtTag.Text, bitmapData);

            //bitmap.Dispose();

            StartActivity(typeof(ListActivity));
        }

        //protected override void OnDestroy()
        //{
        //    if (kinveyClient.User().isUserLoggedIn())
        //    {
        //        kinveyClient.User().Logout();
        //    }
        //}
    }
}