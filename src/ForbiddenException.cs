using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Web
{
	/// <summary>
	/// Defines an exception to represent the condition of a resource action that is forbidden.
	/// </summary>
	public class ForbiddenException : InvalidOperationException
	{
		/// <summary>
		/// Create a new instance with the provided message.
		/// </summary>
		/// <param name="message"></param>
		public ForbiddenException(string message)
			: base(message)
		{
		}
	}
}
