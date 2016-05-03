using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Fenetre.Sitefinity.TwitterFeed
{
    /// <summary>
    /// This class holds several utility methods for the TwitterFeed.
    /// </summary>
	public static class Extensions
	{
		private static ResourceManager resourceManager = new ResourceManager(typeof(Extensions));

        /// <summary>
        /// Gets the DateTime and formats it into a PrettyDate
        /// </summary>
        /// <param name="date">Date to be formatted</param>
        /// <returns>A string containing the PrettyDate</returns>
		public static string PrettyDate(this DateTime date)
		{
			var today = DateTime.Now;
			var yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

			if (date.DayOfYear == today.DayOfYear && date.Year == today.Year)
			{
				return resourceManager.GetString("PrettyDateToday");
			}

			if (date.DayOfYear == yesterday.DayOfYear && date.Year == yesterday.Year)
			{
				return resourceManager.GetString("PrettyDateYesterday");
			}

			return date.ToString(resourceManager.GetString("PrettyDateFormat"));
		}

        /// <summary>
        /// Gets the DateTime relative to the Coordinated Universal Time and
        /// formats it into an expanded PrettyDate.
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
		public static string PrettyDateTodayExpanded(this DateTimeOffset dateTimeOffset)
		{
			return PrettyDateTodayExpanded(dateTimeOffset.ToLocalTime().DateTime);
		}

        /// <summary>
        /// Formats the date into an expanded PrettyDate.
        /// </summary>
        /// <param name="date">Date to be formatted</param>
        /// <returns>A string containing the expanded PrettyDate</returns>
		public static string PrettyDateTodayExpanded(this DateTime date)
		{
			var today = DateTime.Now;
			var yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));

			if (date.DayOfYear == today.DayOfYear && date.Year == today.Year)
			{
				var timeDifference = today - date;
				string createdAtUnitText = String.Empty;
				if (timeDifference.Hours > 0)
				{
					string format;
					if (timeDifference.Hours == 1)
					{
						format = resourceManager.GetString("PrettyDateOneHour");
					}
					else
					{
						format = resourceManager.GetString("PrettyDateHours");
					}
					createdAtUnitText = String.Format(format, timeDifference.Hours);
				}
				else if (timeDifference.Minutes > 0)
				{
					string format;
					if (timeDifference.Minutes == 1)
					{
						format = resourceManager.GetString("PrettyDateOneMinute");
					}
					else
					{
						format = resourceManager.GetString("PrettyDateMinutes");
					}
					createdAtUnitText = String.Format(format, timeDifference.Minutes);
				}
				else
				{
					createdAtUnitText = resourceManager.GetString("PrettyDateSeconds");
				}
				return createdAtUnitText + " " + resourceManager.GetString("PrettyDateAgo");
			}

			if (date.DayOfYear == yesterday.DayOfYear && date.Year == yesterday.Year)
			{
				return resourceManager.GetString("PrettyDateYesterday");
			}

			return date.ToString(resourceManager.GetString("PrettyDateFormat"));
		}
	}
}
