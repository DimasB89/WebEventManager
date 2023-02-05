using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebEventManager.Models;


namespace WebEventManager
{
    public class Utilities
    {

        public class EestiIDAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance is PrivatePerson privatePerson && privatePerson.PersonalID <= 0)
                {
                    return ValidationResult.Success;
                }
                string personalID = value.ToString();

                    int sum = 0;
                    int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
                    int checkDigit = int.Parse(personalID[10].ToString());

                    for (int i = 0; i < 10; i++)
                    {
                        sum += weights[i] * int.Parse(personalID[i].ToString());
                    }

                    int remainder = sum % 11;
                    if (remainder == 10)
                    {
                        weights = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
                        sum = 0;
                        for (int i = 0; i < 10; i++)
                        {
                            sum += weights[i] * int.Parse(personalID[i].ToString());
                        }

                        remainder = sum % 11;
                        if (remainder == 10)
                        {
                            remainder = 0;
                        }
                    }

                    if (remainder != checkDigit)
                    {
                        //ErrorMessage = "Vale ID";
                        return new ValidationResult("Vale ID");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                
                
            }
        }
    }
}
