using FluentValidation;

namespace Task2API.DTOs.Product
{
    public class CreateProductDtoValidation: AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required, please fill it");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required, please fill it");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required, please fill it");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Name lenght must be less than 30");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name lenght must be greater  than 5");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Name lenght must be greater  than 10");
            RuleFor(x => x.Price).InclusiveBetween(20, 3000).WithMessage("Price must be between 20 an 3000");
                

        }
    }
}
