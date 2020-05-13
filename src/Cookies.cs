using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Web
{

	/// <summary>
	/// Provides helper methods for cookie operations.
	/// </summary>
	public static class Cookies
	{

		/// <summary>
		/// Tests whether a cookie exists with the specified value.
		/// A missing cookie, or any cookie with a value not matching the provided value will return false.
		/// </summary>
		/// <param name="cookieName">Name of the cookie to check.</param>
		/// <param name="value">Expected value of the cookie.</param>
		/// <returns></returns>
		public static bool CookieHasValue(string cookieName, string value)
		{
			var cookie = HttpContext.Current.Request.Cookies[cookieName];
			if(cookie != null)
			{
				return cookie.Value == value;
			}
			return false;
		}

		/// <summary>
		/// Compares the provided value to the value of the cookieName cookie using type T's CompareTo method.
		/// If it matches, returns true.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cookieName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool CookieIsValue<T>(string cookieName, T value) where T : IComparable
		{
			var comparable = (IComparable) value;
			var cookie = HttpContext.Current.Request.Cookies[cookieName];
			if(cookie == null) return false;
			return comparable.CompareTo(cookie.Value) == 0;
		}
	}
}
