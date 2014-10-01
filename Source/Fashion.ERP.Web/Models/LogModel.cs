using System;

namespace Fashion.ERP.Web.Models
{
    public class LogModel
    {
        public DateTime Data { get; set; }
        public string Classe { get; set; }
        public string Usuario { get; set; }
        public string IpAddress { get; set; }
        public string Mensagem { get; set; }
    }
}