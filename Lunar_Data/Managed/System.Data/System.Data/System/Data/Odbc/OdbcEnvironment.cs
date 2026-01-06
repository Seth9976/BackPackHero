using System;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x02000294 RID: 660
	internal sealed class OdbcEnvironment
	{
		// Token: 0x06001CF9 RID: 7417 RVA: 0x00003D55 File Offset: 0x00001F55
		private OdbcEnvironment()
		{
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0008E804 File Offset: 0x0008CA04
		internal static OdbcEnvironmentHandle GetGlobalEnvironmentHandle()
		{
			OdbcEnvironmentHandle odbcEnvironmentHandle = OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle;
			if (odbcEnvironmentHandle == null)
			{
				object obj = OdbcEnvironment.s_globalEnvironmentHandleLock;
				lock (obj)
				{
					odbcEnvironmentHandle = OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle;
					if (odbcEnvironmentHandle == null)
					{
						odbcEnvironmentHandle = new OdbcEnvironmentHandle();
						OdbcEnvironment.s_globalEnvironmentHandle = odbcEnvironmentHandle;
					}
				}
			}
			return odbcEnvironmentHandle;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0008E868 File Offset: 0x0008CA68
		internal static void ReleaseObjectPool()
		{
			object obj = Interlocked.Exchange(ref OdbcEnvironment.s_globalEnvironmentHandle, null);
			if (obj != null)
			{
				(obj as OdbcEnvironmentHandle).Dispose();
			}
		}

		// Token: 0x040015A3 RID: 5539
		private static object s_globalEnvironmentHandle;

		// Token: 0x040015A4 RID: 5540
		private static object s_globalEnvironmentHandleLock = new object();
	}
}
