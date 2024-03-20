namespace CashFlow.Core.Constants;

/// <summary>
/// Error messages
/// </summary>
public static class Errors
{
    /// <summary>
    /// General error messages
    /// </summary>
    public static class General
    {
        public const string UnhandledError = "Unhandled error occured";
    }

    /// <summary>
    /// Account error messages
    /// </summary>
    public static class Account
    {
        public const string BalancePositiveNumber = "Balance must be a positive number";
        public const string AccountNotFound = "Account not found";
        public const string InsufficientBalance = "Insufficient balance";
    }

    /// <summary>
    /// Transaction error messages
    /// </summary>
    public static class Transaction
    {
        public const string DescriptionRequired = "Description is required";
        public const string AmountGreaterThanZero = "Amount must be greater than 0";
        public const string TransactionNotFound = "Transaction not found";
        public const string TypeRequired = "Type is required";
    }

    /// <summary>
    /// Category error messages
    /// </summary>
    public static class Category
    {
        public const string NameRequired = "Name is required";
        public const string CategoryNotFound = "Category not found";
        public const string CategoryContainsTransactions = "Category contains transactions";
    }
}