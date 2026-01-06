using System;
using System.IO;
using Mono.Security.X509;

namespace Mono.Btls
{
	// Token: 0x0200010C RID: 268
	internal static class MonoBtlsX509StoreManager
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x000116A8 File Offset: 0x0000F8A8
		private static void Initialize()
		{
			if (MonoBtlsX509StoreManager.initialized)
			{
				return;
			}
			try
			{
				MonoBtlsX509StoreManager.DoInitialize();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("MonoBtlsX509StoreManager.Initialize() threw exception: {0}", ex);
			}
			finally
			{
				MonoBtlsX509StoreManager.initialized = true;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000116FC File Offset: 0x0000F8FC
		private static void DoInitialize()
		{
			string newCurrentUserPath = X509StoreManager.NewCurrentUserPath;
			MonoBtlsX509StoreManager.userTrustedRootPath = Path.Combine(newCurrentUserPath, "Trust");
			MonoBtlsX509StoreManager.userIntermediateCAPath = Path.Combine(newCurrentUserPath, "CA");
			MonoBtlsX509StoreManager.userUntrustedPath = Path.Combine(newCurrentUserPath, "Disallowed");
			string newLocalMachinePath = X509StoreManager.NewLocalMachinePath;
			MonoBtlsX509StoreManager.machineTrustedRootPath = Path.Combine(newLocalMachinePath, "Trust");
			MonoBtlsX509StoreManager.machineIntermediateCAPath = Path.Combine(newLocalMachinePath, "CA");
			MonoBtlsX509StoreManager.machineUntrustedPath = Path.Combine(newLocalMachinePath, "Disallowed");
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00011774 File Offset: 0x0000F974
		public static bool HasStore(MonoBtlsX509StoreType type)
		{
			string storePath = MonoBtlsX509StoreManager.GetStorePath(type);
			return storePath != null && Directory.Exists(storePath);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00011794 File Offset: 0x0000F994
		public static string GetStorePath(MonoBtlsX509StoreType type)
		{
			MonoBtlsX509StoreManager.Initialize();
			switch (type)
			{
			case MonoBtlsX509StoreType.MachineTrustedRoots:
				return MonoBtlsX509StoreManager.machineTrustedRootPath;
			case MonoBtlsX509StoreType.MachineIntermediateCA:
				return MonoBtlsX509StoreManager.machineIntermediateCAPath;
			case MonoBtlsX509StoreType.MachineUntrusted:
				return MonoBtlsX509StoreManager.machineUntrustedPath;
			case MonoBtlsX509StoreType.UserTrustedRoots:
				return MonoBtlsX509StoreManager.userTrustedRootPath;
			case MonoBtlsX509StoreType.UserIntermediateCA:
				return MonoBtlsX509StoreManager.userIntermediateCAPath;
			case MonoBtlsX509StoreType.UserUntrusted:
				return MonoBtlsX509StoreManager.userUntrustedPath;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000450 RID: 1104
		private static bool initialized;

		// Token: 0x04000451 RID: 1105
		private static string machineTrustedRootPath;

		// Token: 0x04000452 RID: 1106
		private static string machineIntermediateCAPath;

		// Token: 0x04000453 RID: 1107
		private static string machineUntrustedPath;

		// Token: 0x04000454 RID: 1108
		private static string userTrustedRootPath;

		// Token: 0x04000455 RID: 1109
		private static string userIntermediateCAPath;

		// Token: 0x04000456 RID: 1110
		private static string userUntrustedPath;
	}
}
