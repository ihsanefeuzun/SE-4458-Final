using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;

namespace WebApplication1.Source.Db
{
    public class DonorAccess
    {
        private BloodContext _context;

        public DonorAccess(BloodContext context)
        {
            _context = context;
        }
        public Donor getDonor(long id)
        {
            return _context.Donors.FirstOrDefault(s => s.Id == id);
        }

        public Donor getDonorbyBranch(long branchId)
        {
            return _context.Donors.FirstOrDefault(s => s.BranchId == branchId);
        }

        public int insertDonor(Donor data)
        {
            // validateData(data);
            _context.Donors.Add(data);
            return _context.SaveChanges();

        }
        
        public int deleteDonor(Donor donor)
        {

            _context.Donors.Remove(donor);
            return _context.SaveChanges();
        }
        public IEnumerable<Donor> getAllDonors()
        {
            return _context.Donors;
        }

        public void updateDonor(Donor donor)
        {
            // Implementation to update a student in the database
            _context.Donors.Update(donor);
            _context.SaveChanges();
        }
        public IEnumerable<BloodDonation> getAllBloodDonations()
        {
            return _context.BloodDonations;
        }
    }
}
