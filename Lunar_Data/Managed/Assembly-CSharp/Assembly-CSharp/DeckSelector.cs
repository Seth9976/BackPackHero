using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000028 RID: 40
public class DeckSelector : MonoBehaviour
{
	// Token: 0x06000130 RID: 304 RVA: 0x0000738F File Offset: 0x0000558F
	private void OnEnable()
	{
		DeckSelector.instance = this;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00007397 File Offset: 0x00005597
	private void OnDisable()
	{
		DeckSelector.instance = null;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000739F File Offset: 0x0000559F
	private void Start()
	{
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000073A4 File Offset: 0x000055A4
	private void Update()
	{
		if (!this.selectedObject)
		{
			this.selectorImage.enabled = false;
			return;
		}
		this.selectorImage.enabled = true;
		base.transform.position = Vector2.Lerp(base.transform.position, this.selectedObject.transform.position, 100f * Time.deltaTime);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000741C File Offset: 0x0000561C
	public void SetSelectedObject(GameObject obj)
	{
		if (!obj)
		{
			return;
		}
		this.selectedObject = obj;
		DeckCard component = obj.GetComponent<DeckCard>();
		if (component)
		{
			base.GetComponentInParent<DeckPanel>().ShowCardInfo(component);
		}
	}

	// Token: 0x040000E8 RID: 232
	public static DeckSelector instance;

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	public GameObject selectedObject;

	// Token: 0x040000EA RID: 234
	[SerializeField]
	private Image selectorImage;
}
