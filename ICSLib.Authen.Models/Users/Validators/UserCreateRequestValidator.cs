﻿/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using FluentValidation;
using System;

namespace ICSLib.Authen.Models.Users.Validators
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Chưa tên đăng nhập")
                .MinimumLength(4).WithMessage("Tên đăng nhập quá ngắn")
                .MaximumLength(250).WithMessage("Tên đăng nhập quá dài");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Chưa nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Chưa xác nhận mật khẩu");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (!string.IsNullOrEmpty(request.Password)
                    && !string.IsNullOrEmpty(request.ConfirmPassword)
                    && !request.Password.Equals(request.ConfirmPassword))
                {
                    context.AddFailure("Xác nhận mật khẩu không khớp");
                }
            });

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Chưa nhập Họ và Tên")
                .MinimumLength(6).WithMessage("Họ và Tên quá ngắn")
                .MaximumLength(250).WithMessage("Họ và Tên quá dài");

            RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.Now.AddYears(-100))
                .WithMessage("Ngày sinh không hợp lệ");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Chưa nhập Email")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email không hợp lệ");
        }
    }
}
