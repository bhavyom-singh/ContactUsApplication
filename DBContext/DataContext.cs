using ContactUsApplication.DTO;
using Microsoft.EntityFrameworkCore;

namespace ContactUsApplication.DBContext
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
