using System;
using Unity.Burst.LowLevel;

namespace Unity.Burst
{
	// Token: 0x02000010 RID: 16
	public static class BurstRuntime
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002E15 File Offset: 0x00001015
		public static int GetHashCode32<T>()
		{
			return BurstRuntime.HashCode32<T>.Value;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E1C File Offset: 0x0000101C
		public static int GetHashCode32(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A32(type.AssemblyQualifiedName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E29 File Offset: 0x00001029
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E30 File Offset: 0x00001030
		public static long GetHashCode64(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A64(type.AssemblyQualifiedName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E40 File Offset: 0x00001040
		internal static int HashStringWithFNV1A32(string text)
		{
			uint num = 2166136261U;
			foreach (char c in text)
			{
				num = 16777619U * (num ^ (uint)((byte)(c & 'ÿ')));
				num = 16777619U * (num ^ (uint)((byte)(c >> 8)));
			}
			return (int)num;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E90 File Offset: 0x00001090
		internal static long HashStringWithFNV1A64(string text)
		{
			ulong num = 14695981039346656037UL;
			foreach (char c in text)
			{
				num = 1099511628211UL * (num ^ (ulong)((byte)(c & 'ÿ')));
				num = 1099511628211UL * (num ^ (ulong)((byte)(c >> 8)));
			}
			return (long)num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EEB File Offset: 0x000010EB
		public static bool LoadAdditionalLibrary(string pathToLibBurstGenerated)
		{
			return BurstCompiler.IsLoadAdditionalLibrarySupported() && BurstRuntime.LoadAdditionalLibraryInternal(pathToLibBurstGenerated);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EFC File Offset: 0x000010FC
		internal static bool LoadAdditionalLibraryInternal(string pathToLibBurstGenerated)
		{
			return (bool)typeof(BurstCompilerService).GetMethod("LoadBurstLibrary").Invoke(null, new object[] { pathToLibBurstGenerated });
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F27 File Offset: 0x00001127
		internal static void Initialize()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F29 File Offset: 0x00001129
		[BurstRuntime.PreserveAttribute]
		internal static void PreventDiscardAttributeStrip()
		{
			new BurstDiscardAttribute();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F31 File Offset: 0x00001131
		[BurstRuntime.PreserveAttribute]
		internal unsafe static void Log(byte* message, int logType, byte* fileName, int lineNumber)
		{
			BurstCompilerService.Log(null, (BurstCompilerService.BurstLogType)logType, message, null, lineNumber);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F3F File Offset: 0x0000113F
		public unsafe static byte* GetUTF8LiteralPointer(string str, out int byteCount)
		{
			throw new NotImplementedException("This function only works from Burst");
		}

		// Token: 0x0200002F RID: 47
		private struct HashCode32<T>
		{
			// Token: 0x0400023F RID: 575
			public static readonly int Value = BurstRuntime.HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000030 RID: 48
		private struct HashCode64<T>
		{
			// Token: 0x04000240 RID: 576
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000031 RID: 49
		internal class PreserveAttribute : Attribute
		{
		}
	}
}
