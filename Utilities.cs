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

                if (personalID.Length < 11)
                {
                    //ErrorMessage = "Vale ID";
                    return new ValidationResult("Vale ID");
                }

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

        public class MaxLengthInt : ValidationAttribute
        {
            private readonly int _maxLength;

            public MaxLengthInt(int maxLength)
            {
                _maxLength = maxLength;
            }

            public override bool IsValid(object value)
            {
                int length = value.ToString().Length;
                return length <= _maxLength;
            }
        }
    }
}
