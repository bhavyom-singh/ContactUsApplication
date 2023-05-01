using System.Threading.Tasks;
using ContactUsApplication.DTO;

namespace ContactUsApplication.Repository
{
    public interface IContactUsRepository
    {
        Task<bool> SaveContactUs(ContactUs contactUs);
    }
}
