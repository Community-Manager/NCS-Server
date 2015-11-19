namespace NeighboursCommunitySystem.Data.CustomExceptions
{
    using System;
    using System.Data.Entity.Validation;
    using System.Text;

    public class FormattedDbEntityValidationException : Exception
    {
        public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as DbEntityValidationException;

                if (innerException != null)
                {
                    StringBuilder resultMessage = new StringBuilder();

                    foreach (var validationError in innerException.EntityValidationErrors)
                    {
                        resultMessage.AppendLine(string.Format(
                                        "- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                        validationError.Entry.Entity.GetType().FullName, 
                                        validationError.Entry.State));

                        foreach (var error in validationError.ValidationErrors)
                        {
                            resultMessage.AppendLine(string.Format(
                                    "-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    error.PropertyName,
                                    validationError.Entry.CurrentValues.GetValue<object>(error.PropertyName),
                                    error.ErrorMessage));
                        }
                    }

                    resultMessage.AppendLine();

                    return resultMessage.ToString();
                }

                return base.Message;
            }
        }
    }
}