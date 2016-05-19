using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Tasky.Shared;
using TaskyAndroid;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskyAndroid.Screens 
{
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "Tasky")]			
	public class TodoItemScreen : Activity 
	{
		TodoItem task = new TodoItem();
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;
		CheckBox doneCheckbox;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			string taskID = Intent.GetStringExtra("TaskID");
			if (!string.IsNullOrEmpty(taskID))
			{
				Get(taskID);
			}
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.TaskDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
			notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = FindViewById<Button>(Resource.Id.SaveButton);

			// TODO: find the Checkbox control and set the value
			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);
			doneCheckbox.Checked = task.Done;

			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);
			
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (string.IsNullOrEmpty(taskID) ? "Cancel" : "Delete");
			
			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		public async Task Get(string ID)
		{
			IList<TodoItem> tasks = (await TodoItemManager.GetKTasks());
			foreach (TodoItem t in tasks)
			{
				if (t._id == ID)
				{
					task = t;
					nameTextEdit.Text = task.Name; 
					notesTextEdit.Text = task.Notes;
					return;
				}
			}
		}

		public async Task Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			//TODO: 
			task.Done = doneCheckbox.Checked;

			await TodoItemManager.SaveKTask(task);
			Finish();
		}

		public async Task CancelDelete()
		{
			if (!string.IsNullOrEmpty(task._id))
			{
				await TodoItemManager.DeleteKTask(task);
			}
			Finish();
		}
	}
}