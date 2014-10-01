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

        public virtual IDictionary<Tamanho, int> Tamanhos { get; set; }

        public virtual void AddTamanho(params Tamanho[] tamanhos)
        {
            //foreach (var tamanho in tamanhos)
            //    if (!_tamanhos.Values.Contains(tamanho))
            //        _tamanhos.Add(_tamanhos.Count + 1, tamanho);
        }

        public virtual void SwapTamanho(int de, int para)
        {
            //if (_tamanhos.ContainsKey(de) && _tamanhos.ContainsKey(para))
            //{
            //    var tamanho = _tamanhos[de];
            //    _tamanhos[de] = _tamanhos[para];
            //    _tamanhos[para] = tamanho;
            //}
        }

        public virtual void RemoveTamanho(params Tamanho[] tamanhos)
        {
            //var keys = tamanhos
            //    .Where(tamanho => _tamanhos.Values.Contains(tamanho))
            //    .Select(t => _tamanhos.First(p => p.Value == t).Key);

            //foreach (var key in keys)
            //    _tamanhos.Remove(key);
        }

        #endregion
    }
}