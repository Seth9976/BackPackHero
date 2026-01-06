using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200043A RID: 1082
	[AssetFileNameExtension("playable", new string[] { })]
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableAsset : ScriptableObject, IPlayableAsset
	{
		// Token: 0x06002582 RID: 9602
		public abstract Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0003F330 File Offset: 0x0003D530
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0003F348 File Offset: 0x0003D548
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0003F360 File Offset: 0x0003D560
		[RequiredByNativeCode]
		internal unsafe static void Internal_CreatePlayable(PlayableAsset asset, PlayableGraph graph, GameObject go, IntPtr ptr)
		{
			bool flag = asset == null;
			Playable playable;
			if (flag)
			{
				playable = Playable.Null;
			}
			else
			{
				playable = asset.CreatePlayable(graph, go);
			}
			Playable* ptr2 = (Playable*)ptr.ToPointer();
			*ptr2 = playable;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0003F39C File Offset: 0x0003D59C
		[RequiredByNativeCode]
		internal unsafe static void Internal_GetPlayableAssetDuration(PlayableAsset asset, IntPtr ptrToDouble)
		{
			double duration = asset.duration;
			double* ptr = (double*)ptrToDouble.ToPointer();
			*ptr = duration;
		}
	}
}
