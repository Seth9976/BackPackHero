using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000058 RID: 88
public class MapSelector : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x0600028D RID: 653 RVA: 0x0000D51C File Offset: 0x0000B71C
	private void OnEnable()
	{
		MapSelector.instance = this;
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000D524 File Offset: 0x0000B724
	private void OnDisable()
	{
		MapSelector.instance = null;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000D52C File Offset: 0x0000B72C
	private void Start()
	{
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000D530 File Offset: 0x0000B730
	private void Update()
	{
		if (!this.selectedObject)
		{
			this.selectorImage.enabled = false;
			return;
		}
		this.selectorImage.enabled = true;
		base.transform.position = Vector2.Lerp(base.transform.position, this.selectedObject.transform.position, 25f * Time.deltaTime);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
	public void SetSelectedObject(GameObject obj)
	{
		if (!obj)
		{
			return;
		}
		this.selectedObject = obj;
		MapEvent component = obj.GetComponent<MapEvent>();
		if (component)
		{
			MapDescriptorManager.instance.SetEvent(component);
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000D5DF File Offset: 0x0000B7DF
	public MapEvent GetCurrentEvent()
	{
		if (!this.selectedObject)
		{
			return null;
		}
		return this.selectedObject.GetComponent<MapEvent>();
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000D5FB File Offset: 0x0000B7FB
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!this.selectedObject)
		{
			return;
		}
		Map.instance.AcceptRoom();
	}

	// Token: 0x040001E3 RID: 483
	public static MapSelector instance;

	// Token: 0x040001E4 RID: 484
	[SerializeField]
	public GameObject selectedObject;

	// Token: 0x040001E5 RID: 485
	[SerializeField]
	private Image selectorImage;
}
