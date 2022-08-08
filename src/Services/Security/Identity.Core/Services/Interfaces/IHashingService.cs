namespace Identity.Core.Services.Interfaces
{
	public interface IHashingService
	{
		(byte[] hash, byte[] salt) CreateHash(string value);

		bool ValidateHash(string value, byte[] hash, byte[] salt);
	}
}
