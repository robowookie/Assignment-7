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
using Android.Graphics;

namespace Assignment_7
{
    [Activity(Label = "ViewActivity")]
    public class ViewActivity : Activity
    {
        List<ReceiptClass> templist;
        Button btn_Edit, btn_Delete;
        DatePicker dpicker;
        EditText editxtBsinss, editText1, editxtGST, editxtServ,
            editxtProdcNam, editxtCost, editxtPhNum, editxtAddrs, editxtTag;
        ImageView imageView1;
        databaseManager dbMan;
        Client kinveyClient;
        string ID;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EditRecordLyt);

            dbMan = new databaseManager();
            kinveyClient = dbMan.buildClient();

            btn_Edit = FindViewById<Button>(Resource.Id.btn_Edit);
            btn_Delete = FindViewById<Button>(Resource.Id.btn_Delete);
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

            ID = Intent.GetStringExtra("id");

            templist = await dbMan.getOneItem(ID);

            editxtBsinss.Text = templist[0].business;
            editText1.Text = templist[0].taxInvoice;
            editxtGST.Text = templist[0].gstNum;
            editxtServ.Text = templist[0].serverName;
            editxtProdcNam.Text = templist[0].productName;
            editxtCost.Text = templist[0].productTotal.ToString();
            editxtPhNum.Text = templist[0].phone;
            editxtAddrs.Text = templist[0].address;
            editxtTag.Text = templist[0].tag;

            imageView1.SetImageBitmap(bytesToBitmap(templist[0].image));

            btn_Edit.Click += Btn_Edit_Click;
            btn_Delete.Click += Btn_Delete_Click;
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            dbMan.deleteItem(ID);

            StartActivity(typeof(ListActivity));
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            ReceiptClass newList = new ReceiptClass();

            newList.id = ID;
            newList.user = templist[0].user;
            newList.business = editxtBsinss.Text;
            newList.taxInvoice = editText1.Text;
            newList.gstNum = editxtGST.Text;
            newList.serverName = editxtServ.Text;
            newList.productName = editxtProdcNam.Text;
            newList.productTotal = Convert.ToDouble(editxtCost.Text);
            newList.phone = editxtPhNum.Text;
            newList.address = editxtAddrs.Text;
            newList.tag = editxtTag.Text;

            dbMan.editItem(ID, newList);

            StartActivity(typeof(ListActivity));
        }

        protected override void OnDestroy()
        {
            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }
        }

        public static Bitmap bytesToBitmap(byte[] imageBytes)
        {

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

            return bitmap;
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(ListActivity));
        }
    }
}