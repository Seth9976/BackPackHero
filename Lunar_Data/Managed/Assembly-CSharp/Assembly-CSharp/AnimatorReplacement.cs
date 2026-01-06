using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class AnimatorReplacement : MonoBehaviour
{
	// Token: 0x06000016 RID: 22 RVA: 0x000024E8 File Offset: 0x000006E8
	private void Start()
	{
		if (!this.simpleAnimator)
		{
			Debug.LogError("SimpleAnimator is not assigned in " + base.gameObject.name);
			return;
		}
		int num = Random.Range(0, this.animations.Count);
		this.simpleAnimator.SetSprites(this.animations[num].sprites);
	}

	// Token: 0x04000009 RID: 9
	[SerializeField]
	private SimpleAnimator simpleAnimator;

	// Token: 0x0400000A RID: 10
	[SerializeField]
	private List<AnimatorReplacement.Animation> animations = new List<AnimatorReplacement.Animation>();

	// Token: 0x020000B6 RID: 182
	[Serializable]
	private class Animation
	{
		// Token: 0x040003B1 RID: 945
		public string name;

		// Token: 0x040003B2 RID: 946
		public List<Sprite> sprites;
	}
}
