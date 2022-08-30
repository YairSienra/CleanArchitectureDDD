using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Models
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Asunto { get; set; }
        public string body { get; set; }
    }
}
