using System;
using UserInterface.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

/// <summary>Attribute used in models to check if a username is already in use nor not.</summary>
public class UsernameAvailableAttribute : ValidationAttribute
{
    /* private readonly ICreateUserService _createuserservice; */

    /* public HomeController(ILogger<HomeController> logger, IAppDb db, IUserBaseService ubs) */
    /* public UsernameAvailableAttribute(ICreateUserService createuserservice) */
    /* { */
    /*     // TODO: Måste läsa in config och anropa lagret Application. */
    /*     _createuserservice = createuserservice; */
    /* } */

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = (ICreateUserService) validationContext
            .GetService(typeof(ICreateUserService));
        var model = (CreateUser)validationContext.ObjectInstance;

        bool isAccountNameAvailable = service.IsAccountNameAvailable(model.UserName);
        Console.WriteLine("isAccountNameAvailable: " + isAccountNameAvailable); 

        /* var test = validationContext.GetService(typeof(ICreateUserService)); */

        if (isAccountNameAvailable == false)
            return new ValidationResult("Username is already in use.");         // ModelState not valid.

        return ValidationResult.Success;
    }

    /* public UsernameAvailableAttribute(string username) */
    /* { */
    /*     Username = username; */
    /* } */

    /* public string Username { get; private set; } */

    /* public string GetErrorMessage() => */
    /*     $"Classic movies must have a release year no later than {Username}."; */

    /* protected override ValidationResult IsValid(object value, */
    /*     ValidationContext validationContext) */
    /* { */
    /*     string test = "fis";            // Test! */
    /*     /1* var movie = (Movie)validationContext.ObjectInstance; *1/ */
    /*     /1* var releaseYear = ((DateTime)value).Year; *1/ */

    /*     if (Username == test) */ 
    /*     { */
    /*         return new ValidationResult(GetErrorMessage()); */
    /*     } */

    /*     return ValidationResult.Success; */
    /* } */
}

// TODO: Hur använder man repository här? Fungerar det med DI på något sätt eller måste man implementera application-lagret här också?
