namespace DrinkMachine.Core.Responses
{
    /// <summary>
    /// код ответа, похож на HTTP, но им не является.
    /// </summary>
    public enum ApiResultCode
    {
        /// <summary>
        /// The success.
        /// </summary>
        Success = 200,

        /// <summary>
        /// The invalid confidant.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Действие запрещено.
        /// </summary>
        ActionProhibited = 405,

        /// <summary>
        /// The invalid data.
        /// </summary>
        InvalidData = 408,

        /// <summary>
        /// The database error.
        /// </summary>
        ServerError = 500,

        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown = 0,
    }
}