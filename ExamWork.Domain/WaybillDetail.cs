using System;

namespace ExamWork.Domain
{
    public class WaybillDetail : Entity
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public Guid WaibillId { get; set; }
    }
}
