using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000092 RID: 146
public class SingleUI : MonoBehaviour
{
	// Token: 0x060003D0 RID: 976 RVA: 0x00012E18 File Offset: 0x00011018
	public static bool IsViewingHideCursorPopUp()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		return SingleUI.singleUIs.Count != 0 && SingleUI.singleUIs[0].hideCursor;
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00012E88 File Offset: 0x00011088
	public static bool IsViewingPopUp()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		return SingleUI.singleUIs.Count != 0;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00012EE7 File Offset: 0x000110E7
	public static SingleUI GetSingleUI()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return SingleUI.singleUIs[0];
		}
		return null;
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00012F00 File Offset: 0x00011100
	public static Transform GetPopUp()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		if (SingleUI.singleUIs.Count == 0)
		{
			return null;
		}
		return SingleUI.singleUIs[0].transform;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00012F70 File Offset: 0x00011170
	public static void DestroyAll()
	{
		while (SingleUI.singleUIs.Count > 0)
		{
			if (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject)
			{
				SingleUI.singleUIs.RemoveAt(0);
			}
			else
			{
				Object gameObject = SingleUI.singleUIs[0].gameObject;
				SingleUI.singleUIs.RemoveAt(0);
				Object.Destroy(gameObject);
			}
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00012FE2 File Offset: 0x000111E2
	public void SetOpeningState(SingleUI.State state)
	{
		this.openState = state;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00012FEB File Offset: 0x000111EB
	public void SetClosingState(SingleUI.State state)
	{
		this.closeState = state;
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00012FF4 File Offset: 0x000111F4
	public void CloseAndDestroyViaFade()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(SingleUI.State.Fade);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00013004 File Offset: 0x00011204
	public void CloseAndDestroyViaAnimation()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(SingleUI.State.Animation);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00013014 File Offset: 0x00011214
	public void CloseAndDestroy()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(this.closeState);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00013029 File Offset: 0x00011229
	public void Close()
	{
		this.isSetForReenable = false;
		SingleUI.singleUIs.Remove(this);
		this.DisableThisUI(this.closeState);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001304A File Offset: 0x0001124A
	public void DestroyImmediate()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00013057 File Offset: 0x00011257
	private void OnApplicationQuit()
	{
		SingleUI.singleUIs.Clear();
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00013063 File Offset: 0x00011263
	private void OnDestroy()
	{
		if (!SingleUI.singleUIs.Contains(this))
		{
			return;
		}
		if (SingleUI.singleUIs[0] == this)
		{
			SingleUI.singleUIs.RemoveAt(0);
			SingleUI.EnableNextSingleUI();
			return;
		}
		SingleUI.singleUIs.Remove(this);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x000130A3 File Offset: 0x000112A3
	private void Start()
	{
		if (!this.canvasGroup)
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
		}
		if (!this.animator)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x000130D8 File Offset: 0x000112D8
	private void Update()
	{
		if (this.moveToZero)
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, Vector3.zero, Time.unscaledDeltaTime * 10f);
		}
		if (EventSystem.current && EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
		{
			this.lastSelected = EventSystem.current.currentSelectedGameObject;
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00013164 File Offset: 0x00011364
	private static void CheckList()
	{
		for (int i = 0; i < SingleUI.singleUIs.Count; i++)
		{
			if (!SingleUI.singleUIs[i] || !SingleUI.singleUIs[i].gameObject)
			{
				SingleUI.singleUIs.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x000131C0 File Offset: 0x000113C0
	private void OnEnable()
	{
		if (this.fadeCoroutine != null)
		{
			base.StopCoroutine(this.fadeCoroutine);
			this.fadeCoroutine = null;
		}
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
		}
		this.isSetForReenable = false;
		SingleUI.singleUIs.Remove(this);
		SingleUI.singleUIs.Insert(0, this);
		SingleUI.CheckList();
		foreach (SingleUI singleUI in SingleUI.singleUIs)
		{
			if (!(singleUI == this))
			{
				singleUI.DisableThisUI(singleUI.closeState);
			}
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00013288 File Offset: 0x00011488
	private void RemoveElement()
	{
		this.DisableThisUI(this.closeState);
		this.isSetForReenable = false;
		SingleUI.singleUIs.Remove(this);
		SingleUI.EnableNextSingleUI();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000132AE File Offset: 0x000114AE
	private void OnDisable()
	{
		if (!this.isSetForReenable && SingleUI.singleUIs.Contains(this))
		{
			SingleUI.singleUIs.Remove(this);
			SingleUI.EnableNextSingleUI();
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000132D8 File Offset: 0x000114D8
	private static void EnableNextSingleUI()
	{
		SingleUI.CheckList();
		while (SingleUI.singleUIs.Count > 0)
		{
			SingleUI singleUI = SingleUI.singleUIs[0];
			if (singleUI.isSetForReenable)
			{
				singleUI.EnableThisSingleUI(singleUI.openState);
				return;
			}
			Debug.LogError("SingleUI " + singleUI.name + " is not set for reenable but it was still part of the list");
			SingleUI.singleUIs.RemoveAt(0);
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00013344 File Offset: 0x00011544
	private void EnableThisSingleUI(SingleUI.State openState)
	{
		this.isSetForReenable = false;
		if (!base.gameObject)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(true);
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.fadeCoroutine != null)
		{
			base.StopCoroutine(this.fadeCoroutine);
			this.fadeCoroutine = null;
		}
		if (openState == SingleUI.State.Animation && this.animator && SingleUI.HasParameter("Open", this.animator))
		{
			this.animator.enabled = true;
			this.animator.SetTrigger("Open");
			if (this.fadeCoroutine == null)
			{
				this.fadeCoroutine = base.StartCoroutine(this.OpenWithAnimation());
			}
		}
		if (this.canvasGroup)
		{
			if (this.fadeCoroutine == null && openState == SingleUI.State.Fade)
			{
				this.fadeCoroutine = base.StartCoroutine(this.FadeIn());
				return;
			}
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001344C File Offset: 0x0001164C
	private void DisableThisUI(SingleUI.State closeState)
	{
		this.isSetForReenable = true;
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (closeState == SingleUI.State.Animation && this.animator && SingleUI.HasParameter("Close", this.animator))
		{
			this.animator.enabled = true;
			this.animator.SetTrigger("Close");
			if (this.fadeCoroutine == null)
			{
				this.fadeCoroutine = base.StartCoroutine(this.CloseWithAnimation());
			}
		}
		if (this.canvasGroup)
		{
			if (closeState == SingleUI.State.Fade && this.fadeCoroutine == null)
			{
				this.fadeCoroutine = base.StartCoroutine(this.FadeOut());
			}
			this.canvasGroup.interactable = false;
			return;
		}
		this.CloseThisUI();
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00013508 File Offset: 0x00011708
	private static bool HasParameter(string paramName, Animator animator)
	{
		AnimatorControllerParameter[] parameters = animator.parameters;
		for (int i = 0; i < parameters.Length; i++)
		{
			if (parameters[i].name == paramName)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001353D File Offset: 0x0001173D
	private IEnumerator CloseWithAnimation()
	{
		yield return new WaitForEndOfFrame();
		float deltaTime = Time.deltaTime;
		if (this.animator)
		{
			AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
			float num = currentAnimatorClipInfo[0].clip.length / this.animator.GetCurrentAnimatorStateInfo(0).speed;
			string name = currentAnimatorClipInfo[0].clip.name;
			yield return new WaitForSeconds(num - deltaTime);
		}
		this.fadeCoroutine = null;
		this.CloseThisUI();
		yield break;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001354C File Offset: 0x0001174C
	private IEnumerator OpenWithAnimation()
	{
		yield return new WaitForEndOfFrame();
		float deltaTime = Time.deltaTime;
		if (this.animator)
		{
			AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
			float num = currentAnimatorClipInfo[0].clip.length / this.animator.GetCurrentAnimatorStateInfo(0).speed;
			string name = currentAnimatorClipInfo[0].clip.name;
			yield return new WaitForSecondsRealtime(num - deltaTime);
		}
		this.fadeCoroutine = null;
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
		}
		yield break;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001355B File Offset: 0x0001175B
	private IEnumerator FadeOut()
	{
		if (this.animator)
		{
			this.animator.enabled = false;
		}
		while (this.canvasGroup && this.canvasGroup.alpha > 0f)
		{
			this.canvasGroup.alpha -= Time.unscaledDeltaTime * 5f;
			this.canvasGroup.alpha = Mathf.Clamp01(this.canvasGroup.alpha);
			yield return null;
		}
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 0f;
		}
		this.fadeCoroutine = null;
		this.CloseThisUI();
		yield break;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001356A File Offset: 0x0001176A
	private IEnumerator FadeIn()
	{
		if (this.animator)
		{
			this.animator.enabled = false;
		}
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 0f;
			this.canvasGroup.interactable = true;
		}
		while (this.canvasGroup && this.canvasGroup.alpha < 1f)
		{
			this.canvasGroup.alpha += Time.unscaledDeltaTime * 5f;
			this.canvasGroup.alpha = Mathf.Clamp01(this.canvasGroup.alpha);
			yield return null;
		}
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 1f;
		}
		this.fadeCoroutine = null;
		yield break;
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00013579 File Offset: 0x00011779
	private void CloseThisUI()
	{
		if (this.destroyOnClose && base.gameObject)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040002F1 RID: 753
	private static List<SingleUI> singleUIs = new List<SingleUI>();

	// Token: 0x040002F2 RID: 754
	[Header("Options")]
	[SerializeField]
	public bool moveToZero = true;

	// Token: 0x040002F3 RID: 755
	[SerializeField]
	private SingleUI.State openState = SingleUI.State.Animation;

	// Token: 0x040002F4 RID: 756
	[SerializeField]
	private SingleUI.State closeState = SingleUI.State.Animation;

	// Token: 0x040002F5 RID: 757
	[SerializeField]
	private bool hideCursor;

	// Token: 0x040002F6 RID: 758
	[Tooltip("If true, this UI will be destroyed when closed, you can also set this with the CloseAndDestroy method")]
	[SerializeField]
	private bool destroyOnClose;

	// Token: 0x040002F7 RID: 759
	[Header("Optional References")]
	[Tooltip("If this is set, the UI will be opened and closed with an animation - called from the animator \"Open\" and  \"Close\" triggers.")]
	[SerializeField]
	private Animator animator;

	// Token: 0x040002F8 RID: 760
	[Tooltip("If this is set, the UI will be faded out and made non-interactable when closed. Animator will override this.")]
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x040002F9 RID: 761
	private bool isSetForReenable;

	// Token: 0x040002FA RID: 762
	private Coroutine fadeCoroutine;

	// Token: 0x040002FB RID: 763
	private GameObject lastSelected;

	// Token: 0x02000110 RID: 272
	public enum State
	{
		// Token: 0x040004EB RID: 1259
		Instant,
		// Token: 0x040004EC RID: 1260
		Fade,
		// Token: 0x040004ED RID: 1261
		Animation
	}
}
