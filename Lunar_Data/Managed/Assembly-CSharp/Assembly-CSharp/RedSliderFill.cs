using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class RedSliderFill : MonoBehaviour
{
	// Token: 0x06000363 RID: 867 RVA: 0x00010F8F File Offset: 0x0000F18F
	private void Start()
	{
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00010F94 File Offset: 0x0000F194
	private void Update()
	{
		float healthPercentage = this.healthBarMaster.GetHealthPercentage();
		this.redSliderFill.anchorMin = new Vector2(0f, this.redSliderFill.anchorMin.y);
		Vector2 vector = new Vector2(healthPercentage, this.redSliderFill.anchorMax.y);
		RedSliderFill.Direction direction = this.direction;
		if (direction != RedSliderFill.Direction.Left)
		{
			if (direction != RedSliderFill.Direction.Right)
			{
				return;
			}
			if (vector.x > this.redSliderFill.anchorMax.x)
			{
				this.redSliderFill.anchorMax = Vector2.Lerp(this.redSliderFill.anchorMax, vector, Time.deltaTime * this.speed);
				return;
			}
			this.redSliderFill.anchorMax = this.standardSliderFillToCopy.anchorMax;
			return;
		}
		else
		{
			if (vector.x < this.redSliderFill.anchorMax.x)
			{
				this.redSliderFill.anchorMax = Vector2.Lerp(this.redSliderFill.anchorMax, vector, Time.deltaTime * this.speed);
				return;
			}
			this.redSliderFill.anchorMax = this.standardSliderFillToCopy.anchorMax;
			return;
		}
	}

	// Token: 0x04000293 RID: 659
	public RedSliderFill.Direction direction;

	// Token: 0x04000294 RID: 660
	[SerializeField]
	private RectTransform redSliderFill;

	// Token: 0x04000295 RID: 661
	[SerializeField]
	private RectTransform standardSliderFillToCopy;

	// Token: 0x04000296 RID: 662
	[SerializeField]
	private HealthBarMaster healthBarMaster;

	// Token: 0x04000297 RID: 663
	[SerializeField]
	private float speed = 10f;

	// Token: 0x02000104 RID: 260
	public enum Direction
	{
		// Token: 0x040004C3 RID: 1219
		Left,
		// Token: 0x040004C4 RID: 1220
		Right
	}
}
