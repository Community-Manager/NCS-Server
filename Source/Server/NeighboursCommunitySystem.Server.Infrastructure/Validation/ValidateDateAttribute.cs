namespace NeighboursCommunitySystem.Server.Common.CustomAttributes
{
    using System;
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateDateAttribute : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            return (DateTime)value >= DateTime.Now.AddDays(1);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}