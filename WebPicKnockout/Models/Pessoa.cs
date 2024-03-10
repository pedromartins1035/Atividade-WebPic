using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPicKnockout.Models
{
    public class Pessoa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNasc { get; set; }
        public string EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}