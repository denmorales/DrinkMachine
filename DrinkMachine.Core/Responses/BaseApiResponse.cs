namespace DrinkMachine.Core.Responses
{
    /// <summary>
    /// базовый ответ сервиса
    /// </summary>
    public class BaseApiResponse
    {
        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        public ApiResultCode ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
    }
}