using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiComprinhasDaShein.Models
{
    /// <summary>
    /// Representa a entidade Produto no banco de dados.
    /// </summary>
    public class Produto
    {
        private string _descricao;

        /// <summary>
        /// Identificador único do produto, gerado automaticamente pelo banco de dados.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// Descrição do produto. Não pode ser nula.
        /// </summary>
        public string descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        }

        /// <summary>
        /// Quantidade disponível do produto.
        /// </summary>
        public double Quantidade { get; set; }

        /// <summary>
        /// Preço unitário do produto.
        /// </summary>
        public double Preco { get; set; }

        /// <summary>
        /// Propriedade calculada que retorna o valor total do produto (Quantidade * Preço).
        /// </summary>
        public double Total { get => Quantidade * Preco; }
    }
}
