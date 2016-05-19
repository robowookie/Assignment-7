using System;
using System.Collections.Generic;

using KinveyXamarin;
using System.Threading.Tasks;

namespace Tasky.Shared 
{
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public class TodoItemManager 
	{
		// add kinveyClient object
		public static Client kinveyClient { get; set; }
		public static AsyncAppData<TodoItem> kinveyStore;

		public static void Init()
		{
			if (kinveyStore == null)
			{
				kinveyStore = kinveyClient.AppData<TodoItem> ("todo", typeof(TodoItem));
			}
		}

		// NEW KINVEY METHOD TO GET TASKS
		public async static Task<IList<TodoItem>> GetKTasks ()
		{
			return new List<TodoItem>(await kinveyStore.GetAsync());
		}

		// NEW KINVEY METHOD TO SAVE TASK
		public async static Task SaveKTask(TodoItem item)
		{
			await kinveyStore.SaveAsync(item);
		}

		// NEW KINVEY METHOD TO DELETE TASK
		public async static Task DeleteKTask(TodoItem item)
		{
			await kinveyStore.DeleteAsync(item._id);
		}
	}
}