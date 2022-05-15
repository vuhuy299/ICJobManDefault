/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.Genders.Validators
{
    public class GenderEditRequestValidator : AbstractValidator<GenderEditRequest>
    {
        public GenderEditRequestValidator()
        {
            RuleFor(x => x.GenderId).GreaterThan(0).WithMessage("Id không hợp lệ");
            RuleFor(x => x.GenderName).NotEmpty().WithMessage("Chưa nhập tên");
            RuleFor(x => x.GenderDesc).NotEmpty().WithMessage("Chưa nhập mô tả");
        }
    }
}
