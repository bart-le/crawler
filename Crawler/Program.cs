using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			try
			{
				string url = args[0];

				string content = await GetURLContent(url);

				if (content != null)
				{
					List<string> emails = GetEmails(content, @"[\w\.-]+@[\w\.-]+\.\w+");

					DisplayEmails(emails);
				}
			}
			catch (IndexOutOfRangeException exception)
			{
				throw new ArgumentNullException("Podaj 1 argument", exception);
			}
		}
	}
}
