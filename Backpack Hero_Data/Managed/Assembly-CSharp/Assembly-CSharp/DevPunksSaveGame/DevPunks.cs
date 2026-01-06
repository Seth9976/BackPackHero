using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DevPunksSaveGame
{
	// Token: 0x0200022C RID: 556
	public class DevPunks
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x000AD3FC File Offset: 0x000AB5FC
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x000AD410 File Offset: 0x000AB610
		[NotNull]
		private static DevPunks Instance
		{
			get
			{
				if (DevPunks._nullableInstance != null)
				{
					return DevPunks.Instance;
				}
				return new DevPunks();
			}
			set
			{
				DevPunks.Instance = value;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000AD418 File Offset: 0x000AB618
		[RuntimeInitializeOnLoadMethod]
		private static void InitDevPunks()
		{
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000AD41A File Offset: 0x000AB61A
		private static string GetMessage(string message, bool addStackTrace)
		{
			return message + (addStackTrace ? ("\n$" + Environment.StackTrace + ".") : ".");
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000AD440 File Offset: 0x000AB640
		public static void Info(string message, bool addStackTrace = true)
		{
			Debug.Log(DevPunks.GetMessage(message, addStackTrace));
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000AD44E File Offset: 0x000AB64E
		public static void Warn(string message, bool addStackTrace = true)
		{
			Debug.LogWarning(DevPunks.GetMessage(message, addStackTrace));
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000AD45C File Offset: 0x000AB65C
		public static void Error(string message, bool addStackTrace = true)
		{
			Debug.LogError(DevPunks.GetMessage(message, addStackTrace));
		}

		// Token: 0x04000E74 RID: 3700
		private const bool EnableInfo = true;

		// Token: 0x04000E75 RID: 3701
		private const bool ForceStackTrace = false;

		// Token: 0x04000E76 RID: 3702
		private static DevPunks _nullableInstance;
	}
}
