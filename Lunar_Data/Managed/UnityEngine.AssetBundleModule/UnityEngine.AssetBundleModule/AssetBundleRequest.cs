using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[StructLayout(0)]
	public class AssetBundleRequest : ResourceRequest
	{
		// Token: 0x06000060 RID: 96
		[NativeMethod("GetLoadedAsset")]
		[MethodImpl(4096)]
		protected override extern Object GetResult();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002850 File Offset: 0x00000A50
		public new Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98
		public extern Object[] allAssets
		{
			[NativeMethod("GetAllLoadedAssets")]
			[MethodImpl(4096)]
			get;
		}
	}
}
