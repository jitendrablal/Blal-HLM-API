using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace BlalApi.Authentication
{
    public static class SecurityManager
    {
        private const string _alg = "HmacSHA256";
        private const string _salt = "klRLuOtFBXphj9WQfjTi";
        public static int ExpirationMinutes = 1440;

        public const string HeaderAuthorizationKey = "Authorization";
        public const string HeaderDeviceIdKey = "DeviceId";
        public const string HeaderUserAgentKey = "User-Agent";

        public static string GetHttpRequestHeader(HttpHeaders headers, string headerName)
        {
            if (!headers.Contains(headerName))
                return string.Empty;

            return headers.GetValues(headerName).FirstOrDefault();
        }

        public static string GenerateToken(string userId, string password, string deviceID, string userAgent, long timeStamp)
        {
            string hash = string.Join(":", new string[] { userId, deviceID, "MobileApp", timeStamp.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { userId, timeStamp.ToString() });
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }


        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }
    }
}