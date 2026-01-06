using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000069 RID: 105
public class PickUpTimer : MonoBehaviour
{
	// Token: 0x060002FA RID: 762 RVA: 0x0000F374 File Offset: 0x0000D574
	private void Start()
	{
		this.pickUpTimerSlider = Object.Instantiate<GameObject>(this.pickUpTimerSliderPrefab, CanvasManager.instance.masterContentScaler);
		this.pickUpTimerSlider.transform.SetAsFirstSibling();
		this.currentTime = this.timeAllowed;
		this.slider = this.pickUpTimerSlider.GetComponentInChildren<Slider>();
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000F3CC File Offset: 0x0000D5CC
	private void Update()
	{
		if (this.pickUpTimerSlider != null)
		{
			Vector2 vector = Camera.main.WorldToScreenPoint(this.sliderPosition.position);
			this.pickUpTimerSlider.transform.position = vector;
			this.slider.value = this.currentTime / this.timeAllowed;
			this.currentTime -= Time.deltaTime * TimeManager.instance.currentTimeScale;
			if (this.currentTime <= 0f)
			{
				Object.Destroy(this.pickUpTimerSlider);
				this.simpleAnimator.PlayAnimation("despawn");
				SimpleAnimator simpleAnimator = this.simpleAnimator;
				simpleAnimator.onAnimationEnd = (SimpleAnimator.AnimationEvent)Delegate.Combine(simpleAnimator.onAnimationEnd, new SimpleAnimator.AnimationEvent(this.DestroySelf));
			}
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000F49F File Offset: 0x0000D69F
	private void DestroySelf()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000F4AC File Offset: 0x0000D6AC
	private void OnDestroy()
	{
		if (this.pickUpTimerSlider != null)
		{
			Object.Destroy(this.pickUpTimerSlider);
		}
	}

	// Token: 0x04000246 RID: 582
	[SerializeField]
	private Transform sliderPosition;

	// Token: 0x04000247 RID: 583
	[SerializeField]
	private GameObject pickUpTimerSliderPrefab;

	// Token: 0x04000248 RID: 584
	[SerializeField]
	private float timeAllowed = 3f;

	// Token: 0x04000249 RID: 585
	private float currentTime;

	// Token: 0x0400024A RID: 586
	[SerializeField]
	private SimpleAnimator simpleAnimator;

	// Token: 0x0400024B RID: 587
	private GameObject pickUpTimerSlider;

	// Token: 0x0400024C RID: 588
	private Slider slider;
}
