using AlphaMarketPDV.Models;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
