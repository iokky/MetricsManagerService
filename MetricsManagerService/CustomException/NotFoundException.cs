using MetricsManagerService.CustomException.ExcetrionMessageEnum;
namespace MetricsManagerService.CustomException
{
    public class NotFoundException: Exception
    {
        public NotFoundException()
            :base(ExceptionMessage.ObjectIsNotFound.ToString()){ }  
    }      
}
