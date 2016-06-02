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
using SQLite.Net.Platform.XamarinAndroid;
using System.Threading.Tasks;

namespace Assignment_7
{
    class databaseManager
    {
        Client kinveyClient;
        string appKey = "kid_WkEykZF0GZ";
        string appSecret = "68a5e8df969642d9b0050d9995dbca28";
        string tblName = "ReceiptTbl";

        public Client buildClient()
        {
            try
            {
                kinveyClient = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
            .setOfflinePlatform(new SQLitePlatformAndroid())
            .build();

                return kinveyClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void isLoggedIn()
        {
            try
            {
                if (kinveyClient.User().isUserLoggedIn())
                {
                    kinveyClient.User().Logout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async void addItem(string business, DateTime dateAndTime, string taxInvoice,
            string gstNum, string serverName, string productName, double productTotal, string phone,
            string address, string tag, byte[] image)
        {
            try
            {
                ReceiptClass item = new ReceiptClass();

                item.user = kinveyClient.ClientUsers.CurrentUser;
                item.business = business;
                item.dateAndTime = dateAndTime;
                item.taxInvoice = taxInvoice;
                item.gstNum = gstNum;
                item.serverName = serverName;
                item.productName = productName;
                item.productTotal = productTotal;
                item.phone = phone;
                item.address = address;
                item.tag = tag;
                item.image = image;

                AsyncAppData<ReceiptClass> myBook = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                ReceiptClass saved = await myBook.SaveAsync(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async void editItem(string editID, ReceiptClass newList)
        {
            try
            {
                AsyncAppData<ReceiptClass> myBook = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                ReceiptClass editList = await myBook.GetEntityAsync(editID);

                editList = newList;

                ReceiptClass saved = await myBook.SaveAsync(editList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public async void deleteItem(string editID)
        {
            try
            {
                if (editID != "")
                {
                    AsyncAppData<ReceiptClass> myBook = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                    await myBook.DeleteAsync(editID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public async Task<List<ReceiptClass>> getAllItems()
        {
            try
            {
                List<ReceiptClass> templist = new List<ReceiptClass>();
                AsyncAppData<ReceiptClass> item = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                ReceiptClass[] itemList = await item.GetAsync();

                templist = itemList.Where(p => p.user == kinveyClient.ClientUsers.CurrentUser).ToList();

                return templist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            
        }

        public async Task<List<ReceiptClass>> getOneItem(string ID)
        {
            try
            {
                List<ReceiptClass> templist = new List<ReceiptClass>();
                AsyncAppData<ReceiptClass> item = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                ReceiptClass[] itemList = await item.GetAsync();

                templist = itemList.Where(p => p.user == kinveyClient.ClientUsers.CurrentUser && p.id == ID).ToList();

                return templist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<ReceiptClass>> searchItems(string business, string tax, string gst, string productName, double productPrice, string tag)
        {
            try
            {


                //List<ReceiptClass> templist = new List<ReceiptClass>();
                //AsyncAppData<ReceiptClass> item = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                //ReceiptClass[] itemList = await item.GetAsync();

                //templist = itemList.Where(p => p.user == kinveyClient.ClientUsers.CurrentUser).ToList();

                //return templist;
                List<ReceiptClass> templist = new List<ReceiptClass>();

                AsyncAppData<ReceiptClass> item = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                ReceiptClass[] itemList = await item.GetAsync();

                templist = itemList.Where(p => p.user == kinveyClient.ClientUsers.CurrentUser)
                    .Where(x => string.IsNullOrEmpty(x.business) || x.business == business)
                        //.Where(x => string.IsNullOrEmpty(x.taxInvoice) || x.taxInvoice == tax)
                        //.Where(x => string.IsNullOrEmpty(x.gstNum) || x.gstNum == gst)
                        //.Where(x => string.IsNullOrEmpty(x.productName) || x.productName == productName)
                        //.Where(x => x.productTotal == 0 || x.productTotal == productPrice)
                        //.Where(x => string.IsNullOrEmpty(x.tag) || x.tag == tag)
                        .ToList();

                return templist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> loginUser(string username, string password)
        {
            if (username != "" && password != "")
            {
                try
                {
                    User user = await kinveyClient.User().LoginAsync(username, password);
                    return true;
                }
                catch (Exception ex)
                {
                    string temp = ex.Message.ToString();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> registerUser(string username, string password)
        {
            if (username != "" && password != "")
            {
                try
                {
                    User myUser = await kinveyClient.User().CreateAsync(username, password);
                    return true;
                }
                catch (Exception ex)
                {

                    string temp = ex.Message.ToString();
                    Console.WriteLine("TESTING REGISTRATION: " + temp);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}