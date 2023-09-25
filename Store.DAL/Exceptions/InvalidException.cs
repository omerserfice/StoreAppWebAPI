using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Exceptions
{
	public abstract class InvalidException : Exception
	{
        protected InvalidException(string message) : base(message)
        {
            
        }
    }
}
