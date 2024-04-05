using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace N.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void UseAppSettings(this IConfiguration configuration)
        {
            AppSettings.ConnectionStrings = new ConnectionStrings()
            {
                DefaultConnection = configuration.GetConnectionString("DefaultConnection") ?? "",
                DistCacheConnectionString = configuration.GetConnectionString("DistCacheConnectionString") ?? "",
            };
            AppSettings.AuthSettings = new AuthSettings()
            {
                Key = configuration["AuthSettings:Key"] ?? "",
                Audience = configuration["AuthSettings:Audience"] ?? "",
                Issuer = configuration["AuthSettings:Issuer"] ?? "",
                SecondsExpires = Convert.ToInt32(configuration["AuthSettings:SecondsExpires"]),
                DaysRefreshTokenExpires = Convert.ToInt32(configuration["AuthSettings:DaysRefreshTokenExpires"]),
            };
            AppSettings.Mail = new Mail()
            {
                From = configuration["Mail:From"] ?? "",
                Host = configuration["Mail:Host"] ?? "",
                Alias = configuration["Mail:Alias"] ?? "",
                Port = Convert.ToInt32(configuration["Mail:Port"] ?? "0"),
                UserName = configuration["Mail:UserName"] ?? "",
                Password = configuration["Mail:Password"] ?? "",
                AllowSendMail = configuration["Mail:AllowSendMail"] == "true",
                EnableSsl = configuration["Mail:EnableSsl"] == "true",
            };
            AppSettings.VnPay = new VnPay()
            {
                TmnCode = configuration["VnPay:TmnCode"] ?? "",
                HashSecret = configuration["VnPay:HashSecret"] ?? "",
                BaseUrl = configuration["VnPay:BaseUrl"] ?? "",
                Command = configuration["VnPay:Command"] ?? "",
                CurrCode = configuration["VnPay:CurrCode"] ?? "",
                Version = configuration["VnPay:Version"] ?? "",
                Locale = configuration["VnPay:Locale"] ?? "",
                ReturnUrl = configuration["VnPay:ReturnUrl"] ?? "",
            };

        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public static class AppSettings
    {
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static AuthSettings AuthSettings { get; set; }
        public static Mail Mail { get; set; }
        public static VnPay VnPay { get; set; }
        public static int SecondsRequestFile { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string DistCacheConnectionString { get; set; }
    }
    public class AuthSettings
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int SecondsExpires { get; set; }
        public int DaysRefreshTokenExpires { get; set; }
    }


    public class Mail
    {
        public string From { get; set; }
        public string Host { get; set; }
        public string Alias { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool AllowSendMail { get; set; }
    }
    public class VnPay
    {
        public string TmnCode { get; set; }
        public string HashSecret { get; set; }
        public string BaseUrl { get; set; }
        public string Command { get; set; }
        public string CurrCode { get; set; }
        public string Version { get; set; }
        public string Locale { get; set; }
        public string ReturnUrl { get; set; }
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
