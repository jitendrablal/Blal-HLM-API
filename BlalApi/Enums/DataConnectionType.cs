namespace BlalApi.Enums
{
    /// <summary>
    /// DataConnectionType
    /// </summary>
    public enum DataConnectionType
    {
        /// <summary>
        /// StoredProcedure
        /// </summary>
        StoredProcedure,

        /// <summary>
        /// Text
        /// </summary>
        Text
    }
    public enum StatusCode
    {
        OK = 200,
        ERROR = 202,
        REQUEST_DENIED = 401,
        INVALID_REQUEST = 400,
        NO_ACCOUNT = 201,
        NOT_VERIFIED = 203,
        TOKEN_EXPIRE = 204,
    }
    public enum HeaderParam
    {
        android,
        ios,
        application_json
    }
}