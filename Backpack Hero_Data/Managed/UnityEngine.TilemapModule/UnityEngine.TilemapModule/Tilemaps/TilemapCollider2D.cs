using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000016 RID: 22
	[RequireComponent(typeof(Tilemap))]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapCollider2D.h")]
	public sealed class TilemapCollider2D : Collider2D
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		public extern uint maximumTileChangeCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		public extern float extrusionFactor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000101 RID: 257
		public extern bool hasTilemapChanges
		{
			[NativeMethod("HasTilemapChanges")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000102 RID: 258
		[NativeMethod(Name = "ProcessTileChangeQueue")]
		[MethodImpl(4096)]
		public extern void ProcessTilemapChanges();
	}
}
