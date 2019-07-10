﻿using AlexisCorePro.Business.Common.Commands;
using AlexisCorePro.Domain.Model;
using FluentValidation;
using Localization.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Auth.Command
{
    public class LoginCommand : BaseCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly SignInManager<User> signInManager;

        public LoginCommandValidator(IStringLocalizer<SharedResource> stringLocalizer, SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;

            RuleFor(cmd => cmd.Email).NotEmpty();
            RuleFor(cmd => cmd.Password).NotEmpty();
            RuleFor(cmd => cmd)
            .MustAsync((cmd, cancellationToken) => CanLogin(cmd)).WithMessage(stringLocalizer["InvalidLoginAttempt"]);
        }

        private async Task<bool> CanLogin(LoginCommand loginCommand)
        {
            var result = await signInManager.PasswordSignInAsync(loginCommand.Email, loginCommand.Password, false, false);

            return result.Succeeded;
        }
    }
}