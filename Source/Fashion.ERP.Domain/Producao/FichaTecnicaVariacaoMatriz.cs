﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaVariacaoMatriz : DomainEmpresaBase<FichaTecnicaVariacaoMatriz>
    {
        private readonly IList<Cor> _cores;

        public virtual Variacao Variacao { get; set; }

        public FichaTecnicaVariacaoMatriz()
        {
            _cores = new List<Cor>();
        }

        #region Cores
        public virtual IReadOnlyCollection<Cor> Cores
        {
            get { return new ReadOnlyCollection<Cor>(_cores); }
        }

        public virtual void AddCor(params Cor[] cores)
        {
            foreach (var cor in cores)
            {
                _cores.Add(cor);
            }
        }

        public virtual void RemoveCor(params Cor[] cores)
        {
            foreach (var cor in cores)
            {
                if (_cores.Contains(cor))
                    _cores.Remove(cor);
            }
        }

        #endregion
    }
}