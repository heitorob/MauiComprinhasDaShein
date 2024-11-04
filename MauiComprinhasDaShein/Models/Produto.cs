using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiComprinhasDaShein.Models
{
    internal class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
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
        public double Quantidade { get; set; }
        public double Preco {  get; set; }
        public double Total { get => Quantidade * Preco; }
    }
}
