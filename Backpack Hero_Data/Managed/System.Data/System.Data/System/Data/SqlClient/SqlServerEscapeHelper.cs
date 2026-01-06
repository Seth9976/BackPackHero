using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001E9 RID: 489
	internal static class SqlServerEscapeHelper
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x00071EA1 File Offset: 0x000700A1
		internal static string EscapeIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00071EC2 File Offset: 0x000700C2
		internal static void EscapeIdentifier(StringBuilder builder, string name)
		{
			builder.Append("[");
			builder.Append(name.Replace("]", "]]"));
			builder.Append("]");
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00071EF3 File Offset: 0x000700F3
		internal static string EscapeStringAsLiteral(string input)
		{
			return input.Replace("'", "''");
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00071F05 File Offset: 0x00070105
		internal static string MakeStringLiteral(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return "''";
			}
			return "'" + SqlServerEscapeHelper.EscapeStringAsLiteral(input) + "'";
		}
	}
}
