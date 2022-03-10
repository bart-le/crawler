using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

		private static List<string> GetEmails(string html, string pattern)
		{
			return Regex.Matches(html, pattern)
				.Cast<Match>()
				.Select(match => match.Value)
				.Distinct()
				.ToList();
		}

		private static void DisplayEmails(List<string> emails)
		{
			if (emails.Count > 0)
			{
				foreach (string email in emails)
					Console.WriteLine(email);
			}
			else
			{
				Console.WriteLine("Nie znaleziono adresów email");
			}
		}

		private static async Task<string> GetURLContent(string url)
		{
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
				throw new ArgumentException("Błedny URL");

			using HttpClient httpClient = new();

			try
			{
				HttpResponseMessage response = await httpClient.GetAsync(url);

				return await response.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException)
			{
				throw new HttpRequestException("Błąd w czasie pobierania strony");
			}
		}
	}
}
