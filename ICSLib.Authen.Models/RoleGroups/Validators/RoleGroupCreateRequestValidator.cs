/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.RoleGroups.Validators
{
    public class RoleGroupCreateRequestValidator : AbstractValidator<RoleGroupCreateRequest>
    {
        public RoleGroupCreateRequestValidator()
        {
            RuleFor(x => x.RoleGroupName).NotEmpty().WithMessage("Chưa nhập Tên");
            RuleFor(x => x.RoleGroupDesc).NotEmpty().WithMessage("Chưa nhập Mô tả");
        }
    }
}
