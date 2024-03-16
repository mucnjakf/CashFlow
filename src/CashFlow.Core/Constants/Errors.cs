namespace CashFlow.Core.Constants;

public static class Errors
{
    public static class Account
    {
        public const string BalancePositiveNumber = "Balance must be a positive number";
        public const string AccountNotFound = "Account not found";
    }

    public static class Transaction
    {
        public const string DescriptionRequired = "Description is required";
        public const string AmountGreaterThanZero = "Amount must be greater than 0";
    }

    public static class Category
    {
        public const string NameRequired = "Name is required";
    }
}