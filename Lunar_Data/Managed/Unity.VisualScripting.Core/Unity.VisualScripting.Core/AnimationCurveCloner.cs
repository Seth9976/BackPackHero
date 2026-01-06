using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000003 RID: 3
	public sealed class AnimationCurveCloner : Cloner<AnimationCurve>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020CD File Offset: 0x000002CD
		public override bool Handles(Type type)
		{
			return type == typeof(AnimationCurve);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020DF File Offset: 0x000002DF
		public override AnimationCurve ConstructClone(Type type, AnimationCurve original)
		{
			return new AnimationCurve();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E8 File Offset: 0x000002E8
		public override void FillClone(Type type, ref AnimationCurve clone, AnimationCurve original, CloningContext context)
		{
			for (int i = 0; i < clone.length; i++)
			{
				clone.RemoveKey(i);
			}
			foreach (Keyframe keyframe in original.keys)
			{
				clone.AddKey(keyframe);
			}
		}
	}
}
