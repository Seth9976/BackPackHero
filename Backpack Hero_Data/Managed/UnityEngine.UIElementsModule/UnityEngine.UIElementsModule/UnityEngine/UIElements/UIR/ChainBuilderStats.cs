using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000309 RID: 777
	internal struct ChainBuilderStats
	{
		// Token: 0x04000AF6 RID: 2806
		public uint elementsAdded;

		// Token: 0x04000AF7 RID: 2807
		public uint elementsRemoved;

		// Token: 0x04000AF8 RID: 2808
		public uint recursiveClipUpdates;

		// Token: 0x04000AF9 RID: 2809
		public uint recursiveClipUpdatesExpanded;

		// Token: 0x04000AFA RID: 2810
		public uint nonRecursiveClipUpdates;

		// Token: 0x04000AFB RID: 2811
		public uint recursiveTransformUpdates;

		// Token: 0x04000AFC RID: 2812
		public uint recursiveTransformUpdatesExpanded;

		// Token: 0x04000AFD RID: 2813
		public uint recursiveOpacityUpdates;

		// Token: 0x04000AFE RID: 2814
		public uint recursiveOpacityUpdatesExpanded;

		// Token: 0x04000AFF RID: 2815
		public uint colorUpdates;

		// Token: 0x04000B00 RID: 2816
		public uint colorUpdatesExpanded;

		// Token: 0x04000B01 RID: 2817
		public uint recursiveVisualUpdates;

		// Token: 0x04000B02 RID: 2818
		public uint recursiveVisualUpdatesExpanded;

		// Token: 0x04000B03 RID: 2819
		public uint nonRecursiveVisualUpdates;

		// Token: 0x04000B04 RID: 2820
		public uint dirtyProcessed;

		// Token: 0x04000B05 RID: 2821
		public uint nudgeTransformed;

		// Token: 0x04000B06 RID: 2822
		public uint boneTransformed;

		// Token: 0x04000B07 RID: 2823
		public uint skipTransformed;

		// Token: 0x04000B08 RID: 2824
		public uint visualUpdateTransformed;

		// Token: 0x04000B09 RID: 2825
		public uint updatedMeshAllocations;

		// Token: 0x04000B0A RID: 2826
		public uint newMeshAllocations;

		// Token: 0x04000B0B RID: 2827
		public uint groupTransformElementsChanged;

		// Token: 0x04000B0C RID: 2828
		public uint immedateRenderersActive;

		// Token: 0x04000B0D RID: 2829
		public uint textUpdates;
	}
}
