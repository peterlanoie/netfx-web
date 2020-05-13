using System;
using System.Linq;
using System.Reflection;
using Common.Mvc;

namespace Common.Web.Helpers
{
	/// <summary>
	/// Providers helper methods for creating unique URLs.
	/// </summary>
	public static class UrlUniquifier
	{
		/// <summary>
		/// Adds a string to the URL to enforce uniqueness based on the <paramref name="uniquification"/> rule provided.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="uniquification"></param>
		/// <returns></returns>
		public static string UniquifyUrl(string url, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			var uniquify = uniquification != UrlUniquificationType.Never;
#if DEBUG
			if(uniquification == UrlUniquificationType.OnlyInDebug)
			{
				uniquify = true;
				uniquification = UrlUniquificationType.Always;
			}
#endif
			if(uniquify)
			{
				string uniquifier;
				if(uniquification == UrlUniquificationType.AssemblyVersion)
				{
					var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
					uniquifier = assembly.GetName().Version.ToString(4);
				}
				else
				{
					uniquifier = DateTime.Now.Ticks.ToString();
				}
				url = String.Format("{0}{1}v{2}", url, url.Contains('?') ? "&" : "?", uniquifier);
			}
			return url;
		}

	}
}