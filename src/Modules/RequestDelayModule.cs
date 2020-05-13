using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Threading;

namespace Common.Web.Modules
{
	/// <summary>
	/// Defines an HTTP module that can be used to introduce a fixed or random delay of the default or a 
	/// defined number of milliseconds to all HTTP requests to the web application in which the module is included.
	/// </summary>
	public class RequestDelayModule : IHttpModule
	{
		private RequestDelaySection _config;

		#region IHttpModule Members

		/// <summary>
		/// Disposes the module.
		/// </summary>
		public void Dispose()
		{
		}

		/// <summary>
		/// Initializes the module.
		/// Binds this modules behavior to the BeginRequest event.
		/// </summary>
		/// <param name="context"></param>
		public void Init(HttpApplication context)
		{
			_config = (RequestDelaySection)ConfigurationManager.GetSection("requestDelay");
			context.BeginRequest += new EventHandler(context_BeginRequest);
		}

		void context_BeginRequest(object sender, EventArgs e)
		{
			int intSleepMS = 0;
			switch(_config.DelayType)
			{
				case RequestDelayType.None:
					break;
				case RequestDelayType.Fixed:
					intSleepMS = _config.FixedDelayTime;
					break;
				case RequestDelayType.Random:
					intSleepMS = new Random().Next(Math.Min(_config.MinDelayTime, _config.MaxDelayTime), Math.Max(_config.MinDelayTime, _config.MaxDelayTime));
					break;
			}
			if(intSleepMS > 0)
			{
				Thread.Sleep(intSleepMS);
			}
		}

		#endregion
	}
}
