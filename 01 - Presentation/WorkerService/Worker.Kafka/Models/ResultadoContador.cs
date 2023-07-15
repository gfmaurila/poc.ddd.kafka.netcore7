using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Kafka.Models;
public class ResultadoContador
{
    public int? ValorAtual { get; set; }
    public string? Mensagem { get; set; }
}
