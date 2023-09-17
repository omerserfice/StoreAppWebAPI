using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.MongoEntity
{
	public class ProductCoverImage
	{
        public ObjectId Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Image { get; set; }
    }
}
