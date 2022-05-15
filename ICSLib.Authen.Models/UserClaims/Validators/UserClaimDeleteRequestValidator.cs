/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.UserClaims.Validators
{
    public class UserClaimDeleteRequestValidator : AbstractValidator<UserClaimDeleteRequest>
    {
        public UserClaimDeleteRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id không hợp lệ");
        }
    }
}
