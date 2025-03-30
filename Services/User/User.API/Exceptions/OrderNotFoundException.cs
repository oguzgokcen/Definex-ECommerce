namespace Ordering.API.Exceptions
{
	public class OrderNotFoundException : Exception
	{
		public OrderNotFoundException():base("Hata: Sipariş bulunamadı") { }
	}
}
