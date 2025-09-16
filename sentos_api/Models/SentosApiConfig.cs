using System;

namespace sentos_api.Models
{
    public class SentosApiConfig
    {
        public string BaseUrl { get; set; } = "https://cappafe.sentos.com.tr/api";
        public string ApiKey { get; set; } = "2bc45b71-f1e7-4749-b81a-4398d910829a";
        public string SecretKey { get; set; } = "J1w5784rXRZhqBsn9lw8OvubhfflzvnKSDaavzay";
        public string AccessToken { get; set; }
        public int TimeoutSeconds { get; set; } = 30;
        public bool EnableLogging { get; set; } = true;
        
        // Basic Auth bilgileri (Sentos API dokümantasyonuna göre)
        public string Username { get; set; } = "ocelikli@editbilisim.com.tr";
        public string Password { get; set; } = "Edit2025*";
        
        // Firma adı (URL için gerekli)
        public string CompanyName { get; set; } = "cappafe";
    }
}
