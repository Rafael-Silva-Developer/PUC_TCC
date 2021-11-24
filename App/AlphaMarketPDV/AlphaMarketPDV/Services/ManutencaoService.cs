using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.ViewModels;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace AlphaMarketPDV.Services
{
    public class ManutencaoService : IMailService
    {
        private readonly AlphaMarketPDVContext _context;
        
        private const string _SERVIDOR_EMAIL = "mail.puc-alphamarketpdv.targetbr.biz";

        private const string _USUARIO_EMAIL = "atendimento@puc-alphamarketpdv.targetbr.biz";

        private const string _SENHA_EMAIL = "R127517s#";

        private const string _NOME_EXIBICAO = "Alpha Market PDV";

        private const int _PORTA_EMAIL = 587; //465 ou 587 

        public ManutencaoService(AlphaMarketPDVContext context) 
        {
            _context = context;         
        }

        public async Task<bool> EnviarEmailContato(ContatoViewModel contato) 
        {
            if (contato == null)
            {
                return false;
            }
            else 
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(_USUARIO_EMAIL, _NOME_EXIBICAO);
                    message.CC.Add(contato.Email);
                    message.To.Add(new MailAddress("rafaeldeoliveira88@gmail.com"));
                    message.Subject = "Opinião do Cliente";
                    message.IsBodyHtml = false;
                    message.Body = contato.Mensagem;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = _PORTA_EMAIL;
                    smtp.Host = _SERVIDOR_EMAIL;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_USUARIO_EMAIL, _SENHA_EMAIL);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                    return true;
                }
                catch (Exception e) 
                {
                    throw new Exception(e.Message);
                }              
            }          
        }

        public Task SendEmailAsync(EmailRequest emailRequest)
        {
            throw new NotImplementedException();
        }
    }
}
