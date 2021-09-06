using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Enums;

namespace AlphaMarketPDV.Data
{
    public class DefaultDbInitializer
    {
        public static void Initialize(AlphaMarketPDVContext context)
        {
            context.Database.EnsureCreated();

            if (context.Loja.Any() || context.Endereco.Any() || context.Usuario.Any())
            {
                return;
            }

            var enderecoDefault = new Endereco
            {
                Id = 1,
                Cep = "00000-000",
                Lougradouro = "Rua Central",
                Bairro = "Central",
                Cidade = "Rio de Janeiro",
                Uf = "RJ"
            };

            context.Endereco.Add(enderecoDefault);
            context.SaveChanges();

            var lojaMatriz = new Loja
            {
                Id = 1, 
                Descricao = "Matriz",
                EndComplemento = "Centro",
                EndNumero = "2021",
                Endereco = enderecoDefault
            };

            context.Loja.Add(lojaMatriz);
            context.SaveChanges();

            var usuarioDefault = new Usuario 
            { 
                Id = 1,
                Login = "supervisor",
                Senha = "03dbef3b07b910f5a6bc78572dac9a23",
                Ativo = true,
                Nome = "Suporte",
                Tipo = TipoUsuario.SUPERVISOR,
                Loja = lojaMatriz
            };

            context.Usuario.Add(usuarioDefault);
            context.SaveChanges();
        }
    }
}
