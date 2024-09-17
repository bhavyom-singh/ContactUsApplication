using System.Threading.Tasks;
using ContactUsApplication.DTO;
using ContactUsApplication.DBContext;

namespace ContactUsApplication.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DataContext _context;
        public ContactUsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveContactUs(ContactUs contactUs)
        {
            // Saving data in SQLite db
            await _context.ContactUs.AddAsync(contactUs);
            return await _context.SaveChangesAsync()>0;
        }
    }
}
