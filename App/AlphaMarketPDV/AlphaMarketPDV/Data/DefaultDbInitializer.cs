using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AlphaMarketPDV.Data
{
    public class DefaultDbInitializer
    {
        public static async Task Initialize(AlphaMarketPDVContext context, 
                                      UserManager<UsuarioApp> userManager,
                                      RoleManager<PerfilApp> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var enderecoDefault = new Endereco
            {
                Id = 1,
                Cep = "99999-999",
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

            //Adicionando os perfils e usuários padrões...
            string supervisorId = "";

            string perfil1 = "Supervisor";
            string descr1 = "Este é o perfil de supervisor que dar acesso a todas as funcionalidades do sistema.";

            string perfil2 = "Atendente";
            string descr2 = "Este é um perfil restrito para atendimento.";

            string password = "P@ssw0rd";

            if (await roleManager.FindByNameAsync(perfil1) == null) 
            {
                await roleManager.CreateAsync(new PerfilApp(perfil1, descr1, DateTime.Now, TipoPerfil.SUPERVISOR));
            }

            if (await roleManager.FindByNameAsync(perfil2) == null) 
            {
                await roleManager.CreateAsync(new PerfilApp(perfil2, descr2, DateTime.Now, TipoPerfil.ATENDENTE));
            }

            if (await userManager.FindByNameAsync("suporte@alphamarket.com.br") == null) 
            {
                var supervisor = new UsuarioApp
                {
                    UserName = "suporte@alphamarket.com.br",
                    Email = "suporte@alphamarket.com.br",
                    Nome = "Suporte",
                    Ativo = true,
                    LojaId = 1
                };

                var result = await userManager.CreateAsync(supervisor);
                if (result.Succeeded) 
                {
                    await userManager.AddPasswordAsync(supervisor, password);
                    await userManager.AddToRoleAsync(supervisor, perfil1);
                }
                supervisorId = supervisor.Id;
            }
            

        }
    }
}
