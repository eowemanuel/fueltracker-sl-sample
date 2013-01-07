using System;
using System.Collections.ObjectModel;
using FuelTracker.Web;
using System.Collections.Generic;

namespace FuelTracker.DesignModel
{
	public class DesignToDoItems : ObservableCollection<ToDoItem>
	{
		private const int entitiesCount = 10;
		public DesignToDoItems():this(entitiesCount)
		{
		}
		
		public DesignToDoItems(int entitiesCount)
		{
			var toDoItemsList = GenerateDesignToDoItemsList(entitiesCount);
			foreach (var toDoItem in toDoItemsList)
			{
				this.Add(toDoItem);
			}
		}

		public IList<ToDoItem> GenerateDesignToDoItemsList(int entitiesCount)
		{
			IList<ToDoItem> generatedSource = new List<ToDoItem>();

			for (int i = 2; i < entitiesCount; i++)
			{
				var toDoItem =
				new ToDoItem
				{
					ToDoItemId = i,
					Text = string.Format("Text {0:5}", i),
					Priority = i,
					LocationAddress = string.Format("LocationAddress {0:5}", i),
					LocationXCoordinate = i,
					LocationYCoordinate = i,
					ReminderRange = i,
					//ReminderIsEnabled = null,//TO DO: Update design model assignment of property ReminderIsEnabled on ToDoItem
		};
				generatedSource.Add(toDoItem);
			}

			return generatedSource;
		}

	}
	
}

