using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class FindCanvas : MonoBehaviour
{
	// Token: 0x06000188 RID: 392 RVA: 0x00009FA8 File Offset: 0x000081A8
	private void Start()
	{
		if (this.canvasTag == "")
		{
			this.canvasTag = "UI Canvas";
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag(this.canvasTag);
		Canvas canvas;
		if (gameObject)
		{
			canvas = gameObject.GetComponent<Canvas>();
			if (canvas)
			{
				base.transform.SetParent(canvas.transform, false);
				base.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		canvas = Object.FindObjectOfType<Canvas>();
		if (canvas)
		{
			base.transform.SetParent(canvas.transform, false);
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	// Token: 0x04000100 RID: 256
	[SerializeField]
	private string canvasTag = "UI Canvas";
}
