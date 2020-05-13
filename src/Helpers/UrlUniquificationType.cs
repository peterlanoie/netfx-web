using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Mvc
{
	/// <summary>
	/// 
	/// </summary>
	public enum UrlUniquificationType
	{
		/// <summary>
		/// Never uniquify the URL. (This is the default.)
		/// </summary>
		Never,

		/// <summary>
		/// Always uniquify the URL.
		/// </summary>
		Always,

		/// <summary>
		/// Uniquify based on the calling assembly version.
		/// Useful for forcing reloads on release changes.
		/// </summary>
		AssemblyVersion,

		/// <summary>
		/// Uniquify URL only #if DEBUG.
		/// </summary>
		OnlyInDebug,
	}
}
