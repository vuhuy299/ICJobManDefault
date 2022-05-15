/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;

namespace ICSLib.Authen.Models.Genders.Validators
{
    public class GenderDeleteReqeustValidator : AbstractValidator<GenderDeleteReqeust>
    {
        public GenderDeleteReqeustValidator()
        {
            RuleFor(x => x.GenderId).GreaterThan(0).WithMessage("Id không hợp lệ");
        }
    }
}
