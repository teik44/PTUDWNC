using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator()
        {
            RuleFor(a => a.FullName)
                .NotEmpty()
                .WithMessage("Tên tác giả k được để trống")
                .MaximumLength(100)
                .WithMessage("ten tac gia toi da 100 ky tu");

            RuleFor(a => a.UrlSlug)
               .NotEmpty()
               .WithMessage("Urlslug k được để trống")
               .MaximumLength(100)
               .WithMessage("Urlslug toi da 100 ky tu");

            RuleFor(a => a.Description)
               .NotEmpty()
               .WithMessage("Des k được để trống")
               .MaximumLength(100)
               .WithMessage("Des toi da 100 ky tu");


            //RuleFor(a => a)
            //    .NotEmpty()
            //    .WithMessage("Email k được để trống")
            //    .MaximumLength(100)
            //    .WithMessage("email chua toio da 100 ky tu ");

            //RuleFor(a => a.Notes)
            //    .MaximumLength(500)
            //    .WithMessage("Ghi chu toi da 500 ky tu");
        }
    }
}