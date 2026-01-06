using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000085 RID: 133
public class RunPanelCursor : MonoBehaviour
{
	// Token: 0x06000397 RID: 919 RVA: 0x00011E18 File Offset: 0x00010018
	private void Start()
	{
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00011E1C File Offset: 0x0001001C
	private void Update()
	{
		if (!EventSystem.current.currentSelectedGameObject)
		{
			this.image.enabled = false;
			return;
		}
		if (!ControllerSpriteManager.instance.isUsingController)
		{
			this.image.enabled = false;
		}
		else
		{
			this.image.enabled = true;
		}
		RectTransform component = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
		if (!component)
		{
			return;
		}
		Vector3[] array = new Vector3[4];
		component.GetWorldCorners(array);
		Vector2 vector = array[3];
		this.image.rectTransform.position = Vector2.MoveTowards(this.image.rectTransform.position, vector, 45f * Time.deltaTime);
	}

	// Token: 0x040002BD RID: 701
	[SerializeField]
	private Image image;
}
