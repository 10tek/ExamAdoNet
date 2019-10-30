using ExamWork.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWork.DataAccess
{
    public class ExamWorkContext
    {
        public Repository<Stock> Stocks { get; set; }
        public Repository<Product> Products { get; set; }
        public Repository<Storekeeper> Storekeepers { get; set; }
        public Repository<Waybill> Waibills { get; set; }
        public Repository<WaybillDetail> WaibillDetails { get; set; }

        public ExamWorkContext(string connectionString, string providerInvariantName)
        {
            Stocks = new Repository<Stock>(connectionString, providerInvariantName);
            Products = new Repository<Product>(connectionString, providerInvariantName);
            Storekeepers = new Repository<Storekeeper>(connectionString, providerInvariantName);
            Waibills = new Repository<Waybill>(connectionString, providerInvariantName);
            WaibillDetails = new Repository<WaybillDetail>(connectionString, providerInvariantName);
        }

        public void Dispose()
        {
            Stocks.Dispose();
            Products.Dispose();
            Storekeepers.Dispose();
            Waibills.Dispose();
            WaibillDetails.Dispose();
        }
    }
}
