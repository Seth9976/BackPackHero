using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001BB RID: 443
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0001B134 File Offset: 0x00019334
		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		// Token: 0x06001367 RID: 4967
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void WriteStringToUnityLogImpl(string s);

		// Token: 0x06001368 RID: 4968 RVA: 0x0001B153 File Offset: 0x00019353
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0001B164 File Offset: 0x00019364
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0001B17B File Offset: 0x0001937B
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0001B18B File Offset: 0x0001938B
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0001B195 File Offset: 0x00019395
		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}
	}
}
