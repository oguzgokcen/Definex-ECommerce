using FluentValidation;

namespace Catalog.API.Products.UpdateProducts;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool response);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidator()
	{
		RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");

		RuleFor(command => command.Name)
			.NotEmpty().WithMessage("Name is required")
			.Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

		RuleFor(command => command.Price)
			.GreaterThan(0).WithMessage("Price must be greater than 0");
	}
}

public class UpdateProductsCommandHandler
	(IDocumentSession session)
	: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
	{
		var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

		if (product == null)
		{
			throw new ProductNotFoundException();
		}

		foreach(var property in typeof(Product).GetProperties())
		{
			if (property.Name == "Id")
			{
				continue;
			}
			var value = typeof(UpdateProductCommand).GetProperty(property.Name).GetValue(command);
			property.SetValue(product, value);
		}

		session.Update(product);
		await session.SaveChangesAsync(cancellationToken);

		return new UpdateProductResult(true);
	}
}
