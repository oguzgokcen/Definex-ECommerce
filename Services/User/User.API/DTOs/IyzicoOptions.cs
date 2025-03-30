namespace Ordering.API.DTOs
{
	public class IyzicoOptions
	{
        public string ApiKey { get; set; }

		public string SecretKey { get; set; }

		public string BaseUrl { get; set; }

		public string CallbackUrl { get; set; }

		//Todo: iyzicooptions,paymentservice,updateorderstatus,optional rabbitmq to notify.
	}
}
