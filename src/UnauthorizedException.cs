using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Common.Web
{
	/// <summary>
	/// Defines an exception to represent the condition of unauthorized access.
	/// </summary>
	public class UnauthorizedException : HttpException
	{
		/// <summary>
		/// Create a new instance with the provided message.
		/// </summary>
		/// <param name="message"></param>
		public UnauthorizedException(string message)
			: base(401, message)
		{
		}
	}
}
