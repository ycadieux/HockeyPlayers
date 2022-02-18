using System;

namespace HockeyPlayers.Api
{
    public class AppSettings
    {
        public string Environnement { get; set; }

        DateTime? _now;
        public DateTime Now
        {
            get
            {
                return _now ?? DateTime.Now;
            }
            set
            {
                _now = value;
            }
        }

        public WebSiteSettings WebApiSettings { get; set; }
        public WebSiteSettings WebSiteSettings { get; set; }

        public CorsSettings CorsSettings { get; set; }
    }

    public class WebSiteSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public UrlSettings Url { get; set; }
    }

    public class UrlSettings
    {
        public string Host { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"{Scheme}://{Host}{Path}";
        }
    }

    public class CorsSettings
    {
        public string[] AllowedOrigins { get; set; }
    }
}
