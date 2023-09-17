using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
	public interface IMongoDbContext 
	{
		IMongoCollection<T> GetCollection<T>();
		IMongoDatabase GetDatabase<T>();
	}
}
