using Library.Web.BusinessLogic.Security;
using NUnit.Framework;

namespace Library.Tests;

[TestFixture]
internal class CryptoUtilitiesTests
{
	[Test]
	public void HashPassword_ShouldGenerateDifferentHashesForSamePasswords()
	{
		string password = Guid.NewGuid().ToString();
		
		(string HashedPassword, string Salt) resultOne = CryptoUtilities.HashPassword(password);
		(string HashedPassword, string Salt) resultTwo = CryptoUtilities.HashPassword(password);
		
		Assert.That(resultOne.Salt, Is.Not.EqualTo(resultTwo.Salt).IgnoreCase);
		Assert.That(resultOne.HashedPassword, Is.Not.EqualTo(resultTwo.HashedPassword).IgnoreCase);
	}
	
	[Test]
	public void IsPasswordValid_ShouldReturnTrueForValidPassword()
	{
		string password = Guid.NewGuid().ToString();
		
		(string HashedPassword, string Salt) newPassword = CryptoUtilities.HashPassword(password);
		
		bool isPasswordValid = CryptoUtilities.IsPasswordValid(
			password, 
			newPassword.HashedPassword,
			newPassword.Salt);
		
		Assert.That(isPasswordValid, Is.True);
	}
	
	[TestCase("qwerty", "Qwerty")]
	[TestCase("1234", "123")]
	public void IsPasswordValid_ShouldReturnFalseForInvalidPassword(string passwordOne, string passwordTwo)
	{
		(string HashedPassword, string Salt) originalPassword = CryptoUtilities.HashPassword(passwordOne);
		
		bool isPasswordValid = CryptoUtilities.IsPasswordValid(
			passwordTwo, 
			originalPassword.HashedPassword,
			originalPassword.Salt);
		
		Assert.That(isPasswordValid, Is.False);
	}

}