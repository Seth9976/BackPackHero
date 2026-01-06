using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000090 RID: 144
public class SimpleAnimator : MonoBehaviour
{
	// Token: 0x060003B9 RID: 953 RVA: 0x000126AD File Offset: 0x000108AD
	private void Start()
	{
		if (this.randomizeStartFrame)
		{
			this.timeForFrame = Random.Range(0f, this.secondsPerFrame);
			this.frameNumber = Random.Range(0, this.sprites.Length);
		}
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000126E4 File Offset: 0x000108E4
	private void Update()
	{
		if (this.sprites.Length == 0)
		{
			return;
		}
		float num = 1f;
		if (this.timeType == SimpleAnimator.TimeType.TimeManagerScaled)
		{
			num = TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == SimpleAnimator.TimeType.Unscaled)
		{
			num = 1f;
		}
		else if (this.timeType == SimpleAnimator.TimeType.TimeManagerLerp)
		{
			num = this.currentTimeLerp;
			this.currentTimeLerp = Mathf.Lerp(this.currentTimeLerp, TimeManager.instance.currentTimeScale, Time.deltaTime * this.speedOfLerp);
			if (Mathf.Abs(this.currentTimeLerp - TimeManager.instance.currentTimeScale) < 0.01f)
			{
				this.timeType = SimpleAnimator.TimeType.TimeManagerScaled;
			}
		}
		this.timeForFrame += Time.deltaTime * num;
		if (this.timeForFrame >= this.secondsPerFrame)
		{
			this.timeForFrame = 0f;
			this.frameNumber++;
			if (this.frameNumber >= this.sprites.Length)
			{
				if (this.loop)
				{
					this.frameNumber = 0;
				}
				SimpleAnimator.AnimationEvent animationEvent = this.onAnimationEnd;
				if (animationEvent != null)
				{
					animationEvent();
				}
			}
			if (this.frameNumber >= this.sprites.Length)
			{
				this.frameNumber = this.sprites.Length - 1;
			}
			if (this.spriteRenderer)
			{
				this.spriteRenderer.sprite = this.sprites[this.frameNumber];
			}
			if (this.image)
			{
				this.image.sprite = this.sprites[this.frameNumber];
			}
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0001285A File Offset: 0x00010A5A
	public void PlayAnimation(string x, SimpleAnimator.AnimationEvent onAnimationEnd)
	{
		this.onAnimationEnd = onAnimationEnd;
		this.PlayAnimation(x);
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001286C File Offset: 0x00010A6C
	public void PlayAnimation(string x)
	{
		if (this.animationName == x)
		{
			return;
		}
		for (int i = 0; i < this.animations.Length; i++)
		{
			if (Utils.GetPrefabName(this.animations[i].name) == Utils.GetPrefabName(x))
			{
				this.animationName = this.animations[i].name;
				this.sprites = this.animations[i].sprites;
				this.frameNumber = 0;
				return;
			}
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x000128E8 File Offset: 0x00010AE8
	public void SetSprites(List<Sprite> sprites)
	{
		this.sprites = sprites.ToArray();
	}

	// Token: 0x040002D6 RID: 726
	[SerializeField]
	public SimpleAnimator.TimeType timeType = SimpleAnimator.TimeType.TimeManagerLerp;

	// Token: 0x040002D7 RID: 727
	private ParticleSystem.MainModule main;

	// Token: 0x040002D8 RID: 728
	private float startingSpeed = 1f;

	// Token: 0x040002D9 RID: 729
	private float currentTimeLerp = 1f;

	// Token: 0x040002DA RID: 730
	[SerializeField]
	private float speedOfLerp = 10f;

	// Token: 0x040002DB RID: 731
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x040002DC RID: 732
	[SerializeField]
	private Image image;

	// Token: 0x040002DD RID: 733
	[SerializeField]
	public string animationName;

	// Token: 0x040002DE RID: 734
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x040002DF RID: 735
	[SerializeField]
	private float secondsPerFrame = 0.1f;

	// Token: 0x040002E0 RID: 736
	[SerializeField]
	private bool scaleWithTimeManager = true;

	// Token: 0x040002E1 RID: 737
	private float timeForFrame;

	// Token: 0x040002E2 RID: 738
	private int frameNumber;

	// Token: 0x040002E3 RID: 739
	public SimpleAnimator.AnimationEvent onAnimationEnd;

	// Token: 0x040002E4 RID: 740
	[SerializeField]
	private bool randomizeStartFrame = true;

	// Token: 0x040002E5 RID: 741
	[SerializeField]
	private bool loop = true;

	// Token: 0x040002E6 RID: 742
	[SerializeField]
	private SimpleAnimator.Animation[] animations;

	// Token: 0x0200010B RID: 267
	[SerializeField]
	public enum TimeType
	{
		// Token: 0x040004DF RID: 1247
		TimeManagerScaled,
		// Token: 0x040004E0 RID: 1248
		Unscaled,
		// Token: 0x040004E1 RID: 1249
		TimeManagerLerp
	}

	// Token: 0x0200010C RID: 268
	[Serializable]
	public class Animation
	{
		// Token: 0x040004E2 RID: 1250
		public string name;

		// Token: 0x040004E3 RID: 1251
		public Sprite[] sprites;
	}

	// Token: 0x0200010D RID: 269
	// (Invoke) Token: 0x060005B1 RID: 1457
	public delegate void AnimationEvent();
}
