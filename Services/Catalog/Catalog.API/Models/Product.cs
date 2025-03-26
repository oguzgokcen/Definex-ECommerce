namespace Catalog.API.Models;

public class Product
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Type { get; set; }
	public string Brand { get; set; }
	public List<string> Collection { get; set; }
	public List<string> Tags { get; set; }
	public string Category { get; set; }
	public decimal Price { get; set; }
	public bool Hot { get; set; }
	public int Discount { get; set; }
	public int Stock { get; set; }
	public bool New { get; set; }
	public int Rating { get; set; }
	public List<Variant> Variants { get; set; }
	public List<ProductImage> Images { get; set; }
}

public class Variant
{
	public int VariantId { get; set; }
	public string Sku { get; set; }
	public string Size { get; set; }
	public string Color { get; set; }
	public int ImageId { get; set; }

}

public class ProductImage
{
	public int ImageId { get; set; }
	public string Alt { get; set; }
	public string Src { get; set; }
	public List<int> VariantIds { get; set; }
}
