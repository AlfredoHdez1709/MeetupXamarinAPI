using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace DivisasRestFull
{
	public class Conexion
	{
		[JsonProperty("disclaimer")]
		public string Disclaimer { get; set; }

		[JsonProperty("license")]
		public string License { get; set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }

		[JsonProperty("base")]
		public string Conexbase { get; set; }

		[JsonProperty("rates")]
		public Rates Rates { get; set; }

	}

	public class Rates
	{
		public double BRL { get; set; }
		public double CAD { get; set; }
		public double CHF { get; set; }
		public double CLP { get; set; }
		public double COP { get; set; }
		public double DKK { get; set; }
		public double EUR { get; set; }
		public double GBP { get; set; }
		public double INR { get; set; }
		public double JPY { get; set; }
		public double MXN { get; set; }
		public double USD { get; set; }
	}
}
