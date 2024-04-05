using N.Model.Entities;
using N.Service.Dto;
using N.Service.FieldServiceFeeService.Dto;

namespace N.Service.ServiceFeePaymentService.Dto
{
    public class ServiceFeePaymentDto : ServiceFeePayment
    {
        public FieldServiceFeeDto? FieldService { get; set; }
    }
}
