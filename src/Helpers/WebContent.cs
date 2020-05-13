using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Web.Helpers
{
	/// <summary>
	/// Provides methods for getting web based content.
	/// </summary>
	public class WebContent
	{
		/// <summary>
		/// The body of the response.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The content type of the response.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The length of the content.
		/// </summary>
		public long Length { get; set; }

		/// <summary>
		/// The reponse headers.
		/// </summary>
		public WebHeaderCollection Headers { get; set; }

		/// <summary>
		/// Gets the response body of the specified URI.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public static string GetWebContentString(Uri uri)
		{
			return GetWebContent(WebRequest.Create(uri)).Body;
		}

		/// <summary>
		/// Gets the response body of the specified URL.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GetWebContentString(string url)
		{
			return GetWebContent(WebRequest.Create(url)).Body;
		}

		/// <summary>
		/// Gets the response of the specified URI.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public static WebContent GetWebContent(Uri uri)
		{
			return GetWebContent(WebRequest.Create(uri));
		}

		/// <summary>
		/// Gets the response of the specified URL.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static WebContent GetWebContent(string url)
		{
			return GetWebContent(WebRequest.Create(url));
		}

		/// <summary>
		/// Gets the content returned by the specified web request.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static WebContent GetWebContent(WebRequest request)
		{
			var result = new WebContent();
			var response = request.GetResponse();
			result.Body = GetResponseString(response);
			result.Length = response.ContentLength;
			result.Type = response.ContentType;
			result.Headers = response.Headers;

			return result;
		}

		/// <summary>
		/// Gets the content in the specified web response.
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		public static string GetResponseString(WebResponse response)
		{
			using(var stream = response.GetResponseStream())
			{
				return new StreamReader(stream).ReadToEnd();
			}
		}

	}
}
