using System;
using Unity.Burst.LowLevel;

namespace Unity.Burst
{
	// Token: 0x02000010 RID: 16
	public static class BurstRuntime
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002EE9 File Offset: 0x000010E9
		public static int GetHashCode32<T>()
		{
			return BurstRuntime.HashCode32<T>.Value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static int GetHashCode32(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A32(type.AssemblyQualifiedName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EFD File Offset: 0x000010FD
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F04 File Offset: 0x00001104
		public static long GetHashCode64(Type type)
		{
			return BurstRuntime.HashStringWithFNV1A64(type.AssemblyQualifiedName);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F14 File Offset: 0x00001114
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

		// Token: 0x0600006C RID: 108 RVA: 0x00002F64 File Offset: 0x00001164
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

		// Token: 0x0600006D RID: 109 RVA: 0x00002FBF File Offset: 0x000011BF
		public static bool LoadAdditionalLibrary(string pathToLibBurstGenerated)
		{
			return BurstCompiler.IsLoadAdditionalLibrarySupported() && BurstRuntime.LoadAdditionalLibraryInternal(pathToLibBurstGenerated);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FD0 File Offset: 0x000011D0
		internal static bool LoadAdditionalLibraryInternal(string pathToLibBurstGenerated)
		{
			return (bool)typeof(BurstCompilerService).GetMethod("LoadBurstLibrary").Invoke(null, new object[] { pathToLibBurstGenerated });
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FFB File Offset: 0x000011FB
		internal static void Initialize()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FFD File Offset: 0x000011FD
		[BurstRuntime.PreserveAttribute]
		internal static void PreventDiscardAttributeStrip()
		{
			new BurstDiscardAttribute();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003005 File Offset: 0x00001205
		[BurstRuntime.PreserveAttribute]
		internal unsafe static void Log(byte* message, int logType, byte* fileName, int lineNumber)
		{
			BurstCompilerService.Log(null, (BurstCompilerService.BurstLogType)logType, message, null, lineNumber);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003013 File Offset: 0x00001213
		public unsafe static byte* GetUTF8LiteralPointer(string str, out int byteCount)
		{
			throw new NotImplementedException("This function only works from Burst");
		}

		// Token: 0x0200002E RID: 46
		private struct HashCode32<T>
		{
			// Token: 0x0400023C RID: 572
			public static readonly int Value = BurstRuntime.HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x0200002F RID: 47
		private struct HashCode64<T>
		{
			// Token: 0x0400023D RID: 573
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}

		// Token: 0x02000030 RID: 48
		internal class PreserveAttribute : Attribute
		{
		}
	}
}
