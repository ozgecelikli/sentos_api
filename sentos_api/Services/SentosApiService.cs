using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sentos_api.Models;

namespace sentos_api.Services
{
    public class SentosApiService
    {
        private readonly HttpClient _httpClient;
        private readonly SentosApiConfig _config;

        public SentosApiService(SentosApiConfig config)
        {
            _config = config;
            
            // Sentos API dokümantasyonuna göre URL yapısı: https://firma-adi.sentos.com.tr/api
            var baseUrl = $"https://{_config.CompanyName}.sentos.com.tr/api";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds)
            };
            
            // Debug için URL'yi logla
            if (_config.EnableLogging)
            {
                System.Diagnostics.Debug.WriteLine($"Sentos API Base URL: {baseUrl}");
                System.Diagnostics.Debug.WriteLine($"Company Name: {_config.CompanyName}");
                System.Diagnostics.Debug.WriteLine($"API Key: {_config.ApiKey?.Substring(0, 8)}...");
                System.Diagnostics.Debug.WriteLine($"API Secret: {_config.SecretKey?.Substring(0, 8)}...");
                System.Diagnostics.Debug.WriteLine($"Headers: Accept=application/json, Content-Type=application/json (HttpContent)");
            }

            // API Key ve Secret ile kimlik doğrulama (Sentos panelinden alınan bilgiler)
            if (!string.IsNullOrEmpty(_config.ApiKey) && !string.IsNullOrEmpty(_config.SecretKey))
            {
                // API Key ve Secret'ı Basic Auth olarak kullan
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{_config.ApiKey}:{_config.SecretKey}"));
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            }
            else
            {
                // Fallback: Email ve şifre ile Basic Auth
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{_config.Username}:{_config.Password}"));
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            }

            // Header'lar - Sentos API dokümantasyonuna göre
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ERPiS-SentOS-Integration/1.0");
        }

        #region Product Operations

        public async Task<ProductListResponse> GetProductsAsync(int page = 1, int pageSize = 50, string status = "active")
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint
                var endpoint = $"/products?page={page}&pageSize={pageSize}&status={status}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductListResponse>(content);
                    return result;
                }
                else
                {
                    return new ProductListResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode} - URL: {_httpClient.BaseAddress}{endpoint}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductListResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ProductResponse> GetProductByIdAsync(int productId)
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint: /products/{id}
                var endpoint = $"/products/{productId}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductResponse>(content);
                    return result;
                }
                else
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ProductResponse> CreateProductAsync(Product product)
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint: POST /products
                var endpoint = "/products";
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductResponse>(responseContent);
                    return result;
                }
                else
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { responseContent }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ProductResponse> UpdateProductAsync(int productId, Product product)
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint: PUT /products/{id}
                var endpoint = $"/products/{productId}";
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductResponse>(responseContent);
                    return result;
                }
                else
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { responseContent }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

   
        #endregion

      
 

        #region ERP Operations
 

        /// <summary>
        /// Sentos ID ile ürün kontrolü yapar
        /// </summary>
        public async Task<ProductResponse> GetProductBySentosIdAsync(int sentosId)
        {
            try
            {
                var endpoint = $"/products/{sentosId}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductResponse>(content);
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = "Ürün bulunamadı",
                        Data = null,
                        Errors = new List<string> { "Ürün Sentos'ta bulunamadı" }
                    };
                }
                else
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// SKU ile ürün kontrolü yapar
        /// </summary>
        public async Task<ProductResponse> GetProductBySkuAsync(string sku)
        {
            try
            {
                var endpoint = $"/products?sku={sku}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ProductListResponse>(content);
                    if (result.Success && result.Data.Count > 0)
                    {
                        return new ProductResponse
                        {
                            Success = true,
                            Message = "Ürün bulundu",
                            Data = result.Data[0],
                            Errors = new List<string>()
                        };
                    }
                    else
                    {
                        return new ProductResponse
                        {
                            Success = false,
                            Message = "Ürün bulunamadı",
                            Data = null,
                            Errors = new List<string> { "SKU ile ürün bulunamadı" }
                        };
                    }
                }
                else
                {
                    return new ProductResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Ürün stoklarını günceller
        /// </summary>
        public async Task<ProductResponse> UpdateProductStockAsync(int productId, List<ProductStock> stocks)
        {
            try
            {
                // Önce mevcut ürünü getir
                var existingProduct = await GetProductBySentosIdAsync(productId);
                if (!existingProduct.Success)
                {
                    return existingProduct;
                }

                // Stok bilgilerini güncelle
                existingProduct.Data.Stocks = stocks;
                existingProduct.Data.UpdatedDate = DateTime.Now;

                // Ürünü güncelle
                return await UpdateProductAsync(productId, existingProduct.Data);
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        #endregion

      

        #region Order Operations

        public async Task<OrderListResponse> GetOrdersAsync(int page = 1, int size = 50, int? status = null, 
            int[] statusIds = null, DateTime? startDate = null, DateTime? endDate = null, 
            string orderId = null, string orderCode = null, string orderbyDirection = "desc", 
            string erpTransfer = null, string erpCheckChanged = null)
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint ve parametreler
                var queryParams = new List<string>();
                
                queryParams.Add($"page={page}");
                queryParams.Add($"size={size}");
                
                if (status.HasValue)
                    queryParams.Add($"status={status.Value}");
                
                if (statusIds != null && statusIds.Length > 0)
                    queryParams.Add($"status_ids={string.Join(",", statusIds)}");
                
                if (startDate.HasValue)
                    queryParams.Add($"start_date={startDate.Value:yyyy-MM-dd}");
                
                if (endDate.HasValue)
                    queryParams.Add($"end_date={endDate.Value:yyyy-MM-dd}");
                
                if (!string.IsNullOrEmpty(orderId))
                    queryParams.Add($"order_id={orderId}");
                
                if (!string.IsNullOrEmpty(orderCode))
                    queryParams.Add($"order_code={orderCode}");
                
                if (!string.IsNullOrEmpty(orderbyDirection))
                    queryParams.Add($"orderby_direction={orderbyDirection}");
                
                if (!string.IsNullOrEmpty(erpTransfer))
                    queryParams.Add($"erp_transfer={erpTransfer}");
                
                if (!string.IsNullOrEmpty(erpCheckChanged))
                    queryParams.Add($"erp_check_changed={erpCheckChanged}");

                var endpoint = $"/orders?{string.Join("&", queryParams)}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<OrderListResponse>(content);
                    return result;
                }
                else
                {
                    return new OrderListResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new OrderListResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
        {
            try
            {
                // Sentos API dokümantasyonuna göre endpoint
                var endpoint = $"/orders/{orderId}";
                var response = await _httpClient.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<OrderResponse>(content);
                    return result;
                }
                else
                {
                    return new OrderResponse
                    {
                        Success = false,
                        Message = $"API Error: {response.StatusCode}",
                        Errors = new List<string> { content }
                    };
                }
            }
            catch (Exception ex)
            {
                return new OrderResponse
                {
                    Success = false,
                    Message = "Connection Error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

      

        #endregion

        #region Authentication

        public async Task<bool> LoginAsync()
        {
            // Sentos API için login gerekmiyor - API Key/Secret ile direkt çalışır
            // Sadece basit bir test yaparak API'nin çalışıp çalışmadığını kontrol et
            try
            {
                var testResponse = await _httpClient.GetAsync("/products?page=1&pageSize=1");
                
                // 200 OK veya 401 Unauthorized alırsak API çalışıyor demektir
                return testResponse.StatusCode == System.Net.HttpStatusCode.OK || 
                       testResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EnsureAuthenticatedAsync()
        {
            // Eğer zaten kimlik doğrulaması yapılmışsa, tekrar yapma
            if (!string.IsNullOrEmpty(_config.AccessToken))
            {
                return true;
            }

            // Otomatik giriş yap
            var loginSuccess = await LoginAsync();
            
            if (loginSuccess)
            {
                // Token başarıyla alındı
                return true;
            }
            else
            {
                // Giriş başarısız - alternatif yöntemler dene
                return await TryAlternativeLoginAsync();
            }
        }

        private async Task<bool> TryAlternativeLoginAsync()
        {
            try
            {
                // Alternatif login endpoint'leri dene
                var endpoints = new[] { "/login", "/api/login", "/auth/login", "/api/auth/token" };
                
                foreach (var endpoint in endpoints)
                {
                    try
                    {
                        var loginData = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("email", _config.Username),
                            new KeyValuePair<string, string>("password", _config.Password)
                        });

                        var loginResponse = await _httpClient.PostAsync(endpoint, loginData);
                        
                        if (loginResponse.IsSuccessStatusCode)
                        {
                            var responseContent = await loginResponse.Content.ReadAsStringAsync();
                            
                            // Token'ı bulmaya çalış
                            if (responseContent.Contains("token") || responseContent.Contains("access_token"))
                            {
                                var loginResult = JsonConvert.DeserializeObject<dynamic>(responseContent);
                                if (loginResult != null)
                                {
                                    var token = loginResult.token ?? loginResult.access_token ?? loginResult.accessToken;
                                    if (token != null)
                                    {
                                        _config.AccessToken = token.ToString();
                                        _httpClient.DefaultRequestHeaders.Authorization = 
                                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config.AccessToken);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Bu endpoint başarısız, diğerini dene
                        continue;
                    }
                }
                
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
