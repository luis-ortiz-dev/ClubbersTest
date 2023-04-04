using System;
namespace PruebaXamarinLuisOrtiz.Models
{
	public class TaskModel
	{
		public LiteDB.ObjectId _id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public bool IsCompleted { get; set; }
	}
}

