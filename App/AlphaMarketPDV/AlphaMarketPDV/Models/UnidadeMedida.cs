using System.Collections.Generic;

namespace AlphaMarketPDV.Models
{
    public class UnidadeMedida
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public UnidadeMedida()
        {
        }

        public UnidadeMedida(int id, string descricao, string sigla, bool ativo) 
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Sigla = sigla;
            this.Ativo = ativo;
        }

        public void AdicionarProduto(Produto p) 
        {
            Produtos.Add(p);
        }

        public void RemoverProoduto(Produto p) 
        {
            Produtos.Remove(p);
        }
    }
}
