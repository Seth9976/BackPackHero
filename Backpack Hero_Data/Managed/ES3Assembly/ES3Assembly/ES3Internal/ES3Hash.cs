using System;
using System.Security.Cryptography;
using System.Text;

namespace ES3Internal
{
	// Token: 0x020000C3 RID: 195
	public static class ES3Hash
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0001E924 File Offset: 0x0001CB24
		public static string SHA1Hash(string input)
		{
			string @string;
			using (SHA1Managed sha1Managed = new SHA1Managed())
			{
				@string = Encoding.UTF8.GetString(sha1Managed.ComputeHash(Encoding.UTF8.GetBytes(input)));
			}
			return @string;
		}
	}
}
