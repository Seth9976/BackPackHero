using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F0 RID: 240
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Misc/Cache.h")]
	public struct Cache : IEquatable<Cache>
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00006F7C File Offset: 0x0000517C
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00006F94 File Offset: 0x00005194
		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00006FB8 File Offset: 0x000051B8
		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00006FE0 File Offset: 0x000051E0
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00006FF8 File Offset: 0x000051F8
		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00007024 File Offset: 0x00005224
		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00007048 File Offset: 0x00005248
		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		// Token: 0x06000454 RID: 1108
		[MethodImpl(4096)]
		internal static extern bool Cache_IsValid(int handle);

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00007068 File Offset: 0x00005268
		public bool ready
		{
			get
			{
				return Cache.Cache_IsReady(this.m_Handle);
			}
		}

		// Token: 0x06000456 RID: 1110
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_IsReady(int handle);

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00007088 File Offset: 0x00005288
		public bool readOnly
		{
			get
			{
				return Cache.Cache_IsReadonly(this.m_Handle);
			}
		}

		// Token: 0x06000458 RID: 1112
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_IsReadonly(int handle);

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000070A8 File Offset: 0x000052A8
		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		// Token: 0x0600045A RID: 1114
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern string Cache_GetPath(int handle);

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000070C8 File Offset: 0x000052C8
		public int index
		{
			get
			{
				return Cache.Cache_GetIndex(this.m_Handle);
			}
		}

		// Token: 0x0600045C RID: 1116
		[MethodImpl(4096)]
		internal static extern int Cache_GetIndex(int handle);

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000070E8 File Offset: 0x000052E8
		public long spaceFree
		{
			get
			{
				return Cache.Cache_GetSpaceFree(this.m_Handle);
			}
		}

		// Token: 0x0600045E RID: 1118
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetSpaceFree(int handle);

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00007108 File Offset: 0x00005308
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00007125 File Offset: 0x00005325
		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache.Cache_GetMaximumDiskSpaceAvailable(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		// Token: 0x06000461 RID: 1121
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		// Token: 0x06000462 RID: 1122
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00007138 File Offset: 0x00005338
		public long spaceOccupied
		{
			get
			{
				return Cache.Cache_GetCachingDiskSpaceUsed(this.m_Handle);
			}
		}

		// Token: 0x06000464 RID: 1124
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00007158 File Offset: 0x00005358
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00007175 File Offset: 0x00005375
		public int expirationDelay
		{
			get
			{
				return Cache.Cache_GetExpirationDelay(this.m_Handle);
			}
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		// Token: 0x06000467 RID: 1127
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern int Cache_GetExpirationDelay(int handle);

		// Token: 0x06000468 RID: 1128
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		// Token: 0x06000469 RID: 1129 RVA: 0x00007188 File Offset: 0x00005388
		public bool ClearCache()
		{
			return Cache.Cache_ClearCache(this.m_Handle);
		}

		// Token: 0x0600046A RID: 1130
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_ClearCache(int handle);

		// Token: 0x0600046B RID: 1131 RVA: 0x000071A8 File Offset: 0x000053A8
		public bool ClearCache(int expiration)
		{
			return Cache.Cache_ClearCache_Expiration(this.m_Handle, expiration);
		}

		// Token: 0x0600046C RID: 1132
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);

		// Token: 0x04000326 RID: 806
		private int m_Handle;
	}
}
