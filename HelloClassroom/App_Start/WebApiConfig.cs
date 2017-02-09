using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HelloClassroom
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;

			// Sets how DateTimeOffset objects are serialized. We want to return all date/times in UTC time.
			jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter()
			{
				DateTimeStyles = DateTimeStyles.AdjustToUniversal
			});

			// Sets how DateTime objects are serialized. We should be using DateTimeOffset in all cases I can think of, but
			// we can set this setting as well just in case.
			jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
			jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			jsonSerializerSettings.Converters.Add(new StringEnumConverter());
			jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Web API routes
			config.MapHttpAttributeRoutes(new CustomDefaultDirectRouteProvider());
		}

		private class CustomDefaultDirectRouteProvider : DefaultDirectRouteProvider
		{
			public override IReadOnlyList<RouteEntry> GetDirectRoutes(
				HttpControllerDescriptor controllerDescriptor,
				IReadOnlyList<HttpActionDescriptor> actionDescriptors,
				IInlineConstraintResolver constraintResolver)
			{
				IReadOnlyList<RouteEntry> coll = base.GetDirectRoutes(controllerDescriptor, actionDescriptors, constraintResolver);

				foreach (RouteEntry routeEntry in coll)
				{
					if (!string.IsNullOrEmpty(routeEntry.Name))
					{
						//This allows the controller to get the name of the current route for the generation of next links
						routeEntry.Route.DataTokens["RouteName"] = routeEntry.Name;
					}
				}

				return coll;
			}
		}
	}
}
