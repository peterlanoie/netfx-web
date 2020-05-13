using System;
using System.Web;

namespace Common.Web
{
	/// <summary>
	/// Defines an exception to represent the condition of a resource not found.
	/// </summary>
	public class NotFoundException : HttpException
	{
		/// <summary>
		/// Create a new instance with the provided message.
		/// </summary>
		/// <param name="message"></param>
		public NotFoundException(string message)
			: base(404, message)
		{
		}
	}
}