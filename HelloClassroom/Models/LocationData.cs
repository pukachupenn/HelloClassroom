using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HelloClassroom.Commands;
using Newtonsoft.Json.Linq;

namespace HelloClassroom.Models
{
	public class LocationData
	{
		public string Name { get; set; }
        public string Population { get; set; }
        public string Area { get; set; }
        public string CurrentTime { get; set; }
        public string Description { get; set; }
        public string MapImageBase64 { get; set; }
        public string PlaceImageBase64 { get; set; }
        public string EntityType { get; set; }
	}
}