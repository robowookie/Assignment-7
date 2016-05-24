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

namespace Assignment_7
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        databaseManager dbMan;
        Client kinveyClient;
        Button btnSave;
        ImageView imageView1;
        DatePicker dpicker;
        EditText editxtUsNam, editxtBsinss, editText1, editxtGST, editxtServ, 
            editxtProdcNam, editxtCost, editxtPhNum, editxtAddrs, editxtTag;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SaveRecordLyt);

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            dpicker = FindViewById<DatePicker>(Resource.Id.datePicker1);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            editxtUsNam = FindViewById<EditText>(Resource.Id.editxtUsNam);
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

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Bitmap b = BitmapFactory.DecodeResource(imageView1.Context.Resources, Resource.Drawable.Icon);

            imageView1.BuildDrawingCache(true);
            Bitmap myb = Bitmap.CreateBitmap(b.Width, b.Height, b.GetConfig());

            MemoryStream stream = new MemoryStream();
            myb.Compress(Bitmap.CompressFormat.Png, 0, stream);
            byte[] mybyte = stream.ToArray();

            dbMan.addItem(editxtUsNam.Text, editxtBsinss.Text, dpicker.DateTime, editText1.Text, editxtGST.Text, editxtServ.Text,
            editxtProdcNam.Text, Convert.ToDouble(editxtCost.Text), editxtPhNum.Text, editxtAddrs.Text, editxtTag.Text, mybyte);
        }
    }
}