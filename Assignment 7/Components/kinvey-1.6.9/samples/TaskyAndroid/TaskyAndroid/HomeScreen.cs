using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using KinveyXamarin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Platform.XamarinAndroid;
using Tasky.Shared;
using TaskyAndroid;
using TaskyAndroid.ApplicationLayer;

namespace TaskyAndroid.Screens 
{
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "Tasky",  
		Icon="@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class HomeScreen : Activity 
	{
		TodoItemListAdapter taskList;
		IList<TodoItem> tasks;
		Button addTaskButton;
		ListView taskListView;
		Client kinveyClient;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.HomeScreen);

			//Find our controls
			taskListView = FindViewById<ListView> (Resource.Id.TaskList);
			addTaskButton = FindViewById<Button> (Resource.Id.AddButton);

			// wire up add task button handler
			if(addTaskButton != null) {
				addTaskButton.Click += (sender, e) => {
					StartActivity(typeof(TodoItemScreen));
				};
			}

			// wire up task click handler
			if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var taskDetails = new Intent (this, typeof (TodoItemScreen));
					taskDetails.PutExtra ("TaskID", tasks[e.Position]._id);
					StartActivity (taskDetails);
				};
			}

			var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			kinveyClient = new Client.Builder ("kid_-k5fJBa5eZ", "11a4659800f341f4a2487cf2feb04cb2")
				.setLogger(delegate(string msg) { Console.WriteLine($"KINVEY{msg}");})
				.setFilePath(path)
				.setOfflinePlatform(new SQLitePlatformAndroid())
				.build();

			TodoItemManager.kinveyClient = kinveyClient;
			TodoItemManager.Init();
			Login();
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

			InitializeAdapter();
		}

		private async Task Login()
		{
			await kinveyClient.User().LoginAsync("testuser", "testpass");
		}

		private async Task InitializeAdapter()
		{
			// get all stored tasks
			//tasks = TodoItemManager.GetTasks();
			tasks = (await TodoItemManager.GetKTasks());

			// create our adapter
			taskList = new TodoItemListAdapter(this, tasks);

			//Hook up our adapter to our ListView
			taskListView.Adapter = taskList;
		}
	}
}