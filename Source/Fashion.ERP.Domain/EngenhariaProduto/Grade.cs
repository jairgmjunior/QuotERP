using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Grade : DomainBase<Grade>
    {
        public virtual string Descricao { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual bool Ativo { get; set; }

        #region Tamanhos

        /// <summary>
        /// Lista de tamanhos, com ordenação.
        /// </summary>
        public virtual IDictionary<Tamanho, int> Tamanhos { get; set; }

        #endregion
    }
}