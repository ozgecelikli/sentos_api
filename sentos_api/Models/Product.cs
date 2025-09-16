using System;
using System.Collections.Generic;

namespace sentos_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string InvoiceName { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; } = "TL";
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int VatRate { get; set; }
        public decimal VolumetricWeight { get; set; }
        public string Barcode { get; set; }
        public string DescriptionDetail { get; set; }
        public List<ProductStock> Stocks { get; set; } = new List<ProductStock>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ProductPrices Prices { get; set; } = new ProductPrices();
        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        
        // Sentos API için ek alanlar
        public string TransferStatus { get; set; } = "Beklemede";
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class ProductStock
    {
        public int Warehouse { get; set; }
        public int Stock { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class ProductPrices
    {
        public PlatformPrice B2C { get; set; } = new PlatformPrice();
        public PlatformPrice N11 { get; set; } = new PlatformPrice();
        public PlatformPrice Trendyol { get; set; } = new PlatformPrice();
        public PlatformPrice Hepsiburada { get; set; } = new PlatformPrice();
        public PlatformPrice Ciceksepeti { get; set; } = new PlatformPrice();
        public PlatformPrice Epttavm { get; set; } = new PlatformPrice();
        public PlatformPrice Pazarama { get; set; } = new PlatformPrice();
        public PlatformPrice Modanisa { get; set; } = new PlatformPrice();
        public PlatformPrice Pasaj { get; set; } = new PlatformPrice();
        public PlatformPrice Amazon { get; set; } = new PlatformPrice();
        public PlatformPrice Beymen { get; set; } = new PlatformPrice();
        public PlatformPrice Lcw { get; set; } = new PlatformPrice();
        public PlatformPrice Joom { get; set; } = new PlatformPrice();
        public PlatformPrice Etsy { get; set; } = new PlatformPrice();
        public PlatformPrice Hepsiglobal { get; set; } = new PlatformPrice();
        public PlatformPrice Exporgin { get; set; } = new PlatformPrice();
        public PlatformPrice Woo { get; set; } = new PlatformPrice();
        public PlatformPrice Idea { get; set; } = new PlatformPrice();
        public PlatformPrice Hipotenus { get; set; } = new PlatformPrice();
        public PlatformPrice Opencart { get; set; } = new PlatformPrice();
        public PlatformPrice Ethica { get; set; } = new PlatformPrice();
        public PlatformPrice Ikas { get; set; } = new PlatformPrice();
    }

    public class PlatformPrice
    {
        public string ListPrice { get; set; }
        public string SalePrice { get; set; }
    }

    public class ProductVariant
    {
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public ProductModel Model { get; set; } = new ProductModel();
        public string Color { get; set; }
        public List<ProductStock> Stocks { get; set; } = new List<ProductStock>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
    }

    public class ProductModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Product Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ProductListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Product> Data { get; set; } = new List<Product>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
