using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Model;

namespace WebApplication1.Source.Svc
{
    public interface IDonationService
    {

        public List<BloodDonation> getBloodDonations();
        string AddBlood(string donorName, int unit);

        public string RequestBlood(BloodRequest request);
    }
}
