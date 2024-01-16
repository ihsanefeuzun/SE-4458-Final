namespace WebApplication1.Model.Dto
{
    public class DonorDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string BloodType { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        //public byte[] Photo { get; set; }
        public long BranchId { get; set; }

        public string PhotoUrl { get; set; }
    }
}
