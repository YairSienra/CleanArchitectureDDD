using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exeptions
{
    public class NotFoundExeptions : ApplicationException
    {
        public NotFoundExeptions(string name, object key) : base($"Entity \"{name}\" ({key})  No fue encontrado")
        {

        }
    }
}
