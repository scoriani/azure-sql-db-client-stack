using System;
using System.Collections.Generic;

namespace whatsnew
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public int BillToCustomerId { get; set; }
        public int CustomerCategoryId { get; set; }
        public int? BuyingGroupId { get; set; }
        public int PrimaryContactPersonId { get; set; }
        public int? AlternateContactPersonId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int DeliveryCityId { get; set; }
        public int PostalCityId { get; set; }
        public decimal? CreditLimit { get; set; }
        public DateTime AccountOpenedDate { get; set; }
        public decimal StandardDiscountPercentage { get; set; }
        public bool IsStatementSent { get; set; }
        public bool IsOnCreditHold { get; set; }
        public int PaymentDays { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string FaxNumber { get; set; } = null!;
        public string? DeliveryRun { get; set; }
        public string? RunPosition { get; set; }
        public string WebsiteUrl { get; set; } = null!;
        public string DeliveryAddressLine1 { get; set; } = null!;
        public string? DeliveryAddressLine2 { get; set; }
        public string DeliveryPostalCode { get; set; } = null!;
        public string PostalAddressLine1 { get; set; } = null!;
        public string? PostalAddressLine2 { get; set; }
        public string PostalPostalCode { get; set; } = null!;
        public int LastEditedBy { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string? NuCustomerName { get; set; }
    }
}
