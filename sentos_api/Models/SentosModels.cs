using System;
using System.Collections.Generic;

namespace sentos_api.Models
{
    #region Category Models

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string Status { get; set; } = "active";
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class CategoryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Category Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class CategoryListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Category> Data { get; set; } = new List<Category>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    #endregion

    #region Warehouse Models

    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class WarehouseListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Warehouse> Data { get; set; } = new List<Warehouse>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class StockResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ProductStock> Data { get; set; } = new List<ProductStock>();
        public List<string> Errors { get; set; } = new List<string>();
    }

    #endregion

    #region ERP Models

    public class ErpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    #endregion

    #region Platform Models

    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; } = true;
        public Dictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class PlatformResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Platform Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class PlatformListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Platform> Data { get; set; } = new List<Platform>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    #endregion

    #region Retail Models

    public class RetailSale
    {
        public int Id { get; set; }
        public string SaleCode { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "TL";
        public string PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public List<RetailSaleItem> Items { get; set; } = new List<RetailSaleItem>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class RetailSaleItem
    {
        public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int VatRate { get; set; }
    }

    public class RetailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    #endregion
}
