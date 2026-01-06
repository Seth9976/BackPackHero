using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x0200080A RID: 2058
	internal static class PathInternal
	{
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x000E573A File Offset: 0x000E393A
		internal static StringComparison StringComparison
		{
			get
			{
				if (!PathInternal.s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000E5746 File Offset: 0x000E3946
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000E5750 File Offset: 0x000E3950
		private static bool GetIsCaseSensitive()
		{
			bool flag;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					flag = !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000C537F File Offset: 0x000C357F
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000E57D8 File Offset: 0x000E39D8
		private static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000E580A File Offset: 0x000E3A0A
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000E582C File Offset: 0x000E3A2C
		internal static string EnsureExtendedPrefix(string path)
		{
			if (PathInternal.IsPartiallyQualified(path) || PathInternal.IsDevice(path))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000E5868 File Offset: 0x000E3A68
		internal static bool IsDevice(string path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator(path[0]) && PathInternal.IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?') && PathInternal.IsDirectorySeparator(path[3]));
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000E58C8 File Offset: 0x000E3AC8
		internal static bool IsExtended(string path)
		{
			return path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?' && path[3] == '\\';
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000E5918 File Offset: 0x000E3B18
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			int i = 0;
			int num = 2;
			int num2 = 2;
			bool flag = path.StartsWith("\\\\?\\");
			bool flag2 = path.StartsWith("\\\\?\\UNC\\");
			if (flag)
			{
				if (flag2)
				{
					num2 = "\\\\?\\UNC\\".Length;
				}
				else
				{
					num += "\\\\?\\".Length;
				}
			}
			if ((!flag || flag2) && path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				i = 1;
				if (flag2 || (path.Length > 1 && PathInternal.IsDirectorySeparator((char)(*path[1]))))
				{
					i = num2;
					int num3 = 2;
					while (i < path.Length)
					{
						if (PathInternal.IsDirectorySeparator((char)(*path[i])) && --num3 <= 0)
						{
							break;
						}
						i++;
					}
				}
			}
			else if (path.Length >= num && *path[num - 1] == (ushort)Path.VolumeSeparatorChar)
			{
				i = num;
				if (path.Length >= num + 1 && PathInternal.IsDirectorySeparator((char)(*path[num])))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000E5A1C File Offset: 0x000E3C1C
		internal static bool IsPartiallyQualified(string path)
		{
			if (path.Length < 2)
			{
				return true;
			}
			if (PathInternal.IsDirectorySeparator(path[0]))
			{
				return path[1] != '?' && !PathInternal.IsDirectorySeparator(path[1]);
			}
			return path.Length < 3 || path[1] != Path.VolumeSeparatorChar || !PathInternal.IsDirectorySeparator(path[2]) || !PathInternal.IsValidDriveChar(path[0]);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000E5A95 File Offset: 0x000E3C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x04002759 RID: 10073
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();

		// Token: 0x0400275A RID: 10074
		internal const string ExtendedDevicePathPrefix = "\\\\?\\";

		// Token: 0x0400275B RID: 10075
		internal const string UncPathPrefix = "\\\\";

		// Token: 0x0400275C RID: 10076
		internal const string UncDevicePrefixToInsert = "?\\UNC\\";

		// Token: 0x0400275D RID: 10077
		internal const string UncExtendedPathPrefix = "\\\\?\\UNC\\";

		// Token: 0x0400275E RID: 10078
		internal const string DevicePathPrefix = "\\\\.\\";

		// Token: 0x0400275F RID: 10079
		internal const int MaxShortPath = 260;

		// Token: 0x04002760 RID: 10080
		internal const int DevicePrefixLength = 4;
	}
}
