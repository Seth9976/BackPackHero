using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000057 RID: 87
public class MapPlayer : MonoBehaviour
{
	// Token: 0x06000288 RID: 648 RVA: 0x0000D487 File Offset: 0x0000B687
	private void OnEnable()
	{
		MapPlayer.instance = this;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000D48F File Offset: 0x0000B68F
	private void OnDisable()
	{
		MapPlayer.instance = null;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000D497 File Offset: 0x0000B697
	private void Start()
	{
		this.image.sprite = Singleton.instance.selectedCharacter.mapIcon;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
	private void Update()
	{
		if (!this.currentEvent)
		{
			return;
		}
		base.transform.position = Vector2.Lerp(base.transform.position, this.currentEvent.transform.position, 10f * Time.deltaTime);
	}

	// Token: 0x040001E0 RID: 480
	public static MapPlayer instance;

	// Token: 0x040001E1 RID: 481
	public MapEvent currentEvent;

	// Token: 0x040001E2 RID: 482
	[SerializeField]
	private Image image;
}
