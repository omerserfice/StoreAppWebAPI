using FluentValidation;
using Store.DAL.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Validation.Category
{
	public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
	{
        public UpdateCategoryValidator()
        {
			RuleFor(p => p.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez.")
			   .MaximumLength(200).WithMessage("Kategori adı en fazla 200 karakter olmalıdır.");
		}
    }
}
