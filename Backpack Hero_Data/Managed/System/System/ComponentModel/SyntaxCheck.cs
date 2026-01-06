using System;
using System.IO;

namespace System.ComponentModel
{
	/// <summary>Provides methods to verify the machine name and path conform to a specific syntax. This class cannot be inherited.</summary>
	// Token: 0x02000703 RID: 1795
	public static class SyntaxCheck
	{
		/// <summary>Checks the syntax of the machine name to confirm that it does not contain "\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper machine name format; otherwise, false.</returns>
		/// <param name="value">A string containing the machine name to check. </param>
		// Token: 0x0600396B RID: 14699 RVA: 0x000C8530 File Offset: 0x000C6730
		public static bool CheckMachineName(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.IndexOf('\\') == -1;
		}

		/// <summary>Checks the syntax of the path to see whether it starts with "\\".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x0600396C RID: 14700 RVA: 0x000C8559 File Offset: 0x000C6759
		public static bool CheckPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.StartsWith("\\\\");
		}

		/// <summary>Checks the syntax of the path to see if it starts with "\" or drive letter "C:".</summary>
		/// <returns>true if <paramref name="value" /> matches the proper path format; otherwise, false.</returns>
		/// <param name="value">A string containing the path to check. </param>
		// Token: 0x0600396D RID: 14701 RVA: 0x000C8582 File Offset: 0x000C6782
		public static bool CheckRootedPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && Path.IsPathRooted(value);
		}
	}
}
