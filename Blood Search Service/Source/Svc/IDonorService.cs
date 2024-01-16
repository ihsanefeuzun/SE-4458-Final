using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Source.Svc
{
    public interface IDonorService
    {
        public List<Donor> getDonors();
        public Donor getDonorById(long id);
        public int insertDonor(Donor donor);
        public void updateDonor(Donor donor);

        public void deleteDonor(Donor donor);
    }
}
