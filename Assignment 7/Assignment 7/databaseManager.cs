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
            kinveyClient = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
            .setOfflinePlatform(new SQLitePlatformAndroid())
            .build();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }

            return kinveyClient;
        }

        public async void addItem(string user, string business, DateTime dateAndTime, string taxInvoice,
            string gstNum, string serverName, string productName, double productTotal, string phone,
            string address, string tag, byte[] image)
        {
            ReceiptClass item = new ReceiptClass();

            item.user = user;
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

        public async void editItem(string editID, ReceiptClass newList)
        {
            AsyncAppData<ReceiptClass> myBook = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
            ReceiptClass editList = await myBook.GetEntityAsync(editID);

            editList = newList;

            ReceiptClass saved = await myBook.SaveAsync(editList);
        }

        public async void deleteItem(string editID)
        {
            if (editID != "")
            {
                AsyncAppData<ReceiptClass> myBook = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
                await myBook.DeleteAsync(editID);
            }
        }

        public async Task<List<ReceiptClass>> getAllItems()
        {
            List<ReceiptClass> templist = new List<ReceiptClass>();
            AsyncAppData<ReceiptClass> item = kinveyClient.AppData<ReceiptClass>(tblName, typeof(ReceiptClass));
            ReceiptClass[] itemList = await item.GetAsync();

            templist = itemList.Where(p => p.user == kinveyClient.ClientUsers.CurrentUser).ToList();

            return templist;
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