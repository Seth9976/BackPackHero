using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Util;
using System.Threading;

namespace System
{
	// Token: 0x0200020C RID: 524
	internal sealed class SharedStatics
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x0000259F File Offset: 0x0000079F
		private SharedStatics()
		{
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0005A5AC File Offset: 0x000587AC
		public static string Remoting_Identity_IDGuid
		{
			[SecuritySafeCritical]
			get
			{
				if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(SharedStatics._sharedStatics, ref flag);
						if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
						{
							SharedStatics._sharedStatics._Remoting_Identity_IDGuid = Guid.NewGuid().ToString().Replace('-', '_');
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(SharedStatics._sharedStatics);
						}
					}
				}
				return SharedStatics._sharedStatics._Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0005A63C File Offset: 0x0005883C
		[SecuritySafeCritical]
		public static Tokenizer.StringMaker GetSharedStringMaker()
		{
			Tokenizer.StringMaker stringMaker = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				if (SharedStatics._sharedStatics._maker != null)
				{
					stringMaker = SharedStatics._sharedStatics._maker;
					SharedStatics._sharedStatics._maker = null;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
			if (stringMaker == null)
			{
				stringMaker = new Tokenizer.StringMaker();
			}
			return stringMaker;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005A6AC File Offset: 0x000588AC
		[SecuritySafeCritical]
		public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				SharedStatics._sharedStatics._maker = maker;
				maker = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005A6FC File Offset: 0x000588FC
		internal static int Remoting_Identity_GetNextSeqNum()
		{
			return Interlocked.Increment(ref SharedStatics._sharedStatics._Remoting_Identity_IDSeqNum);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005A70D File Offset: 0x0005890D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static long AddMemoryFailPointReservation(long size)
		{
			return Interlocked.Add(ref SharedStatics._sharedStatics._memFailPointReservedMemory, size);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005A71F File Offset: 0x0005891F
		internal static ulong MemoryFailPointReservedMemory
		{
			get
			{
				return (ulong)Volatile.Read(ref SharedStatics._sharedStatics._memFailPointReservedMemory);
			}
		}

		// Token: 0x040015BF RID: 5567
		private static readonly SharedStatics _sharedStatics = new SharedStatics();

		// Token: 0x040015C0 RID: 5568
		private volatile string _Remoting_Identity_IDGuid;

		// Token: 0x040015C1 RID: 5569
		private Tokenizer.StringMaker _maker;

		// Token: 0x040015C2 RID: 5570
		private int _Remoting_Identity_IDSeqNum;

		// Token: 0x040015C3 RID: 5571
		private long _memFailPointReservedMemory;
	}
}
