using LiteDB;

namespace eShopServices.Services.Cart.Cart.API.DataServices
{
	/// <summary>
	/// A wrapper for the LiteDb repository.
	/// </summary>
	public class LiteDbRepository : LiteRepository
	{
		public LiteDbRepository(ILiteDatabase database) : base(database)
		{
		}

		public LiteDbRepository(string connectionString, BsonMapper mapper = null) : base(connectionString, mapper)
		{
		}

		public LiteDbRepository(ConnectionString connectionString, BsonMapper mapper = null) : base(connectionString, mapper)
		{
		}

		public LiteDbRepository(Stream stream, BsonMapper mapper = null, Stream logStream = null) : base(stream, mapper, logStream)
		{
		}

		
	}
}
