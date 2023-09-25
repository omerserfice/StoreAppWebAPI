using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Exceptions
{
	public sealed class InvalidCategoryIdException : InvalidException
	{
		public InvalidCategoryIdException(int id) : base("Girilen id değeri geçersizdir. ")
		{

		}
	}
}
