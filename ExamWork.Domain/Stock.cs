using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWork.Domain
{
    public class Stock : Entity
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
