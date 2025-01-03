using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Services.Interfaces
{
	public interface IWhatsappService
	{
		Task<Object> AdicionarNumero(string numero);
		Task<Object> EnviarMensagem(string numero, string template, List<Parameters> parameters = null);
	}
}
