/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.RoleGroups.Validators
{
    public class RoleGroupDeleteRequestValidator : AbstractValidator<RoleGroupDeleteRequest>
    {
        public RoleGroupDeleteRequestValidator()
        {
            RuleFor(x => x.RoleGroupId).GreaterThan(0).WithMessage("Id không hợp lệ");
        }
    }
}
