using System;
using System.Collections.Generic;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000311 RID: 785
	internal struct RenderChainVEData
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0006A504 File Offset: 0x00068704
		internal RenderChainCommand lastClosingOrLastCommand
		{
			get
			{
				return this.lastClosingCommand ?? this.lastCommand;
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006A528 File Offset: 0x00068728
		internal static bool AllocatesID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Owned && alloc.IsValid();
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0006A550 File Offset: 0x00068750
		internal static bool InheritsID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x04000B5E RID: 2910
		internal VisualElement prev;

		// Token: 0x04000B5F RID: 2911
		internal VisualElement next;

		// Token: 0x04000B60 RID: 2912
		internal VisualElement groupTransformAncestor;

		// Token: 0x04000B61 RID: 2913
		internal VisualElement boneTransformAncestor;

		// Token: 0x04000B62 RID: 2914
		internal VisualElement prevDirty;

		// Token: 0x04000B63 RID: 2915
		internal VisualElement nextDirty;

		// Token: 0x04000B64 RID: 2916
		internal int hierarchyDepth;

		// Token: 0x04000B65 RID: 2917
		internal RenderDataDirtyTypes dirtiedValues;

		// Token: 0x04000B66 RID: 2918
		internal uint dirtyID;

		// Token: 0x04000B67 RID: 2919
		internal RenderChainCommand firstCommand;

		// Token: 0x04000B68 RID: 2920
		internal RenderChainCommand lastCommand;

		// Token: 0x04000B69 RID: 2921
		internal RenderChainCommand firstClosingCommand;

		// Token: 0x04000B6A RID: 2922
		internal RenderChainCommand lastClosingCommand;

		// Token: 0x04000B6B RID: 2923
		internal bool isInChain;

		// Token: 0x04000B6C RID: 2924
		internal bool isHierarchyHidden;

		// Token: 0x04000B6D RID: 2925
		internal bool localFlipsWinding;

		// Token: 0x04000B6E RID: 2926
		internal bool localTransformScaleZero;

		// Token: 0x04000B6F RID: 2927
		internal bool worldFlipsWinding;

		// Token: 0x04000B70 RID: 2928
		internal ClipMethod clipMethod;

		// Token: 0x04000B71 RID: 2929
		internal int childrenStencilRef;

		// Token: 0x04000B72 RID: 2930
		internal int childrenMaskDepth;

		// Token: 0x04000B73 RID: 2931
		internal bool disableNudging;

		// Token: 0x04000B74 RID: 2932
		internal bool usesLegacyText;

		// Token: 0x04000B75 RID: 2933
		internal MeshHandle data;

		// Token: 0x04000B76 RID: 2934
		internal MeshHandle closingData;

		// Token: 0x04000B77 RID: 2935
		internal Matrix4x4 verticesSpace;

		// Token: 0x04000B78 RID: 2936
		internal int displacementUVStart;

		// Token: 0x04000B79 RID: 2937
		internal int displacementUVEnd;

		// Token: 0x04000B7A RID: 2938
		internal BMPAlloc transformID;

		// Token: 0x04000B7B RID: 2939
		internal BMPAlloc clipRectID;

		// Token: 0x04000B7C RID: 2940
		internal BMPAlloc opacityID;

		// Token: 0x04000B7D RID: 2941
		internal BMPAlloc textCoreSettingsID;

		// Token: 0x04000B7E RID: 2942
		internal BMPAlloc backgroundColorID;

		// Token: 0x04000B7F RID: 2943
		internal BMPAlloc borderLeftColorID;

		// Token: 0x04000B80 RID: 2944
		internal BMPAlloc borderTopColorID;

		// Token: 0x04000B81 RID: 2945
		internal BMPAlloc borderRightColorID;

		// Token: 0x04000B82 RID: 2946
		internal BMPAlloc borderBottomColorID;

		// Token: 0x04000B83 RID: 2947
		internal BMPAlloc tintColorID;

		// Token: 0x04000B84 RID: 2948
		internal float compositeOpacity;

		// Token: 0x04000B85 RID: 2949
		internal Color backgroundColor;

		// Token: 0x04000B86 RID: 2950
		internal VisualElement prevText;

		// Token: 0x04000B87 RID: 2951
		internal VisualElement nextText;

		// Token: 0x04000B88 RID: 2952
		internal List<RenderChainTextEntry> textEntries;

		// Token: 0x04000B89 RID: 2953
		internal BasicNode<TextureEntry> textures;
	}
}
