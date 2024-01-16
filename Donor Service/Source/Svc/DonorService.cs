using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Db;

namespace WebApplication1.Source.Svc
{
    public class DonorService : IDonorService
    {
        private BloodContext _context;
        private readonly IMemoryCache _memoryCache;

        public DonorService(BloodContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public Donor getDonorById(long id)
        {
            DonorAccess access = new DonorAccess(_context);
            return access.getDonor(id);
        }

        public List<Donor> getDonors()
        {
            DonorAccess access = new DonorAccess(_context);
            return access.getAllDonors().ToList();
        }

        public int insertDonor(Donor donor)
        {
            DonorAccess access = new DonorAccess(_context);
            return access.insertDonor(donor);
        }

        public Donor getDonorbyBranch(long branchId)
        {
            DonorAccess access = new DonorAccess(_context);
            return access.getDonorbyBranch(branchId);
        }

   
        public void updateDonor(Donor donor)
        {
            DonorAccess access = new DonorAccess(_context);
            access.updateDonor(donor);
        }

        public void deleteDonor(Donor donor)
        {
            DonorAccess access = new DonorAccess(_context);
            access.deleteDonor(donor);
        }
    }
}
