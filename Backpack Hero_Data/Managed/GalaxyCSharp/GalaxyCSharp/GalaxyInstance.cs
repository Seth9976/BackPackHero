using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000007 RID: 7
	public static class GalaxyInstance
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002428 File Offset: 0x00000628
		public static IError GetError()
		{
			IntPtr error = GalaxyInstancePINVOKE.GetError();
			IError error2 = ((!(error == IntPtr.Zero)) ? new IError(error, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return error2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000246C File Offset: 0x0000066C
		public static IListenerRegistrar ListenerRegistrar()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.ListenerRegistrar();
			IListenerRegistrar listenerRegistrar = ((!(intPtr == IntPtr.Zero)) ? new IListenerRegistrar(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerRegistrar;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B0 File Offset: 0x000006B0
		public static IListenerRegistrar GameServerListenerRegistrar()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerListenerRegistrar();
			IListenerRegistrar listenerRegistrar = ((!(intPtr == IntPtr.Zero)) ? new IListenerRegistrar(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerRegistrar;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024F2 File Offset: 0x000006F2
		public static void Init(InitParams initpParams)
		{
			GalaxyInstancePINVOKE.Init(InitParams.getCPtr(initpParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000250F File Offset: 0x0000070F
		public static void InitGameServer(InitParams initpParams)
		{
			GalaxyInstancePINVOKE.InitGameServer(InitParams.getCPtr(initpParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000252C File Offset: 0x0000072C
		public static void Shutdown(bool unloadModule)
		{
			GalaxyInstancePINVOKE.Shutdown(unloadModule);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002544 File Offset: 0x00000744
		public static void ShutdownEx(ShutdownParams shutdownParams)
		{
			GalaxyInstancePINVOKE.ShutdownEx(ShutdownParams.getCPtr(shutdownParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002561 File Offset: 0x00000761
		public static void ShutdownGameServerEx(ShutdownParams shutdownParams)
		{
			GalaxyInstancePINVOKE.ShutdownGameServerEx(ShutdownParams.getCPtr(shutdownParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002580 File Offset: 0x00000780
		public static IUser User()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.User();
			IUser user = ((!(intPtr == IntPtr.Zero)) ? new IUser(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return user;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025C4 File Offset: 0x000007C4
		public static IFriends Friends()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Friends();
			IFriends friends = ((!(intPtr == IntPtr.Zero)) ? new IFriends(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return friends;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002608 File Offset: 0x00000808
		public static IChat Chat()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Chat();
			IChat chat = ((!(intPtr == IntPtr.Zero)) ? new IChat(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return chat;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000264C File Offset: 0x0000084C
		public static IMatchmaking Matchmaking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Matchmaking();
			IMatchmaking matchmaking = ((!(intPtr == IntPtr.Zero)) ? new IMatchmaking(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return matchmaking;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002690 File Offset: 0x00000890
		public static INetworking Networking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Networking();
			INetworking networking = ((!(intPtr == IntPtr.Zero)) ? new INetworking(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return networking;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026D4 File Offset: 0x000008D4
		public static IStats Stats()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Stats();
			IStats stats = ((!(intPtr == IntPtr.Zero)) ? new IStats(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return stats;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002718 File Offset: 0x00000918
		public static IUtils Utils()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Utils();
			IUtils utils = ((!(intPtr == IntPtr.Zero)) ? new IUtils(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return utils;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000275C File Offset: 0x0000095C
		public static IApps Apps()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Apps();
			IApps apps = ((!(intPtr == IntPtr.Zero)) ? new IApps(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return apps;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027A0 File Offset: 0x000009A0
		public static IStorage Storage()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Storage();
			IStorage storage = ((!(intPtr == IntPtr.Zero)) ? new IStorage(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return storage;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027E4 File Offset: 0x000009E4
		public static ICustomNetworking CustomNetworking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.CustomNetworking();
			ICustomNetworking customNetworking = ((!(intPtr == IntPtr.Zero)) ? new ICustomNetworking(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return customNetworking;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002828 File Offset: 0x00000A28
		public static ILogger Logger()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Logger();
			ILogger logger = ((!(intPtr == IntPtr.Zero)) ? new ILogger(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return logger;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000286C File Offset: 0x00000A6C
		public static ITelemetry Telemetry()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Telemetry();
			ITelemetry telemetry = ((!(intPtr == IntPtr.Zero)) ? new ITelemetry(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return telemetry;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028B0 File Offset: 0x00000AB0
		public static ICloudStorage CloudStorage()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.CloudStorage();
			ICloudStorage cloudStorage = ((!(intPtr == IntPtr.Zero)) ? new ICloudStorage(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return cloudStorage;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028F2 File Offset: 0x00000AF2
		public static void ProcessData()
		{
			GalaxyInstancePINVOKE.ProcessData();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002909 File Offset: 0x00000B09
		public static void ShutdownGameServer()
		{
			GalaxyInstancePINVOKE.ShutdownGameServer();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002920 File Offset: 0x00000B20
		public static IUser GameServerUser()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerUser();
			IUser user = ((!(intPtr == IntPtr.Zero)) ? new IUser(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return user;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002964 File Offset: 0x00000B64
		public static IMatchmaking GameServerMatchmaking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerMatchmaking();
			IMatchmaking matchmaking = ((!(intPtr == IntPtr.Zero)) ? new IMatchmaking(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return matchmaking;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029A8 File Offset: 0x00000BA8
		public static INetworking GameServerNetworking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerNetworking();
			INetworking networking = ((!(intPtr == IntPtr.Zero)) ? new INetworking(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return networking;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029EC File Offset: 0x00000BEC
		public static IUtils GameServerUtils()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerUtils();
			IUtils utils = ((!(intPtr == IntPtr.Zero)) ? new IUtils(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return utils;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A30 File Offset: 0x00000C30
		public static ITelemetry GameServerTelemetry()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerTelemetry();
			ITelemetry telemetry = ((!(intPtr == IntPtr.Zero)) ? new ITelemetry(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return telemetry;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A74 File Offset: 0x00000C74
		public static ILogger GameServerLogger()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerLogger();
			ILogger logger = ((!(intPtr == IntPtr.Zero)) ? new ILogger(intPtr, false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return logger;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public static void ProcessGameServerData()
		{
			GalaxyInstancePINVOKE.ProcessGameServerData();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x04000015 RID: 21
		private static GalaxyInstance.CustomExceptionHelper exceptionHelper = new GalaxyInstance.CustomExceptionHelper();

		// Token: 0x02000008 RID: 8
		public class Error : ApplicationException
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00002ACD File Offset: 0x00000CCD
			public Error(string message)
				: base(message)
			{
			}
		}

		// Token: 0x02000009 RID: 9
		public class UnauthorizedAccessError : GalaxyInstance.Error
		{
			// Token: 0x06000039 RID: 57 RVA: 0x00002AD6 File Offset: 0x00000CD6
			public UnauthorizedAccessError(string message)
				: base(message)
			{
			}
		}

		// Token: 0x0200000A RID: 10
		public class InvalidArgumentError : GalaxyInstance.Error
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002ADF File Offset: 0x00000CDF
			public InvalidArgumentError(string message)
				: base(message)
			{
			}
		}

		// Token: 0x0200000B RID: 11
		public class InvalidStateError : GalaxyInstance.Error
		{
			// Token: 0x0600003B RID: 59 RVA: 0x00002AE8 File Offset: 0x00000CE8
			public InvalidStateError(string message)
				: base(message)
			{
			}
		}

		// Token: 0x0200000C RID: 12
		public class RuntimeError : GalaxyInstance.Error
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00002AF1 File Offset: 0x00000CF1
			public RuntimeError(string message)
				: base(message)
			{
			}
		}

		// Token: 0x0200000D RID: 13
		private class CustomExceptionHelper
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002AFA File Offset: 0x00000CFA
			static CustomExceptionHelper()
			{
				GalaxyInstance.CustomExceptionHelper.CustomExceptionRegisterCallback(GalaxyInstance.CustomExceptionHelper.customDelegate);
			}

			// Token: 0x0600003F RID: 63
			[DllImport("GalaxyCSharpGlue")]
			public static extern void CustomExceptionRegisterCallback(GalaxyInstance.CustomExceptionHelper.CustomExceptionDelegate customCallback);

			// Token: 0x06000040 RID: 64 RVA: 0x00002B20 File Offset: 0x00000D20
			[MonoPInvokeCallback(typeof(GalaxyInstance.CustomExceptionHelper.CustomExceptionDelegate))]
			private static void SetPendingCustomException(IError.Type type, string message)
			{
				switch (type)
				{
				case IError.Type.UNAUTHORIZED_ACCESS:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new GalaxyInstance.UnauthorizedAccessError(message));
					break;
				case IError.Type.INVALID_ARGUMENT:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new GalaxyInstance.InvalidArgumentError(message));
					break;
				case IError.Type.INVALID_STATE:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new GalaxyInstance.InvalidStateError(message));
					break;
				case IError.Type.RUNTIME_ERROR:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new GalaxyInstance.RuntimeError(message));
					break;
				default:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new ApplicationException(message));
					break;
				}
			}

			// Token: 0x04000016 RID: 22
			private static GalaxyInstance.CustomExceptionHelper.CustomExceptionDelegate customDelegate = new GalaxyInstance.CustomExceptionHelper.CustomExceptionDelegate(GalaxyInstance.CustomExceptionHelper.SetPendingCustomException);

			// Token: 0x0200000E RID: 14
			// (Invoke) Token: 0x06000042 RID: 66
			public delegate void CustomExceptionDelegate(IError.Type type, string message);
		}
	}
}
