using ExamWork.DataAccess;
using ExamWork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamWork.Service
{
    public class StockService
    {
        private readonly ExamWorkContext context;

        public StockService(ExamWorkContext context)
        {
            this.context = context;
        }

        public Waybill MakeWaybill(Dictionary<Product, int> waybillDetails, Storekeeper storekeeper)
        {
            var waybill = new Waybill
            {
                WaybillNumber = $"{new Random().Next(1000, 10000)}",
                StorekeeperId = storekeeper.Id
            };
            foreach (var waybillDetail in waybillDetails)
            {
                context.WaibillDetails.Add(new WaybillDetail
                {
                    ProductId = waybillDetail.Key.Id,
                    WaibillId = waybill.Id,
                    Count = waybillDetail.Value
                });
            }
            return waybill;
        }

        public void SetByWaybill(Waybill waybill)
        {
            var stocks = context.Stocks.GetAll().ToList();
            var waybillDetails = context.WaibillDetails.GetAll().Where(x => x.WaibillId == waybill.Id).ToList();
            foreach (var waybillDetail in waybillDetails)
            {
                UpdateProductCount(stocks, waybillDetail);
            }
        }

        public bool GetByWaybill(Waybill waybill)
        {
            var stocks = context.Stocks.GetAll().ToList();
            var waybillDetails = context.WaibillDetails.GetAll().Where(x => x.WaibillId == waybill.Id).ToList();
            if (!CheckProducts(stocks, waybillDetails))
            {
                return false;
            }
            else
            {
                foreach (var waybillDetail in waybillDetails)
                {
                    DeductProducts(stocks, waybillDetail);
                }
                return true;
            }
        }

        private void UpdateProductCount(List<Stock> stocks, WaybillDetail waybillDetail)
        {
            foreach (var product in stocks)
            {
                if (product.ProductId == waybillDetail.ProductId)
                {
                    product.Count += waybillDetail.Count;
                    context.Stocks.Update(product);
                }
            }

            foreach (var product in stocks)
            {
                if (product.ProductId != waybillDetail.ProductId)
                {
                    context.Stocks.Add(new Stock
                    {
                        ProductId = waybillDetail.ProductId,
                        Count = waybillDetail.Count
                    });
                }
            }
        }

        private void DeductProducts(List<Stock> stocks, WaybillDetail waybillDetail)
        {
            foreach (var product in stocks)
            {
                product.Count -= waybillDetail.Count;
                context.Stocks.Update(product);
            }
        }

        private bool CheckProducts(List<Stock> stocks, List<WaybillDetail> waybillDetails)
        {
            foreach (var waybillDetail in waybillDetails)
            {
                foreach (var product in stocks)
                {
                    if (product.ProductId != waybillDetail.ProductId || product.Count - waybillDetail.Count < 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
