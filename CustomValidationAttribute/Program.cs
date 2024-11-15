namespace CustomValidationAttribute;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

public interface IUserRepository
{
    bool Exists(string username);
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
      
    }
}
public class UserValidator : ValidationAttribute
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));   

    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("The field is required.");
        }

        string username = value.ToString();

        // Length Validation
        if (username.Length < 6 || username.Length > 50)
        {
            return new ValidationResult(
                $"Username must be between 6 and 50 characters. You entered {username.Length} characters.");
        }

        // Format Validation (Regular Expression)
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
        {
            return new ValidationResult("Username can only contain alphanumeric characters and underscores.");
        }

        // Uniqueness Validation (Dependency Injection)
        if (_userRepository.Exists(username))
        {
            return new ValidationResult("Username is already taken. Please choose a different one.");
        }

        return ValidationResult.Success;
    }
}


