using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000108 RID: 264
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	[DebuggerTypeProxy(typeof(WordStorageDebugView))]
	public struct WordStorage
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		[NotBurstCompatible]
		public static ref WordStorage Instance
		{
			get
			{
				WordStorage.Initialize();
				return ref WordStorageStatic.Ref.Data;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001DA6D File Offset: 0x0001BC6D
		public int Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001DA78 File Offset: 0x0001BC78
		[NotBurstCompatible]
		public static void Initialize()
		{
			if (WordStorageStatic.Ref.Data.buffer.IsCreated)
			{
				return;
			}
			WordStorageStatic.Ref.Data.buffer = new NativeArray<byte>(2097152, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			WordStorageStatic.Ref.Data.entry = new NativeArray<WordStorage.Entry>(16384, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			WordStorageStatic.Ref.Data.hash = new NativeParallelMultiHashMap<int, int>(16384, Allocator.Persistent);
			WordStorage.Clear();
			AppDomain.CurrentDomain.DomainUnload += delegate(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			};
			AppDomain.CurrentDomain.ProcessExit += delegate(object _, EventArgs __)
			{
				WordStorage.Shutdown();
			};
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001DB48 File Offset: 0x0001BD48
		[NotBurstCompatible]
		public static void Shutdown()
		{
			if (!WordStorageStatic.Ref.Data.buffer.IsCreated)
			{
				return;
			}
			WordStorageStatic.Ref.Data.buffer.Dispose();
			WordStorageStatic.Ref.Data.entry.Dispose();
			WordStorageStatic.Ref.Data.hash.Dispose();
			WordStorageStatic.Ref.Data = default(WordStorage);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		[NotBurstCompatible]
		public static void Clear()
		{
			WordStorage.Initialize();
			WordStorageStatic.Ref.Data.chars = 0;
			WordStorageStatic.Ref.Data.entries = 0;
			WordStorageStatic.Ref.Data.hash.Clear();
			FixedString32Bytes fixedString32Bytes = default(FixedString32Bytes);
			WordStorageStatic.Ref.Data.GetOrCreateIndex<FixedString32Bytes>(ref fixedString32Bytes);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001DC18 File Offset: 0x0001BE18
		[NotBurstCompatible]
		public static void Setup()
		{
			WordStorage.Clear();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public unsafe void GetFixedString<T>(int index, ref T temp) where T : IUTF8Bytes, INativeList<byte>
		{
			WordStorage.Entry entry = this.entry[index];
			temp.Length = entry.length;
			UnsafeUtility.MemCpy((void*)temp.GetUnsafePtr(), (void*)((byte*)this.buffer.GetUnsafePtr<byte>() + entry.offset), (long)temp.Length);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public int GetIndexFromHashAndFixedString<T>(int h, ref T temp) where T : IUTF8Bytes, INativeList<byte>
		{
			int num;
			NativeParallelMultiHashMapIterator<int> nativeParallelMultiHashMapIterator;
			if (this.hash.TryGetFirstValue(h, out num, out nativeParallelMultiHashMapIterator))
			{
				for (;;)
				{
					WordStorage.Entry entry = this.entry[num];
					if (entry.length == temp.Length)
					{
						int num2 = 0;
						while (num2 < entry.length && temp[num2] == this.buffer[entry.offset + num2])
						{
							num2++;
						}
						if (num2 == temp.Length)
						{
							break;
						}
					}
					if (!this.hash.TryGetNextValue(out num, ref nativeParallelMultiHashMapIterator))
					{
						return -1;
					}
				}
				return num;
			}
			return -1;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001DD14 File Offset: 0x0001BF14
		public bool Contains<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			int hashCode = value.GetHashCode();
			return this.GetIndexFromHashAndFixedString<T>(hashCode, ref value) != -1;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		[NotBurstCompatible]
		public bool Contains(string value)
		{
			FixedString512Bytes fixedString512Bytes = value;
			return this.Contains<FixedString512Bytes>(ref fixedString512Bytes);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001DD58 File Offset: 0x0001BF58
		public int GetOrCreateIndex<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			int hashCode = value.GetHashCode();
			int indexFromHashAndFixedString = this.GetIndexFromHashAndFixedString<T>(hashCode, ref value);
			if (indexFromHashAndFixedString != -1)
			{
				return indexFromHashAndFixedString;
			}
			int num = this.chars;
			ushort num2 = (ushort)value.Length;
			int num3;
			for (int i = 0; i < (int)num2; i++)
			{
				num3 = this.chars;
				this.chars = num3 + 1;
				this.buffer[num3] = value[i];
			}
			this.entry[this.entries] = new WordStorage.Entry
			{
				offset = num,
				length = (int)num2
			};
			this.hash.Add(hashCode, this.entries);
			num3 = this.entries;
			this.entries = num3 + 1;
			return num3;
		}

		// Token: 0x0400033E RID: 830
		private NativeArray<byte> buffer;

		// Token: 0x0400033F RID: 831
		private NativeArray<WordStorage.Entry> entry;

		// Token: 0x04000340 RID: 832
		private NativeParallelMultiHashMap<int, int> hash;

		// Token: 0x04000341 RID: 833
		private int chars;

		// Token: 0x04000342 RID: 834
		private int entries;

		// Token: 0x04000343 RID: 835
		private const int kMaxEntries = 16384;

		// Token: 0x04000344 RID: 836
		private const int kMaxChars = 2097152;

		// Token: 0x04000345 RID: 837
		public const int kMaxCharsPerEntry = 4096;

		// Token: 0x02000109 RID: 265
		private struct Entry
		{
			// Token: 0x04000346 RID: 838
			public int offset;

			// Token: 0x04000347 RID: 839
			public int length;
		}
	}
}
