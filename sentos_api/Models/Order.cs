using System;
using System.Collections.Generic;

namespace sentos_api.Models
{
    public class Order
    {
        public int id { get; set; }
        public string order_id { get; set; }
        public string order_code { get; set; }
        public int status { get; set; }
        public string source { get; set; }
        public string shop { get; set; }
        public DateTime order_date { get; set; }
        public string total { get; set; }
        public string shipping_total { get; set; }
        public string cargo_provider { get; set; }
        public string cargo_number { get; set; }
        public string has_invoice { get; set; }
        public string cancel_invoice { get; set; }
        public string invoice_type { get; set; }
        public string invoice_number { get; set; }
        public string invoice_url { get; set; }
        public string note { get; set; }
        public OrderCustomer customer { get; set; }
        public OrderAddress invoice_address { get; set; }
        public OrderAddress shipment_address { get; set; }
        public List<OrderLine> lines { get; set; } = new List<OrderLine>();
    }

    public class OrderLine
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public int vat_rate { get; set; }
        public string color { get; set; }
        public OrderModel model { get; set; }
    }

    public class OrderModel
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class OrderCustomer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string mail_address { get; set; }
    }

    public class OrderAddress
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
    }


    public class OrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Order Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class OrderListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Order> Data { get; set; } = new List<Order>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
