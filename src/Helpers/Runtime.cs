using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Web.Helpers
{
	/// <summary>
	/// Defines helpers for runtime state information.
	/// </summary>
	public static class WebRuntime
	{
		/// <summary>
		/// Gets the current state of request security (SSL/TLS)
		/// </summary>
		/// <param name="request">The request to check.</param>
		/// <param name="offloadHeaders">Optional list of headers that indicate SSL has been handled by an upstream network appliance.</param>
		/// <returns></returns>
		public static SslState GetRequestSecureState(HttpRequest request, params string[] offloadHeaders)
		{
			var result = new SslState();

			// we can set this right away
			result.IsLocalSsl = request.IsSecureConnection;

			// we'll also look for the SSL offload header that could be added by a network appliance
			foreach (var header in offloadHeaders)
			{
				var headerValue = request.Headers[header];
				if (headerValue == null || !bool.Parse(headerValue)) continue;
				result.OffLoadHeader = header;
				result.IsOffLoadSsl = true;
				break;
			}

			return result;
		}

		/// <summary>
		/// Gets whether or not the current request is secure.
		/// </summary>
		public static bool IsSecure(HttpRequest request, params string[] offloadHeaders)
		{
			return GetRequestSecureState(request, offloadHeaders).IsSecure;
		}

		/// <summary>
		/// Returns the scheme of the request, considering the state of security that could include security offload.
		/// </summary>
		public static string GetRequestScheme(HttpRequest request, params string[] offloadHeaders)
		{
			return GetRequestSecureState(request, offloadHeaders).UrlScheme;
		}

	}

	/// <summary>
	/// A snapshot of the state of security of a request.
	/// </summary>
	public class SslState
	{
		/// <summary>
		/// Whether security (SSL/TLS) of the request is being handled locally in IIS.
		/// </summary>
		public bool IsLocalSsl { get; set; }

		/// <summary>
		/// Whether security (SSL/TLS) of the request is being handled by an upstream network appliance.
		/// </summary>
		public bool IsOffLoadSsl { get; set; }

		/// <summary>
		/// When IsOffLoadSsl = true, contains the first header found that indicates security was handled.
		/// </summary>
		public string OffLoadHeader { get; set; }

		/// <summary>
		/// Whether the request is secure or not.
		/// </summary>
		public bool IsSecure { get { return IsLocalSsl || IsOffLoadSsl; } }

		/// <summary>
		/// Gets the url scheme/protocol for this state instance.
		/// </summary>
		public string UrlScheme { get { return IsSecure ? "https" : "http"; } }
	}
}