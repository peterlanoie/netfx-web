using System.Web;

namespace Common.Web.Helpers
{
	/// <summary>
	/// Defines methods for helper build web paths.
	/// </summary>
	public class WebPathHelper
	{
		/// <summary>
		/// Creates a fully qualified path to a web resource.
		/// Uses the current request's host and protocol.
		/// </summary>
		/// <param name="rootRelativeUrl">The path portion of a URL. Must be root relative (including a leading /).</param>
		/// <returns></returns>
		public static string BuildCompleteUrl(string rootRelativeUrl)
		{
			return BuildCompleteUrl(rootRelativeUrl, null, null);
		}

		/// <summary>
		/// Creates a fully qualified path to a web resource using the specified URL scheme.
		/// Uses the current request's hostname and/or url scheme for each that is null.
		/// If but hostname and url scheme are passed, no current request is used so the method can be used outside the scope of an active HTTP request.
		/// </summary>
		/// <param name="rootRelativeUrl">The path portion of a URL. Must be root relative (including a leading /).</param>
		/// <param name="hostname">The host name for the URL. Uses the current request's scheme if null.</param>
		/// <param name="urlScheme">The scheme to use in the generated URL. Uses the current request scheme if null.</param>
		/// <returns></returns>
		public static string BuildCompleteUrl(string rootRelativeUrl, string hostname = null, string urlScheme = null)
		{
			HttpRequest request = null;
			if (hostname == null || urlScheme == null)
			{
				request = HttpContext.Current.Request;
				hostname = hostname ?? request.ServerVariables["HTTP_HOST"];
				urlScheme = urlScheme ?? request.Url.Scheme;
			}

			return string.Format("{0}://{1}{2}", urlScheme, hostname, rootRelativeUrl);
		}
	}
}
