using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Task2API.Data;
using Task2API.DTOs.Product;
using Task2API.Model;

namespace Task2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task< IActionResult> GetAllAsync()
        {
            var Products = await context.Products.Select(
                x => new GetAllDto()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                }).ToListAsync();

            return Ok(Products);
        }

        [HttpPost("Create")]
        public  async Task<IActionResult> CreateAsync(CreateProductDto dtomodel, [FromServices]IValidator<CreateProductDto>validator)
        {
            var validationResult = validator.Validate(dtomodel);
            if (!validationResult.IsValid)
            {
                var ModelState = new ModelStateDictionary();
                validationResult.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);


                });
                return ValidationProblem(ModelState);

            }
            var product = new Product()
            {
                Name = dtomodel.Name,
                Description =dtomodel.Description,
                Price= dtomodel.Price,
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok(product);

        }
        [HttpGet("FindById")]
        public async Task<IActionResult>FindByIdAsync(int Id)
        {
            var product = await context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            GetAllDto pro = new GetAllDto()
            {
                Id = product.Id,
                Name = product.Name,
            };
            return Ok(pro);

        }

        [HttpPut("Update")]
        public async Task< IActionResult> UpdateAsync(int Id, CreateProductDto dtomodel, [FromServices] IValidator<CreateProductDto> validator)
        {
            var product = await context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            var validationResult = validator.Validate(dtomodel);
            if (!validationResult.IsValid)
            {
                var ModelState = new ModelStateDictionary();
                validationResult.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);


                });
                return ValidationProblem(ModelState);

            }
            product.Name = dtomodel.Name;
            product.Description = dtomodel.Description;
            product.Price = dtomodel.Price;


            await context.SaveChangesAsync();
            GetAllDto pro = new GetAllDto()
            {
                Id = product.Id,
                Description=product.Description,
                Name = product.Name,
            };
            return Ok(pro);

        }
        [HttpDelete("Delete")]
        public async Task< IActionResult> RemoveAsync(int Id)
        {
            var product =await context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
