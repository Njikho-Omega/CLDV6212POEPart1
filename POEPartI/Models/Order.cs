using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Data.Tables;

namespace POEPartI.Models
{
    public class Order : ITableEntity
    {
        public string PartitionKey { get; set; } = "Order";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        [Display(Name = "Order ID")]
        public string OrderId => RowKey;

        [Required]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; } = string.Empty;

        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow.Date;

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice {  get; set; }

        public string UnitPriceString
        {
            get => UnitPrice.ToString("F2");
            set
            {
                if (decimal.TryParse(value, out var result))
                    UnitPrice = result;
                else
                    UnitPrice = 0m;
            }
        }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public string TotalPriceString
        {
            get => TotalPrice.ToString("F2");
            set
            {
                if (decimal.TryParse(value, out var result))
                    TotalPrice = result;
                else
                    TotalPrice = 0m;
            }
        }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Submitted";
    }

    public enum OrderSatatus
    {
        Submitted,
        Processing,
        Completed,
        Cancelled
    }
}
