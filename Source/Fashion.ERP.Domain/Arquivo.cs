using System;

namespace Fashion.ERP.Domain
{
    public class Arquivo : DomainBase<Arquivo>
    {
        public virtual string Nome { get; set; }
        public virtual string Titulo { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Extensao { get; set; }
        public virtual double Tamanho { get; set; }
    }
}