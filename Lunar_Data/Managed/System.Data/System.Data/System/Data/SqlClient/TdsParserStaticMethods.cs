using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x0200022B RID: 555
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x060019DF RID: 6623 RVA: 0x00081EC4 File Offset: 0x000800C4
		internal static byte[] ObfuscatePassword(string password)
		{
			byte[] array = new byte[password.Length << 1];
			for (int i = 0; i < password.Length; i++)
			{
				char c = password[i];
				byte b = (byte)(c & 'ÿ');
				byte b2 = (byte)((c >> 8) & 'ÿ');
				array[i << 1] = (byte)((((int)(b & 15) << 4) | (b >> 4)) ^ 165);
				array[(i << 1) + 1] = (byte)((((int)(b2 & 15) << 4) | (b2 >> 4)) ^ 165);
			}
			return array;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00081F3C File Offset: 0x0008013C
		internal static byte[] ObfuscatePassword(byte[] password)
		{
			for (int i = 0; i < password.Length; i++)
			{
				byte b = password[i] & 15;
				byte b2 = password[i] & 240;
				password[i] = (byte)(((b2 >> 4) | ((int)b << 4)) ^ 165);
			}
			return password;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00081F7C File Offset: 0x0008017C
		internal static int GetCurrentProcessIdForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_currentProcessId == -1)
			{
				int num = new Random().Next();
				Interlocked.CompareExchange(ref TdsParserStaticMethods.s_currentProcessId, num, -1);
			}
			return TdsParserStaticMethods.s_currentProcessId;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00081FAE File Offset: 0x000801AE
		internal static int GetCurrentThreadIdForTdsLoginOnly()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00081FB8 File Offset: 0x000801B8
		internal static byte[] GetNetworkPhysicalAddressForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_nicAddress == null)
			{
				byte[] array = new byte[6];
				new Random().NextBytes(array);
				Interlocked.CompareExchange<byte[]>(ref TdsParserStaticMethods.s_nicAddress, array, null);
			}
			return TdsParserStaticMethods.s_nicAddress;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00081FF0 File Offset: 0x000801F0
		internal static int GetTimeoutMilliseconds(long timeoutTime)
		{
			if (9223372036854775807L == timeoutTime)
			{
				return -1;
			}
			long num = ADP.TimerRemainingMilliseconds(timeoutTime);
			if (num < 0L)
			{
				return 0;
			}
			if (num > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0008202C File Offset: 0x0008022C
		internal static long GetTimeout(long timeoutMilliseconds)
		{
			long num;
			if (timeoutMilliseconds <= 0L)
			{
				num = long.MaxValue;
			}
			else
			{
				try
				{
					num = checked(ADP.TimerCurrent() + ADP.TimerFromMilliseconds(timeoutMilliseconds));
				}
				catch (OverflowException)
				{
					num = long.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00082078 File Offset: 0x00080278
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool flag = false;
			if (timeoutTime != 0L && 9223372036854775807L != timeoutTime)
			{
				flag = ADP.TimerHasExpired(timeoutTime);
			}
			return flag;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0008209E File Offset: 0x0008029E
		internal static int NullAwareStringLength(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000820AC File Offset: 0x000802AC
		internal static int GetRemainingTimeout(int timeout, long start)
		{
			if (timeout <= 0)
			{
				return timeout;
			}
			long num = ADP.TimerRemainingSeconds(start + ADP.TimerFromSeconds(timeout));
			if (num <= 0L)
			{
				return 1;
			}
			return checked((int)num);
		}

		// Token: 0x0400128F RID: 4751
		private const int NoProcessId = -1;

		// Token: 0x04001290 RID: 4752
		private static int s_currentProcessId = -1;

		// Token: 0x04001291 RID: 4753
		private static byte[] s_nicAddress = null;
	}
}
