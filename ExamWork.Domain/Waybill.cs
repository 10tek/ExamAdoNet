using System;

namespace ExamWork.Domain
{
    public class Waybill : Entity
    {
        public string WaybillNumber { get; set; }
        public Guid StorekeeperId { get; set; }
    }
}
