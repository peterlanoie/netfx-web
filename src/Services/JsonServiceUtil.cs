using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Common.Web.Services
{
	/// <summary>
	/// Defines utility methods for handling JSON data type requests.
	/// </summary>
	public static class JsonServiceUtil
	{
		/// <summary>
		/// Gets a provided object type from a web endpoint that returns JSON data.
		/// </summary>
		/// <typeparam name="TResponse">The object type to which the returned JSON should be mapped.</typeparam>
		/// <param name="url">The URL to a JSON service endpoint.</param>
		/// <returns></returns>
		public static TResponse GetWebResponseObject<TResponse>(string url) where TResponse : class
		{
			var request = WebRequest.Create(url);// as HttpWebRequest
			return GetWebResponseObject<TResponse>(request);
		}

		/// <summary>
		/// Gets a specified object type instance from a web request to a web endpoint that returns JSON data.
		/// </summary>
		/// <typeparam name="TResponse">The object type to which the returned JSON should be mapped.</typeparam>
		/// <param name="request">The web request to make.</param>
		/// <returns></returns>
		public static TResponse GetWebResponseObject<TResponse>(WebRequest request) where TResponse : class
		{
			var result = request.GetResponseString();
			return result != null ? JsonConvert.DeserializeObject<TResponse>(result) : null;
		}

		/// <summary>
		/// Posts an object as JSON to the provided URL, returning the response string.
		/// </summary>
		/// <param name="url">The URL to which the object is posted.</param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string PostObject(string url, object obj) //, RequestOptions options = null
		{
			string responseString = null;
			var request = (HttpWebRequest)WebRequest.Create(url);

			//if(options == null)
			//{
			//    options = new RequestOptions();
			//}

			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";

			var payload = JsonConvert.SerializeObject(obj);

			using(var writer = new StreamWriter(request.GetRequestStream()))
			{
				writer.Write(payload);
				writer.Flush();
			}

			responseString = request.GetResponseString();
			return responseString;
		}

		/// <summary>
		/// added overloaded method to pass in header auth token
		/// added code to send an array of json objects for Lumina
		/// gba - 2/10/2017
		/// </summary>
		/// <param name="url"></param>
		/// <param name="jsonPayload"></param>
		/// <param name="authToken"></param>
		/// <returns></returns>
		public static string PostObject(string url, string jsonPayload, string authToken) //, RequestOptions options = null
		{
			string responseString = null;
			var request = (HttpWebRequest)WebRequest.Create(url);

			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";
			request.Headers.Add("Authorization", authToken);  // gba

			//var objectArray = new List<object>();
			//objectArray.Add(obj);
			//var payload = JsonConvert.SerializeObject(objectArray);

			using(var writer = new StreamWriter(request.GetRequestStream()))
			{
				writer.Write(jsonPayload);
				writer.Flush();
			}

			responseString = request.GetResponseString();
			return responseString;
		}

		/// <summary>
		/// Posts an object as JSON to the provided URL, returning the response as a typed object.
		/// </summary>
		/// <typeparam name="TResult">Service response as a strong type.</typeparam>
		/// <param name="url"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static TResult PostObject<TResult>(string url, object obj)
		{
			return JsonConvert.DeserializeObject<TResult>(PostObject(url, obj));
		}

	}

	//public class RequestOptions
	//{
	//}
}
