using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000147 RID: 327
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Renderer.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[UsedByNativeCode]
	public class Renderer : Component
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00010338 File Offset: 0x0000E538
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00010353 File Offset: 0x0000E553
		[EditorBrowsable(1)]
		[Obsolete("Use shadowCastingMode instead.", false)]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.On : ShadowCastingMode.Off);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00010364 File Offset: 0x0000E564
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0001037F File Offset: 0x0000E57F
		[Obsolete("Use motionVectorGenerationMode instead.", false)]
		public bool motionVectors
		{
			get
			{
				return this.motionVectorGenerationMode == MotionVectorGenerationMode.Object;
			}
			set
			{
				this.motionVectorGenerationMode = (value ? MotionVectorGenerationMode.Object : MotionVectorGenerationMode.Camera);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00010390 File Offset: 0x0000E590
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x000103AB File Offset: 0x0000E5AB
		[Obsolete("Use lightProbeUsage instead.", false)]
		public bool useLightProbes
		{
			get
			{
				return this.lightProbeUsage > LightProbeUsage.Off;
			}
			set
			{
				this.lightProbeUsage = (value ? LightProbeUsage.BlendProbes : LightProbeUsage.Off);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000103BC File Offset: 0x0000E5BC
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x000103D2 File Offset: 0x0000E5D2
		public Bounds bounds
		{
			[FreeFunction(Name = "RendererScripting::GetWorldBounds", HasExplicitThis = true)]
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
			[NativeName("SetWorldAABB")]
			set
			{
				this.set_bounds_Injected(ref value);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000103DC File Offset: 0x0000E5DC
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x000103F2 File Offset: 0x0000E5F2
		public Bounds localBounds
		{
			[FreeFunction(Name = "RendererScripting::GetLocalBounds", HasExplicitThis = true)]
			get
			{
				Bounds bounds;
				this.get_localBounds_Injected(out bounds);
				return bounds;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		// Token: 0x06000B9E RID: 2974
		[NativeName("ResetWorldAABB")]
		[MethodImpl(4096)]
		public extern void ResetBounds();

		// Token: 0x06000B9F RID: 2975
		[NativeName("ResetLocalAABB")]
		[MethodImpl(4096)]
		public extern void ResetLocalBounds();

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000103FC File Offset: 0x0000E5FC
		[FreeFunction(Name = "RendererScripting::SetStaticLightmapST", HasExplicitThis = true)]
		private void SetStaticLightmapST(Vector4 st)
		{
			this.SetStaticLightmapST_Injected(ref st);
		}

		// Token: 0x06000BA1 RID: 2977
		[FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material GetMaterial();

		// Token: 0x06000BA2 RID: 2978
		[FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material GetSharedMaterial();

		// Token: 0x06000BA3 RID: 2979
		[FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMaterial(Material m);

		// Token: 0x06000BA4 RID: 2980
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Material[] GetMaterialArray();

		// Token: 0x06000BA5 RID: 2981
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CopyMaterialArray([Out] Material[] m);

		// Token: 0x06000BA6 RID: 2982
		[FreeFunction(Name = "RendererScripting::GetSharedMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CopySharedMaterialArray([Out] Material[] m);

		// Token: 0x06000BA7 RID: 2983
		[FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMaterialArray([NotNull("ArgumentNullException")] Material[] m);

		// Token: 0x06000BA8 RID: 2984
		[FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_SetPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x06000BA9 RID: 2985
		[FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_GetPropertyBlock([NotNull("ArgumentNullException")] MaterialPropertyBlock dest);

		// Token: 0x06000BAA RID: 2986
		[FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex);

		// Token: 0x06000BAB RID: 2987
		[FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void Internal_GetPropertyBlockMaterialIndex([NotNull("ArgumentNullException")] MaterialPropertyBlock dest, int materialIndex);

		// Token: 0x06000BAC RID: 2988
		[FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasPropertyBlock();

		// Token: 0x06000BAD RID: 2989 RVA: 0x00010406 File Offset: 0x0000E606
		public void SetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_SetPropertyBlock(properties);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00010411 File Offset: 0x0000E611
		public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_SetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001041D File Offset: 0x0000E61D
		public void GetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_GetPropertyBlock(properties);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00010428 File Offset: 0x0000E628
		public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_GetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000BB1 RID: 2993
		[FreeFunction(Name = "RendererScripting::GetClosestReflectionProbes", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetClosestReflectionProbesInternal(object result);

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BB2 RID: 2994
		// (set) Token: 0x06000BB3 RID: 2995
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BB4 RID: 2996
		public extern bool isVisible
		{
			[NativeName("IsVisibleInScene")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BB5 RID: 2997
		// (set) Token: 0x06000BB6 RID: 2998
		public extern ShadowCastingMode shadowCastingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BB7 RID: 2999
		// (set) Token: 0x06000BB8 RID: 3000
		public extern bool receiveShadows
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BB9 RID: 3001
		// (set) Token: 0x06000BBA RID: 3002
		public extern bool forceRenderingOff
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000BBB RID: 3003
		[NativeName("GetIsStaticShadowCaster")]
		[MethodImpl(4096)]
		private extern bool GetIsStaticShadowCaster();

		// Token: 0x06000BBC RID: 3004
		[NativeName("SetIsStaticShadowCaster")]
		[MethodImpl(4096)]
		private extern void SetIsStaticShadowCaster(bool value);

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00010434 File Offset: 0x0000E634
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0001044C File Offset: 0x0000E64C
		public bool staticShadowCaster
		{
			get
			{
				return this.GetIsStaticShadowCaster();
			}
			set
			{
				this.SetIsStaticShadowCaster(value);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BBF RID: 3007
		// (set) Token: 0x06000BC0 RID: 3008
		public extern MotionVectorGenerationMode motionVectorGenerationMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BC1 RID: 3009
		// (set) Token: 0x06000BC2 RID: 3010
		public extern LightProbeUsage lightProbeUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BC3 RID: 3011
		// (set) Token: 0x06000BC4 RID: 3012
		public extern ReflectionProbeUsage reflectionProbeUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BC5 RID: 3013
		// (set) Token: 0x06000BC6 RID: 3014
		public extern uint renderingLayerMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BC7 RID: 3015
		// (set) Token: 0x06000BC8 RID: 3016
		public extern int rendererPriority
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BC9 RID: 3017
		// (set) Token: 0x06000BCA RID: 3018
		public extern RayTracingMode rayTracingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BCB RID: 3019
		// (set) Token: 0x06000BCC RID: 3020
		public extern string sortingLayerName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BCD RID: 3021
		// (set) Token: 0x06000BCE RID: 3022
		public extern int sortingLayerID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000BCF RID: 3023
		// (set) Token: 0x06000BD0 RID: 3024
		public extern int sortingOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000BD1 RID: 3025
		// (set) Token: 0x06000BD2 RID: 3026
		internal extern int sortingGroupID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000BD3 RID: 3027
		// (set) Token: 0x06000BD4 RID: 3028
		internal extern int sortingGroupOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000BD5 RID: 3029
		// (set) Token: 0x06000BD6 RID: 3030
		[NativeProperty("IsDynamicOccludee")]
		public extern bool allowOcclusionWhenDynamic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000BD7 RID: 3031
		// (set) Token: 0x06000BD8 RID: 3032
		[NativeProperty("StaticBatchRoot")]
		internal extern Transform staticBatchRootTransform
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000BD9 RID: 3033
		internal extern int staticBatchIndex
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000BDA RID: 3034
		[MethodImpl(4096)]
		internal extern void SetStaticBatchInfo(int firstSubMesh, int subMeshCount);

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000BDB RID: 3035
		public extern bool isPartOfStaticBatch
		{
			[NativeName("IsPartOfStaticBatch")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00010458 File Offset: 0x0000E658
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_worldToLocalMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00010470 File Offset: 0x0000E670
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_localToWorldMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000BDE RID: 3038
		// (set) Token: 0x06000BDF RID: 3039
		public extern GameObject lightProbeProxyVolumeOverride
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BE0 RID: 3040
		// (set) Token: 0x06000BE1 RID: 3041
		public extern Transform probeAnchor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000BE2 RID: 3042
		[NativeName("GetLightmapIndexInt")]
		[MethodImpl(4096)]
		private extern int GetLightmapIndex(LightmapType lt);

		// Token: 0x06000BE3 RID: 3043
		[NativeName("SetLightmapIndexInt")]
		[MethodImpl(4096)]
		private extern void SetLightmapIndex(int index, LightmapType lt);

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00010488 File Offset: 0x0000E688
		[NativeName("GetLightmapST")]
		private Vector4 GetLightmapST(LightmapType lt)
		{
			Vector4 vector;
			this.GetLightmapST_Injected(lt, out vector);
			return vector;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001049F File Offset: 0x0000E69F
		[NativeName("SetLightmapST")]
		private void SetLightmapST(Vector4 st, LightmapType lt)
		{
			this.SetLightmapST_Injected(ref st, lt);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000104AC File Offset: 0x0000E6AC
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000104C5 File Offset: 0x0000E6C5
		public int lightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.StaticLightmap);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000104D4 File Offset: 0x0000E6D4
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x000104ED File Offset: 0x0000E6ED
		public int realtimeLightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000104FC File Offset: 0x0000E6FC
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00010515 File Offset: 0x0000E715
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetStaticLightmapST(value);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00010520 File Offset: 0x0000E720
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00010539 File Offset: 0x0000E739
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapST(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x06000BEE RID: 3054
		[MethodImpl(4096)]
		private extern int GetMaterialCount();

		// Token: 0x06000BEF RID: 3055
		[NativeName("GetMaterialArray")]
		[MethodImpl(4096)]
		private extern Material[] GetSharedMaterialArray();

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00010548 File Offset: 0x0000E748
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00010560 File Offset: 0x0000E760
		public Material[] materials
		{
			get
			{
				return this.GetMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0001056C File Offset: 0x0000E76C
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00010584 File Offset: 0x0000E784
		public Material material
		{
			get
			{
				return this.GetMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00010590 File Offset: 0x0000E790
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00010584 File Offset: 0x0000E784
		public Material sharedMaterial
		{
			get
			{
				return this.GetSharedMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000105A8 File Offset: 0x0000E7A8
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x00010560 File Offset: 0x0000E760
		public Material[] sharedMaterials
		{
			get
			{
				return this.GetSharedMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public void GetMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopyMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00010600 File Offset: 0x0000E800
		public void GetSharedMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopySharedMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00010640 File Offset: 0x0000E840
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}

		// Token: 0x06000BFC RID: 3068
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000BFD RID: 3069
		[MethodImpl(4096)]
		private extern void set_bounds_Injected(ref Bounds value);

		// Token: 0x06000BFE RID: 3070
		[MethodImpl(4096)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x06000BFF RID: 3071
		[MethodImpl(4096)]
		private extern void set_localBounds_Injected(ref Bounds value);

		// Token: 0x06000C00 RID: 3072
		[MethodImpl(4096)]
		private extern void SetStaticLightmapST_Injected(ref Vector4 st);

		// Token: 0x06000C01 RID: 3073
		[MethodImpl(4096)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000C02 RID: 3074
		[MethodImpl(4096)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000C03 RID: 3075
		[MethodImpl(4096)]
		private extern void GetLightmapST_Injected(LightmapType lt, out Vector4 ret);

		// Token: 0x06000C04 RID: 3076
		[MethodImpl(4096)]
		private extern void SetLightmapST_Injected(ref Vector4 st, LightmapType lt);
	}
}
