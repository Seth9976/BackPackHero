using System;
using System.Collections.Generic;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200003E RID: 62
	internal class TexturePool : RenderGraphResourcePool<RTHandle>
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000C44C File Offset: 0x0000A64C
		protected override void ReleaseInternalResource(RTHandle res)
		{
			res.Release();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C454 File Offset: 0x0000A654
		protected override string GetResourceName(RTHandle res)
		{
			return res.rt.name;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C461 File Offset: 0x0000A661
		protected override long GetResourceSize(RTHandle res)
		{
			return Profiler.GetRuntimeMemorySizeLong(res.rt);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C46E File Offset: 0x0000A66E
		protected override string GetResourceTypeName()
		{
			return "Texture";
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C475 File Offset: 0x0000A675
		protected override int GetSortIndex(RTHandle res)
		{
			return res.GetInstanceID();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C480 File Offset: 0x0000A680
		public override void PurgeUnusedResources(int currentFrameIndex)
		{
			RenderGraphResourcePool<RTHandle>.s_CurrentFrameIndex = currentFrameIndex;
			this.m_RemoveList.Clear();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<RTHandle, int>>> keyValuePair in this.m_ResourcePool)
			{
				SortedList<int, ValueTuple<RTHandle, int>> value = keyValuePair.Value;
				IList<int> keys = value.Keys;
				IList<ValueTuple<RTHandle, int>> values = value.Values;
				for (int i = 0; i < value.Count; i++)
				{
					ValueTuple<RTHandle, int> valueTuple = values[i];
					if (RenderGraphResourcePool<RTHandle>.ShouldReleaseResource(valueTuple.Item2, RenderGraphResourcePool<RTHandle>.s_CurrentFrameIndex))
					{
						valueTuple.Item1.Release();
						this.m_RemoveList.Add(keys[i]);
					}
				}
				foreach (int num in this.m_RemoveList)
				{
					value.Remove(num);
				}
			}
		}
	}
}
