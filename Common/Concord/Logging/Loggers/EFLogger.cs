using System;
using System.Web;
using System.Linq;
using Concord.Logging.Exceptions;
using Concord.Logging.Models;

namespace Concord.Logging.Loggers
{
	public class EFLogger : ILogger
	{
		/// <summary>
		/// details about the application used when logging
		/// </summary>
		public ApplicationDetails ApplicationDetails { get; set; }

		/// <summary>
		/// unique identifier to associate all log entries made during the current request
		/// </summary>
		public Guid RequestID
		{
			get
			{
				// TODO: get this from DI (simpleinjector?)
				// TODO: add MVC stuff for HttpContextBase instead of HttpContext

				var guid = Guid.NewGuid();
				if (HttpContext.Current != null && HttpContext.Current.Items["ConcordRequestID"] != null)
					guid = (Guid) HttpContext.Current.Items["ConcordRequestID"];
				else if (HttpContext.Current != null)
					HttpContext.Current.Items["ConcordRequestID"] = guid;
				return guid;
			}
		}


		public EFLogger(ApplicationDetails details)
		{
			ApplicationDetails = details;
		}

		
		/// <summary>
		/// configures children elements with matches from the database to remove duplicates
		/// </summary>
		/// <param name="db">the database context</param>
		private Request ConfigureRequest(LogContext db)
		{
			var client = new Client();
			var host = new Host();
			var environment = new Models.Environment(ApplicationDetails.Environment);

			var app = new Application
			{
				Name = ApplicationDetails.ApplicationName,
				Version = ApplicationDetails.ApplicationVersion,
				Environment = environment
			};

			// add environment if it's not in the db
			var env = (from e in db.Environments
									  where e.Name == environment.Name
									  select e).FirstOrDefault();
			if (env == null)
			{
				db.Environments.Add(environment);
			}

			// add client details if it's not in the db
			var match = (from m in db.Clients
							where m.Name == client.Name
								&& m.UserAgent == client.UserAgent
								&& m.Browser.Name == client.Browser.Name
							select m).FirstOrDefault();
			if (match == null)
			{
				db.Clients.Add(client);
			}
			else
			{
				client = match;
			}

			// look for matching browser
			var browser = (from b in db.Browsers
							   where b.Name == client.Browser.Name
									&& b.FullVersion == client.Browser.FullVersion
							   select b).FirstOrDefault();
			if (browser != null)
				client.Browser = browser;


			// get host details
			var hostMatch = (from h in db.Hosts
							  where h.Name == host.Name
								&& h.OSVersion == host.OSVersion
							  select h).FirstOrDefault();
			if (hostMatch == null)
			{
				db.Hosts.Add(host);
			}
			else
			{
				host = hostMatch;
			}

			// now check if app exists
			var appMatch = (from a in db.Applications
									where a.Name == app.Name
										&& a.Version == app.Version
										&& a.Environment.Name == app.Environment.Name
									select a).FirstOrDefault();
			if (appMatch == null)
			{
				db.Applications.Add(app);
			}
			else
			{
				app = appMatch;
			}

			// now add request
			var request = new Request { Application = app, Host = host, Client = client };
			var reqMatch = (from r in db.Requests
								where r.Application.ID == app.ID
									&& r.Client.ID == client.ID
									&& r.Host.ID == host.ID
								select r).FirstOrDefault();
			if (reqMatch == null)
			{
				db.Requests.Add(request);
			}
			else
			{
				request = reqMatch;
			}

			return request;
		}



		public void Log(Exception ex)
		{
			try
			{
				using (var db = new LogContext())
				{
					var request = ConfigureRequest(db);

					var log = new ErrorLog
						{
							Message = ex.Message,
							Request = request,
							SessionToken = "168895.9998398.908983",
							SystemGuid = RequestID,
							FullMessage = ex.ToString(),
							Type = ex.GetType().ToString()

						};
					db.ErrorLogs.Add(log);
					db.SaveChanges();
				}
			}
			catch (Exception)
			{
				// what the heck do we do when logging an error errors?
			}
		}

		public void Log(string message)
		{
			Log(new ConcordException(message));
		}

		public void LogXml(string xml, bool isRequest = false)
		{
			try
			{
				using (var db = new LogContext())
				{
					var request = ConfigureRequest(db);

					var log = new XmlLog
					{
						Message = "Xml record",
						Request = request,
						SessionToken = "8675309",
						SystemGuid = RequestID,
						Username = "jmcruz",
						Xml = xml,
						Type = isRequest ? "Request" : "Response"
					};
					db.XmlLogs.Add(log);
					db.SaveChanges();
				}
			}
			catch (Exception)
			{
				// do something cool
			}
		}
	}
}
