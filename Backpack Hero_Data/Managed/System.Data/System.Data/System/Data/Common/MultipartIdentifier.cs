using System;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x0200031B RID: 795
	internal class MultipartIdentifier
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000A7EC9 File Offset: 0x000A60C9
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, string property, bool ThrowOnEmptyMultipartName)
		{
			return MultipartIdentifier.ParseMultipartIdentifier(name, leftQuote, rightQuote, '.', 4, true, property, ThrowOnEmptyMultipartName);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000A7EDC File Offset: 0x000A60DC
		private static void IncrementStringCount(string name, string[] ary, ref int position, string property)
		{
			position++;
			int num = ary.Length;
			if (position >= num)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, num);
			}
			ary[position] = string.Empty;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000A7F0A File Offset: 0x000A610A
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000A7F14 File Offset: 0x000A6114
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, char separator, int limit, bool removequotes, string property, bool ThrowOnEmptyMultipartName)
		{
			if (limit <= 0)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, limit);
			}
			if (-1 != leftQuote.IndexOf(separator) || -1 != rightQuote.IndexOf(separator) || leftQuote.Length != rightQuote.Length)
			{
				throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			}
			string[] array = new string[limit];
			int num = 0;
			MultipartIdentifier.MPIState mpistate = MultipartIdentifier.MPIState.MPI_Value;
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			StringBuilder stringBuilder2 = null;
			char c = ' ';
			foreach (char c2 in name)
			{
				switch (mpistate)
				{
				case MultipartIdentifier.MPIState.MPI_Value:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						int num2;
						if (c2 == separator)
						{
							array[num] = string.Empty;
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						}
						else if (-1 != (num2 = leftQuote.IndexOf(c2)))
						{
							c = rightQuote[num2];
							stringBuilder.Length = 0;
							if (!removequotes)
							{
								stringBuilder.Append(c2);
							}
							mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
						}
						else
						{
							if (-1 != rightQuote.IndexOf(c2))
							{
								throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
							}
							stringBuilder.Length = 0;
							stringBuilder.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
					if (c2 == separator)
					{
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (-1 != rightQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (MultipartIdentifier.IsWhitespace(c2))
						{
							array[num] = stringBuilder.ToString();
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder();
							}
							stringBuilder2.Length = 0;
							stringBuilder2.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator;
						}
						else
						{
							stringBuilder.Append(c2);
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 != separator)
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 == separator)
						{
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
							mpistate = MultipartIdentifier.MPIState.MPI_Value;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder.Append(c2);
							array[num] = stringBuilder.ToString();
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					else
					{
						stringBuilder2.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseQuote:
					if (c2 == c)
					{
						if (!removequotes)
						{
							stringBuilder.Append(c2);
						}
						mpistate = MultipartIdentifier.MPIState.MPI_RightQuote;
					}
					else
					{
						stringBuilder.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_RightQuote:
					if (c2 == c)
					{
						stringBuilder.Append(c2);
						mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
					}
					else if (c2 == separator)
					{
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (!MultipartIdentifier.IsWhitespace(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						array[num] = stringBuilder.ToString();
						mpistate = MultipartIdentifier.MPIState.MPI_LookForSeparator;
					}
					break;
				}
			}
			switch (mpistate)
			{
			case MultipartIdentifier.MPIState.MPI_Value:
			case MultipartIdentifier.MPIState.MPI_LookForSeparator:
			case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
				goto IL_02D4;
			case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
			case MultipartIdentifier.MPIState.MPI_RightQuote:
				array[num] = stringBuilder.ToString();
				goto IL_02D4;
			}
			throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			IL_02D4:
			if (array[0] == null)
			{
				if (ThrowOnEmptyMultipartName)
				{
					throw ADP.InvalidMultipartName(property, name);
				}
			}
			else
			{
				int num3 = limit - num - 1;
				if (num3 > 0)
				{
					for (int j = limit - 1; j >= num3; j--)
					{
						array[j] = array[j - num3];
						array[j - num3] = null;
					}
				}
			}
			return array;
		}

		// Token: 0x04001834 RID: 6196
		private const int MaxParts = 4;

		// Token: 0x04001835 RID: 6197
		internal const int ServerIndex = 0;

		// Token: 0x04001836 RID: 6198
		internal const int CatalogIndex = 1;

		// Token: 0x04001837 RID: 6199
		internal const int SchemaIndex = 2;

		// Token: 0x04001838 RID: 6200
		internal const int TableIndex = 3;

		// Token: 0x0200031C RID: 796
		private enum MPIState
		{
			// Token: 0x0400183A RID: 6202
			MPI_Value,
			// Token: 0x0400183B RID: 6203
			MPI_ParseNonQuote,
			// Token: 0x0400183C RID: 6204
			MPI_LookForSeparator,
			// Token: 0x0400183D RID: 6205
			MPI_LookForNextCharOrSeparator,
			// Token: 0x0400183E RID: 6206
			MPI_ParseQuote,
			// Token: 0x0400183F RID: 6207
			MPI_RightQuote
		}
	}
}
