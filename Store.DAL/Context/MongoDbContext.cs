using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.DAL.MongoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
	public class MongoDbContext : IMongoDbContext
	{
		private readonly IMongoDatabase _mongoDB;

		public MongoDbContext(IOptions<MongoOptions> mongoOptions)
		{
			var mongoClient = new MongoClient("mongodb://localhost:27017");
			_mongoDB = mongoClient.GetDatabase(mongoOptions.Value.DatabaseName);
		}

		public IMongoCollection<T> GetCollection<T>()
		{
			return _mongoDB.GetCollection<T>(typeof(T).Name);
		}

		public IMongoDatabase GetDatabase<T>()
		{
			return _mongoDB;
		}
	}
}
