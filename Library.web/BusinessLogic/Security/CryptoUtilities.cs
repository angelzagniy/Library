using System.Security.Cryptography;

namespace Library.Web.BusinessLogic.Security;

internal static class CryptoUtilities
{
	private const int KeySize = 64;
	private const int Iterations = 350000;
	private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

	/// <summary>
	/// Generates hashed password and salt for a user password. 
	/// </summary>
	/// <param name="password">User password.</param>
	internal static (string HashedPassword, string Salt) HashPassword(string password)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(count: KeySize);

		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
			password,
			salt,
			Iterations,
			HashAlgorithm,
			outputLength: KeySize);

		return (HashedPassword: Convert.ToHexString(hash), Salt: Convert.ToHexString(salt));
	}

	/// <summary>
	/// Generates hashed password for a user password and salt. 
	/// </summary>
	/// <param name="password">User password.</param>
	/// <param name="hashedPassword">Persisted hashed password.</param>
	/// <param name="salt">Password salt.</param>
	internal static bool IsPasswordValid(
		string password,
		string hashedPassword,
		string salt)
	{
		byte[] saltBytes = Convert.FromHexString(salt);

		byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
			password,
			saltBytes,
			Iterations,
			HashAlgorithm,
			outputLength: KeySize);

		return CryptographicOperations.FixedTimeEquals(
			hashToCompare,
			Convert.FromHexString(hashedPassword));
	}
}