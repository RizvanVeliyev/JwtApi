﻿using FluentValidation;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            //RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            //RuleFor(x=>x.FullName).NotEmpty().MaximumLength(50);
            //RuleFor(x=>x.Password).NotEmpty().MinimumLength(6);
                
        }
    }
}
