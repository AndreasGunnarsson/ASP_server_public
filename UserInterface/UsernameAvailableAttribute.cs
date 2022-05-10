using UserInterface.Models;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

/// <summary>Attribute used in models to check if a username is used or not.</summary>
public class UsernameAvailableAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = (ICreateUserService) validationContext
            .GetService(typeof(ICreateUserService));
        var model = (CreateUser)validationContext.ObjectInstance;

        bool isAccountNameAvailable = service.IsAccountNameAvailable(model.UserName);

        if (isAccountNameAvailable == false)
            return new ValidationResult("Username is already in use.");         // ModelState not valid.

        return ValidationResult.Success;
    }
}
