using FluentValidation;
using Store.DAL.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Validation.Product
{
	public class AddProductValidator : AbstractValidator<AddProductDto>
	{
        public AddProductValidator()
        {
			RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş geçilemez.")
			   .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakterden oluşmalıdır.");
			RuleFor(p => p.Price).NotEmpty().WithMessage("Ürünün fiyatı boş geçilemez.");
			RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori id boş geçilemez.");
		}
    }
}
