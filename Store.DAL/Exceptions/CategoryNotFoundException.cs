namespace Store.DAL.Exceptions
{
	public sealed class CategoryNotFoundException : NotFoundException
	{
		public CategoryNotFoundException(int id) : base($"{id} numaralı id ile ilgili kategori bulunamadı.")
		{
		}
	}
}
