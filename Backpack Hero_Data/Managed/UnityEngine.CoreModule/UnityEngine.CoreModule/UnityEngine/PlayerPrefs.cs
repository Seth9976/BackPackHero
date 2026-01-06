using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001D2 RID: 466
	[NativeHeader("Runtime/Utilities/PlayerPrefs.h")]
	public class PlayerPrefs
	{
		// Token: 0x060015AF RID: 5551
		[NativeMethod("SetInt")]
		[MethodImpl(4096)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x060015B0 RID: 5552
		[NativeMethod("SetFloat")]
		[MethodImpl(4096)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x060015B1 RID: 5553
		[NativeMethod("SetString")]
		[MethodImpl(4096)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x060015B2 RID: 5554 RVA: 0x00022DAC File Offset: 0x00020FAC
		public static void SetInt(string key, int value)
		{
			bool flag = !PlayerPrefs.TrySetInt(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015B3 RID: 5555
		[MethodImpl(4096)]
		public static extern int GetInt(string key, int defaultValue);

		// Token: 0x060015B4 RID: 5556 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key, 0);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00022DF0 File Offset: 0x00020FF0
		public static void SetFloat(string key, float value)
		{
			bool flag = !PlayerPrefs.TrySetFloat(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015B6 RID: 5558
		[MethodImpl(4096)]
		public static extern float GetFloat(string key, float defaultValue);

		// Token: 0x060015B7 RID: 5559 RVA: 0x00022E18 File Offset: 0x00021018
		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key, 0f);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00022E38 File Offset: 0x00021038
		public static void SetString(string key, string value)
		{
			bool flag = !PlayerPrefs.TrySetSetString(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015B9 RID: 5561
		[MethodImpl(4096)]
		public static extern string GetString(string key, string defaultValue);

		// Token: 0x060015BA RID: 5562 RVA: 0x00022E60 File Offset: 0x00021060
		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key, "");
		}

		// Token: 0x060015BB RID: 5563
		[MethodImpl(4096)]
		public static extern bool HasKey(string key);

		// Token: 0x060015BC RID: 5564
		[MethodImpl(4096)]
		public static extern void DeleteKey(string key);

		// Token: 0x060015BD RID: 5565
		[NativeMethod("DeleteAllWithCallback")]
		[MethodImpl(4096)]
		public static extern void DeleteAll();

		// Token: 0x060015BE RID: 5566
		[NativeMethod("Sync")]
		[MethodImpl(4096)]
		public static extern void Save();
	}
}
