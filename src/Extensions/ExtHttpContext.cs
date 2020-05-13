using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

// ReSharper disable CheckNamespace
namespace System.Web
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Provides extensions methods to the standard HttpContext type.
	/// </summary>
	public static class ExtHttpContext
	{
		/// <summary>
		/// Gets an item from the HttpContext.Items collection, creating a new one if none is found.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="context"></param>
		/// <param name="key"></param>
		/// <param name="createIfMissing">Whether to create the item in the context's items collection if it's missing. Default is TRUE.</param>
		/// <returns></returns>
		public static T GetContextItem<T>(this HttpContext context, string key, bool createIfMissing = true) where T : class, new()
		{
			var item = context.Items[key] as T;
			if(item == null && createIfMissing)
			{
				context.Items[key] = item = new T();
			}
			return item;
		}

	}
}
