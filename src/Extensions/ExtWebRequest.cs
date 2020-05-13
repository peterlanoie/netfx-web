using System.IO;

// ReSharper disable CheckNamespace
namespace System.Net
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for the System.Net.WebRequest class.
	/// </summary>
	public static class ExtWebRequest
	{
		/// <summary>
		/// Gets the response string of the web request.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static string GetResponseString(this WebRequest request)
		{
			string responseString = null;
			using(var response = request.GetResponse())
			{
				using(var responseStream = response.GetResponseStream())
				{
					if(responseStream != null)
					{
						var reader = new StreamReader(responseStream);
						responseString = reader.ReadToEnd();
						responseStream.Close();
					}
				}
				response.Close();
			}
			return responseString;
		}

	}
}
