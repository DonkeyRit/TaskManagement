using System;
using System.Linq;
using System.Text;
using DepartmentEmployee.Model;
using System.Collections.Generic;

namespace DepartmentEmployee.Controllers
{
	public class CheckAssignmentsController
	{
		private readonly List<Task> _tasks;

		public CheckAssignmentsController(List<Task> tasks)
		{
			_tasks = tasks;
		}

		public bool IsCorrect(out List<Task> additionalTasks)
		{
			additionalTasks = new List<Task>();
			bool flag = false;

			IDictionary<DateTime, List<Task>> dictionary = ConvertToSortedDictionary(_tasks).Reverse()
				.ToDictionary(x => x.Key, x => x.Value);

			for(int i = 0; i < dictionary.Count(); i++)
			{
				var current = Aggregate(dictionary.ElementAt(i));
				KeyValuePair<DateTime, Task> next = new KeyValuePair<DateTime, Task>();

				try
				{
					next = Aggregate(dictionary.ElementAt(i + 1));
				}
				catch (ArgumentOutOfRangeException)
				{
					next = new KeyValuePair<DateTime, Task>(DateTime.Now, new Task
					{
						Id = "-1",
						Name = "Default Task",
						endDate = DateTime.Now,
						C1 = 0,
						C2 = 0,
						C3 = 0,
						C4 = 0
					});
					flag = true;
				}

				int workHours = Convert.ToInt32(Math.Round((current.Key - next.Key).TotalDays)) * 8;
				//int workHours = CalculateWorkDays(current.Key - next.Key) * 8;
				int maxComplexity = new[] { current.Value.C1, current.Value.C2, current.Value.C3, current.Value.C4 }.ToList().Max();

				if(workHours < maxComplexity)
				{
					if (flag)
						return false;

					CleanAdditionalTask(workHours, additionalTasks);

					var additionalTask = new Task
					{
						Id = current.Value.Id,
						Name = current.Value.Name,
						endDate = next.Value.endDate,
						C1 = (current.Value.C1 - workHours < 0) ? 0 : (current.Value.C1 - workHours),
						C2 = (current.Value.C2 - workHours < 0) ? 0 : (current.Value.C2 - workHours),
						C3 = (current.Value.C3 - workHours < 0) ? 0 : (current.Value.C3 - workHours),
						C4 = (current.Value.C4 - workHours < 0) ? 0 : (current.Value.C4 - workHours),
					};
					additionalTasks.Add(additionalTask);

					var key = dictionary.Keys.Where(k => k == next.Value.endDate).First();
					dictionary[key].Add(additionalTask);
				}
				else
				{
					additionalTasks.Clear();
				}
			}

			return true;
		}

		private static void CleanAdditionalTask(int workHours, List<Task> tasks)
		{
			for(int i = 0, n = tasks.Count; i < n ; i++)
			{
				int maxComplexity = new[] { tasks[i].C1, tasks[i].C2, tasks[i].C3, tasks[i].C4 }.ToList().Max();
				if(maxComplexity < workHours)
				{
					tasks.Remove(tasks[i]);
					workHours -= maxComplexity;
				}
			}
		}
		private static KeyValuePair<DateTime, Task> Aggregate(KeyValuePair<DateTime, List<Task>> keyValue)
		{
			var list = keyValue.Value;
			DateTime endDateTemp = DateTime.MinValue;
			StringBuilder idBuilder = new StringBuilder(),
				nameBuilder = new StringBuilder();

			int c1 = 0, c2 = 0, c3 = 0, c4 = 0;

			for(int i = 0, n = list.Count; i < n; i++)
			{
				var currentTask = list[i];

				if (!currentTask.Id.Contains("["))
				{
					idBuilder.Append("[").Append(currentTask.Id).Append("]");
					nameBuilder.Append("[").Append(currentTask.Name).Append("]");
				}

				endDateTemp = currentTask.endDate;

				c1 += currentTask.C1;
				c2 += currentTask.C2;
				c3 += currentTask.C3;
				c4 += currentTask.C4;
			}

			var result = new KeyValuePair<DateTime, Task>(keyValue.Key, new Task
			{
				Id = idBuilder.ToString(),
				Name = nameBuilder.ToString(),
				endDate = endDateTemp,
				C1 = c1,
				C2 = c2,
				C3 = c3,
				C4 = c4
			});

			return result;
		}
		private static SortedDictionary<DateTime, List<Task>> ConvertToSortedDictionary(List<Task> list)
		{
			var dictionary = new SortedDictionary<DateTime, List<Task>>();

			for (int i = 0, n = list.Count; i < n; i++)
			{
				var dateTime = list[i].endDate;
				var currentTask = list[i];

				if (dictionary.ContainsKey(dateTime))
				{
					var key = dictionary.Keys.Where(k => k == dateTime).First();
					dictionary[key].Add(currentTask);
				}
				else
				{
					dictionary.Add(dateTime, new List<Task>{ currentTask });
				}

			}

			return dictionary;
		}
		private static int CalculateWorkDays(DateTime fromDate, DateTime toDate)
		{
			DateTime temp = fromDate;
			int count = 0;

			while (temp != toDate)
			{
				if (temp.DayOfWeek != DayOfWeek.Saturday &&
					temp.DayOfWeek != DayOfWeek.Sunday)
					count++;

				temp = temp.AddDays(1);
			}

			return count;
		}
	}
}
