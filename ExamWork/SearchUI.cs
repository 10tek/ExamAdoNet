using ExamWork.DataAccess;
using ExamWork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamWork.UI
{
    public class SearchUI
    {
        private const int COUNT_IN_PAGE = 3;
        private readonly ExamWorkContext context;

        public SearchUI(ExamWorkContext context)
        {
            this.context = context;
        }

        public void ShowProducts(List<Product> products)
        {
            var index = 1;
            products.ForEach(x => Console.WriteLine($"{index++}) Name: {x.ProductName}"));
        }
    }
}
