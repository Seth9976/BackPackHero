using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200004B RID: 75
public class DigitalCursorSelectOnEnable : MonoBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x0000966C File Offset: 0x0000786C
	private void OnEnable()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.Select());
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00009681 File Offset: 0x00007881
	private IEnumerator Select()
	{
		yield return new WaitForEndOfFrame();
		if (this.selectRightAway)
		{
			Vector3[] array = new Vector3[4];
			if (!EventSystem.current || !EventSystem.current.currentSelectedGameObject)
			{
				yield break;
			}
			EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().GetWorldCorners(array);
			Vector3 vector = array[3] + Vector3.left * 0.25f + Vector3.up * 0.25f;
			DigitalCursor.main.transform.position = vector;
			DigitalCursor.main.SetPosition(vector);
		}
		DigitalCursor.main.SelectUIElement(base.gameObject);
		yield break;
	}

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	private bool selectRightAway;
}
