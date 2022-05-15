/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.Roles.Validators
{
    public class RoleDeleteRequestValidator : AbstractValidator<RoleDeleteRequest>
    {
        public RoleDeleteRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id không hợp lệ");
        }
    }
}
