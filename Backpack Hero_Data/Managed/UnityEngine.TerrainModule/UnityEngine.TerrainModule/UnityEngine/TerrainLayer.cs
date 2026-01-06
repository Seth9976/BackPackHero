using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[NativeHeader("Modules/Terrain/Public/TerrainLayerScriptingInterface.h")]
	[NativeHeader("TerrainScriptingClasses.h")]
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class TerrainLayer : Object
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00004294 File Offset: 0x00002494
		public TerrainLayer()
		{
			TerrainLayer.Internal_Create(this);
		}

		// Token: 0x0600016A RID: 362
		[FreeFunction("TerrainLayerScriptingInterface::Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] TerrainLayer layer);

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016B RID: 363
		// (set) Token: 0x0600016C RID: 364
		public extern Texture2D diffuseTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016D RID: 365
		// (set) Token: 0x0600016E RID: 366
		public extern Texture2D normalMapTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		public extern Texture2D maskMapTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000042A8 File Offset: 0x000024A8
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000042BE File Offset: 0x000024BE
		public Vector2 tileSize
		{
			get
			{
				Vector2 vector;
				this.get_tileSize_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_tileSize_Injected(ref value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000042C8 File Offset: 0x000024C8
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000042DE File Offset: 0x000024DE
		public Vector2 tileOffset
		{
			get
			{
				Vector2 vector;
				this.get_tileOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_tileOffset_Injected(ref value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000042E8 File Offset: 0x000024E8
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000042FE File Offset: 0x000024FE
		[NativeProperty("SpecularColor")]
		public Color specular
		{
			get
			{
				Color color;
				this.get_specular_Injected(out color);
				return color;
			}
			set
			{
				this.set_specular_Injected(ref value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000178 RID: 376
		public extern float metallic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		public extern float smoothness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600017B RID: 379
		// (set) Token: 0x0600017C RID: 380
		public extern float normalScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004308 File Offset: 0x00002508
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000431E File Offset: 0x0000251E
		public Vector4 diffuseRemapMin
		{
			get
			{
				Vector4 vector;
				this.get_diffuseRemapMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_diffuseRemapMin_Injected(ref value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004328 File Offset: 0x00002528
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000433E File Offset: 0x0000253E
		public Vector4 diffuseRemapMax
		{
			get
			{
				Vector4 vector;
				this.get_diffuseRemapMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_diffuseRemapMax_Injected(ref value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004348 File Offset: 0x00002548
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000435E File Offset: 0x0000255E
		public Vector4 maskMapRemapMin
		{
			get
			{
				Vector4 vector;
				this.get_maskMapRemapMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_maskMapRemapMin_Injected(ref value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004368 File Offset: 0x00002568
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000437E File Offset: 0x0000257E
		public Vector4 maskMapRemapMax
		{
			get
			{
				Vector4 vector;
				this.get_maskMapRemapMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_maskMapRemapMax_Injected(ref value);
			}
		}

		// Token: 0x06000185 RID: 389
		[MethodImpl(4096)]
		private extern void get_tileSize_Injected(out Vector2 ret);

		// Token: 0x06000186 RID: 390
		[MethodImpl(4096)]
		private extern void set_tileSize_Injected(ref Vector2 value);

		// Token: 0x06000187 RID: 391
		[MethodImpl(4096)]
		private extern void get_tileOffset_Injected(out Vector2 ret);

		// Token: 0x06000188 RID: 392
		[MethodImpl(4096)]
		private extern void set_tileOffset_Injected(ref Vector2 value);

		// Token: 0x06000189 RID: 393
		[MethodImpl(4096)]
		private extern void get_specular_Injected(out Color ret);

		// Token: 0x0600018A RID: 394
		[MethodImpl(4096)]
		private extern void set_specular_Injected(ref Color value);

		// Token: 0x0600018B RID: 395
		[MethodImpl(4096)]
		private extern void get_diffuseRemapMin_Injected(out Vector4 ret);

		// Token: 0x0600018C RID: 396
		[MethodImpl(4096)]
		private extern void set_diffuseRemapMin_Injected(ref Vector4 value);

		// Token: 0x0600018D RID: 397
		[MethodImpl(4096)]
		private extern void get_diffuseRemapMax_Injected(out Vector4 ret);

		// Token: 0x0600018E RID: 398
		[MethodImpl(4096)]
		private extern void set_diffuseRemapMax_Injected(ref Vector4 value);

		// Token: 0x0600018F RID: 399
		[MethodImpl(4096)]
		private extern void get_maskMapRemapMin_Injected(out Vector4 ret);

		// Token: 0x06000190 RID: 400
		[MethodImpl(4096)]
		private extern void set_maskMapRemapMin_Injected(ref Vector4 value);

		// Token: 0x06000191 RID: 401
		[MethodImpl(4096)]
		private extern void get_maskMapRemapMax_Injected(out Vector4 ret);

		// Token: 0x06000192 RID: 402
		[MethodImpl(4096)]
		private extern void set_maskMapRemapMax_Injected(ref Vector4 value);
	}
}
