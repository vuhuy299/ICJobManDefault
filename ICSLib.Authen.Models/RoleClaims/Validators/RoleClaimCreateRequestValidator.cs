/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.RoleClaims.Validators
{
    public class RoleClaimCreateRequestValidator : AbstractValidator<RoleClaimCreateRequest>
    {
        public RoleClaimCreateRequestValidator()
        {
            RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("Id Quyền không hợp lệ");

            RuleFor(x => x.ClaimType).NotEmpty().WithMessage("Chưa nhập Loại");
            RuleFor(x => x.ClaimValue).NotEmpty().WithMessage("Chưa nhập Giá trị");
        }
    }
}
