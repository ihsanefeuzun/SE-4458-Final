using WebApplication1.Controllers;

namespace WebApplication1.Source.Svc
{
    public interface IBloodSearchService
    {
        void NightlySearchForBloodRequests(BloodRequest request);
    }
}
