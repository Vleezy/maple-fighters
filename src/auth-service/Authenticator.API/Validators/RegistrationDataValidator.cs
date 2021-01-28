﻿using Authenticator.API.Constants;
using Authenticator.API.Datas;
using FluentValidation;

namespace Authenticator.API.Validators
{
    public class RegistrationDataValidator : AbstractValidator<RegistrationData>
    {
        public RegistrationDataValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(ErrorMessages.InvalidEmail);
            RuleFor(x => x.Email).Length(3, 50).WithMessage(ErrorMessages.EmailLength);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ErrorMessages.PasswordRequired);
            RuleFor(x => x.Password).Length(8, 16).WithMessage(ErrorMessages.PasswordLength);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ErrorMessages.FirstNameRequired);
            RuleFor(x => x.FirstName).Length(2, 26).WithMessage(ErrorMessages.FirstNameLength);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(ErrorMessages.LastNameRequired);
            RuleFor(x => x.LastName).Length(2, 26).WithMessage(ErrorMessages.LastNameLength);
        }
    }
}