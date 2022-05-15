/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.Roles.Validators
{
    public class RoleEditRequestValidator : AbstractValidator<RoleEditRequest>
    {
        public RoleEditRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id không hợp lệ");

            RuleFor(x => x.SortOrder).GreaterThan((short)0).WithMessage("Chưa nhập Thứ tự");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Chưa nhập Tên");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Chưa nhập Mô tả");
        }
    }
}
