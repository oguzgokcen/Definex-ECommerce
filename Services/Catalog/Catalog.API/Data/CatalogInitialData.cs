using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
	public async Task Populate(IDocumentStore store, CancellationToken cancellation)
	{
		using var session = store.LightweightSession();

		if (await session.Query<Product>().AnyAsync())
			return;

		session.Store<Product>(GetPreconfiguredProducts());
		await session.SaveChangesAsync();
	}

	private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
		{
			new Product
			{
				Id = 1,
				Title = "Black T-Shirt For Woman",
				Description = "Vivamus suscipit tortor eget felis porttitor volutpat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eget tortor risus. Nulla porttitoraccumsan tincidunt. Pellentesque in ipsum id orci porta dapibus.",
				Type = "fashion",
				Brand = "nike",
				Collection = new List<string> { "YENİ GELEN ÜRÜNLER" },
				Category = "Women",
				Price = 145,
				Hot = true,
				Discount = 40,
				Stock = 5,
				New = true,
				Rating = 5,
				Variants = new List<Variant>
				{
					new Variant { VariantId = 101, Sku = "sku1", Size = "s", Color = "yellow", ImageId = 111 },
					new Variant { VariantId = 102, Sku = "sku2", Size = "s", Color = "white", ImageId = 112 }
				},
				Images = new List<ProductImage>
				{
					new ProductImage { ImageId = 111, Alt = "yellow", Src = "1.png", VariantIds = new List<int> { 101, 104 } },
					new ProductImage { ImageId = 112, Alt = "white", Src = "2.png", VariantIds = new List<int> { 102, 105 } },
					new ProductImage { ImageId = 113, Alt = "pink", Src = "3.png", VariantIds = new List<int> { 103, 106 } }
				}
			},
			new Product
			{
				Id = 2,
				Title = "T-Shirt Form Girls",
				Description = "Vivamus suscipit tortor eget felis porttitor volutpat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eget tortor risus. Nulla porttitoraccumsan tincidunt. Pellentesque in ipsum id orci porta dapibus.",
				Type = "fashion",
				Brand = "geox",
				Collection = new List<string> { "TREND" },
				Category = "Women",
				Price = 185,
				Hot = false,
				Discount = 40,
				Stock = 2,
				New = false,
				Rating = 4,
				Tags = new List<string> { "s", "m", "l", "olive", "navy", "red", "geox" },
				Variants = new List<Variant>
				{
					new Variant { VariantId = 201, Sku = "sku1", Size = "s", Color = "olive", ImageId = 211 },
					new Variant { VariantId = 202, Sku = "sku2", Size = "s", Color = "navy", ImageId = 212 },
					new Variant { VariantId = 203, Sku = "sku3", Size = "s", Color = "red", ImageId = 213 },
					new Variant { VariantId = 204, Sku = "sku4", Size = "m", Color = "olive", ImageId = 211 },
					new Variant { VariantId = 205, Sku = "sku4", Size = "m", Color = "navy", ImageId = 212 },
					new Variant { VariantId = 206, Sku = "sku4", Size = "m", Color = "red", ImageId = 213 },
					new Variant { VariantId = 207, Sku = "sku4", Size = "l", Color = "olive", ImageId = 211 },
					new Variant { VariantId = 208, Sku = "sku4", Size = "l", Color = "navy", ImageId = 212 },
					new Variant { VariantId = 209, Sku = "sku4", Size = "l", Color = "red", ImageId = 213 }
				},
				Images = new List<ProductImage>
				{
					new ProductImage { ImageId = 211, Alt = "olive", Src = "3.png", VariantIds = new List<int> { 201, 204, 207 } },
					new ProductImage { ImageId = 212, Alt = "navy", Src = "4.png", VariantIds = new List<int> { 202, 205, 208 } },
					new ProductImage { ImageId = 213, Alt = "pink", Src = "1.png", VariantIds = new List<int> { 203, 206, 209 } },
					new ProductImage { ImageId = 214, Alt = "pink", Src = "1.png", VariantIds = new List<int> { 201, 204 } }
				}
			}
		};
}
