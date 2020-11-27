using System;

namespace BookStore.Business.Exceptions
{
    public class ValidationFailException : Exception
    {
        public string[] ValidationErrors { get; set; }

        public ValidationFailException(params string[] ValidationErrors)
            : base($"Validation Error:{string.Join("; ", ValidationErrors)}")
        {
            this.ValidationErrors = ValidationErrors;
        }
    }
}
