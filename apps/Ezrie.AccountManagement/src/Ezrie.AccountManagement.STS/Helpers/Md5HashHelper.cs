using System.Security.Cryptography;
using System.Text;

namespace Ezrie.AccountManagement.STS.Helpers;

/// <summary>
/// Helper-class to create Md5hashes from strings
/// </summary>
public static class Md5HashHelper
{
	/// <summary>
	/// Computes a Md5-hash of the submitted string and returns the corresponding hash
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "MD5 is sufficiently secure for the purpose (Gravatar API).")]
	public static String GetHash(String input)
	{
		using (var md5 = MD5.Create())
		{
			var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

			var sBuilder = new StringBuilder();

			foreach (var dataByte in bytes)
			{
				sBuilder.Append(dataByte.ToString("x2", CultureInfo.InvariantCulture));
			}

			return sBuilder.ToString();
		}
	}
}

