using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Common.Web
{
	/// <summary>
	/// Exposes the Mime Mapping method that Microsoft hid from us.
	/// Adapted from http://www.haneycodes.net/determine-mime-type-from-file-name/
	/// </summary>
	public static class MimeMapper
	{
		// The get mime mapping method info
		private static readonly MethodInfo _getMimeMappingMethod = null;

		/// <summary>
		/// Static constructor sets up reflection.
		/// </summary>
		static MimeMapper()
		{
			// Load hidden mime mapping class and method from System.Web
			var assembly = Assembly.GetAssembly(typeof(HttpApplication));
			Type mimeMappingType = assembly.GetType("System.Web.MimeMapping");
			_getMimeMappingMethod = mimeMappingType.GetMethod("GetMimeMapping",
				BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
				BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		}

		/// <summary>
		/// Exposes the hidden Mime mapping method.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns>The mime mapping.</returns>
		public static string GetMimeMapping(string fileName)
		{
			return (string)_getMimeMappingMethod.Invoke(null /*static method*/, new[] { fileName });
		}
	}
}


