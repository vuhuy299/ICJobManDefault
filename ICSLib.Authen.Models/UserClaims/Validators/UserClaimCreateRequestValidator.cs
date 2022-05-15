/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.UserClaims.Validators
{
    public class UserClaimCreateRequestValidator : AbstractValidator<UserClaimCreateRequest>
    {
        public UserClaimCreateRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Id Người dùng không hợp lệ");
            RuleFor(x => x.ClaimType).NotEmpty().WithMessage("Chưa nhập Loại");
            RuleFor(x => x.ClaimValue).NotEmpty().WithMessage("Chưa nhập Giá trị");
        }
    }
}
