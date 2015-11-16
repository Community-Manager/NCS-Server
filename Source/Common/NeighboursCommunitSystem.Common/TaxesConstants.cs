namespace NeighboursCommunitySystem.Common
{
    public class TaxesConstants
    {
        public const int TaxesNameLengthMin = 1;
        public const int TaxesNameLengthMax = 30;
        public const string ShortNameErrorMessage = "Tax name length should be equal to or more than 1 characters.";
        public const string LongNameErrorMessage = "Tax name length should be equal to or shorter than 30 characters.";

        public const int DescriptionLengthMax = 300;
        public const string LongDescriptionErrorMessage = "Tax description should be no more than 300 characters long.";

        public const string DeadLineExceptionMessage = "The deadline must be atleast 24 hours further in the future counting from the current day.";
    }
}
