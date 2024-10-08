using System;
using SalesTaxApp.Utils;

namespace SalesTaxApp.Models
{
    public class Item
    {
        #region Properties
        public string Name { get; }
        public int Quantity { get;}
        public decimal Price { get; }
        public bool IsImported { get; }
        public bool IsTaxable { get; }

        #endregion

        #region Constructor
        public Item(string Name, int Quantity, decimal Price, bool IsImported = false, bool IsTaxable = false)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Quantity = Quantity >= 0 ? Quantity : throw new ArgumentException("Quantity cannot be negative");
            this.Price = Price >= 0 ? Price : throw new ArgumentException("Price cannot be negative");
            this.IsImported = IsImported;
            this.IsTaxable = IsTaxable;

        }
        #endregion

        #region Public Method
        public decimal CalculateSalesTax()
        {
            decimal totalTax = 0;

            // Basic sales tax
            if (IsTaxable)
            {
                totalTax += Price * 0.10m * Quantity;
            }

            // Import duty
            if (IsImported)
            {
                totalTax += TaxUtils.ImportDuty(Price) * Quantity;
            }

            // Round up to the nearest 0.05
            return TaxUtils.RoundUpToNearest05(totalTax);
        }
        #endregion
    }
}
