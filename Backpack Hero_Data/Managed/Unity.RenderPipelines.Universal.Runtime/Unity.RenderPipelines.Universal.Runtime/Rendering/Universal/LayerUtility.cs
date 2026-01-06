using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002C RID: 44
	internal static class LayerUtility
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000E5B3 File Offset: 0x0000C7B3
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000E5BA File Offset: 0x0000C7BA
		public static uint maxTextureCount { get; private set; }

		// Token: 0x06000191 RID: 401 RVA: 0x0000E5C2 File Offset: 0x0000C7C2
		public static void InitializeBudget(uint maxTextureCount)
		{
			LayerUtility.maxTextureCount = math.max(4U, maxTextureCount);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		private static bool CanBatchLightsInLayer(int layerIndex1, int layerIndex2, SortingLayer[] sortingLayers, ILight2DCullResult lightCullResult)
		{
			int id = sortingLayers[layerIndex1].id;
			int id2 = sortingLayers[layerIndex2].id;
			foreach (Light2D light2D in lightCullResult.visibleLights)
			{
				bool flag = light2D.lightType != Light2D.LightType.Global && light2D.shadowsEnabled;
				if (light2D.IsLitLayer(id) != light2D.IsLitLayer(id2) || (light2D.IsLitLayer(id) && flag))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000E670 File Offset: 0x0000C870
		private static int FindUpperBoundInBatch(int startLayerIndex, SortingLayer[] sortingLayers, ILight2DCullResult lightCullResult)
		{
			for (int i = startLayerIndex + 1; i < sortingLayers.Length; i++)
			{
				if (!LayerUtility.CanBatchLightsInLayer(startLayerIndex, i, sortingLayers, lightCullResult))
				{
					return i - 1;
				}
			}
			return sortingLayers.Length - 1;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		private static void InitializeBatchInfos(SortingLayer[] cachedSortingLayers)
		{
			int num = cachedSortingLayers.Length;
			bool flag = LayerUtility.s_LayerBatches == null;
			if (LayerUtility.s_LayerBatches == null)
			{
				LayerUtility.s_LayerBatches = new LayerBatch[num];
			}
			if (flag)
			{
				for (int i = 0; i < LayerUtility.s_LayerBatches.Length; i++)
				{
					LayerUtility.s_LayerBatches[i].InitRTIds(i);
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		public static LayerBatch[] CalculateBatches(ILight2DCullResult lightCullResult, out int batchCount)
		{
			SortingLayer[] cachedSortingLayer = Light2DManager.GetCachedSortingLayer();
			LayerUtility.InitializeBatchInfos(cachedSortingLayer);
			batchCount = 0;
			int num3;
			for (int i = 0; i < cachedSortingLayer.Length; i = num3 + 1)
			{
				int id = cachedSortingLayer[i].id;
				LightStats lightStatsByLayer = lightCullResult.GetLightStatsByLayer(id);
				LayerBatch[] array = LayerUtility.s_LayerBatches;
				int num = batchCount;
				batchCount = num + 1;
				int num2 = num;
				num3 = LayerUtility.FindUpperBoundInBatch(i, cachedSortingLayer, lightCullResult);
				short num4 = (short)cachedSortingLayer[i].value;
				short num5 = ((i == 0) ? short.MinValue : num4);
				short num6 = (short)cachedSortingLayer[num3].value;
				short num7 = ((num3 == cachedSortingLayer.Length - 1) ? short.MaxValue : num6);
				SortingLayerRange sortingLayerRange = new SortingLayerRange(num5, num7);
				array[num2].startLayerID = id;
				array[num2].endLayerValue = (int)num6;
				array[num2].layerRange = sortingLayerRange;
				array[num2].lightStats = lightStatsByLayer;
			}
			return LayerUtility.s_LayerBatches;
		}

		// Token: 0x040000F6 RID: 246
		private static LayerBatch[] s_LayerBatches;
	}
}
