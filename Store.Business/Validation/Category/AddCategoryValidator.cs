using FluentValidation;
using Store.Business.Abstract;
using Store.DAL.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Validation.Category
{
	public  class AddCategoryValidator : AbstractValidator<AddCategoryDto>
	{
		
		public AddCategoryValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("Kategori boş geçilemez!")
				.MaximumLength(200).WithMessage("Kategori en fazla 200 karakter olmalıdır.");
		}


	}
}
