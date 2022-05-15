/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.UserClaims.Validators
{
    public class UserClaimEditRequestValidator : AbstractValidator<UserClaimEditRequest>
    {
        public UserClaimEditRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id không hợp lệ");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("Id Người dùng không hợp lệ");
            RuleFor(x => x.ClaimType).NotEmpty().WithMessage("Chưa nhập Loại");
            RuleFor(x => x.ClaimValue).NotEmpty().WithMessage("Chưa nhập Giá trị");
        }
    }
}
