using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class SingleUI : MonoBehaviour
{
	// Token: 0x06001041 RID: 4161 RVA: 0x0009C6C2 File Offset: 0x0009A8C2
	public void ChangeState()
	{
		this.openState = SingleUI.State.Fade;
		this.closeState = SingleUI.State.Fade;
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0009C6D4 File Offset: 0x0009A8D4
	public static bool IsViewingHideCursorPopUp()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		return SingleUI.singleUIs.Count != 0 && SingleUI.singleUIs[0].hideCursor;
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x0009C744 File Offset: 0x0009A944
	public static bool IsViewingPopUpButItCanBeIgnored()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		return SingleUI.singleUIs.Count != 0 && SingleUI.singleUIs[0].canBeIgnoredBySomeStuff;
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0009C7B4 File Offset: 0x0009A9B4
	public static bool IsViewingPopUp()
	{
		while (SingleUI.singleUIs.Count > 0 && (!SingleUI.singleUIs[0] || !SingleUI.singleUIs[0].gameObject))
		{
			SingleUI.singleUIs.RemoveAt(0);
		}
		return SingleUI.singleUIs.Count != 0;
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0009C814 File Offset: 0x0009AA14
	public static bool FreeToInteractWith(Transform t)
	{
		SingleUI singleUI = SingleUI.GetSingleUI();
		TimeSpan timeSpan = DateTime.Now - SingleUI.lastTimeOpened;
		return (!singleUI && timeSpan.TotalSeconds > 0.10000000149011612) || t.IsChildOf(singleUI.transform);
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0009C864 File Offset: 0x0009AA64
	public static bool HasntBeenViewingPopUpForATime()
	{
		return !SingleUI.IsViewingPopUp() && (DateTime.Now - SingleUI.lastTimeOpened).TotalSeconds >= 0.10000000149011612;
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0009C89F File Offset: 0x0009AA9F
	public static SingleUI GetSingleUI()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return SingleUI.singleUIs[0];
		}
		return null;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0009C8B8 File Offset: 0x0009AAB8
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

	// Token: 0x06001049 RID: 4169 RVA: 0x0009C928 File Offset: 0x0009AB28
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

	// Token: 0x0600104A RID: 4170 RVA: 0x0009C99A File Offset: 0x0009AB9A
	public void SetOpeningState(SingleUI.State state)
	{
		this.openState = state;
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0009C9A3 File Offset: 0x0009ABA3
	public void SetClosingState(SingleUI.State state)
	{
		this.closeState = state;
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x0009C9AC File Offset: 0x0009ABAC
	public void CloseAndDestroyViaFade()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(SingleUI.State.Fade);
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0009C9BC File Offset: 0x0009ABBC
	public void CloseAndDestroyViaAnimation()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(SingleUI.State.Animation);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0009C9CC File Offset: 0x0009ABCC
	public void CloseAndDestroy()
	{
		this.destroyOnClose = true;
		this.DisableThisUI(this.closeState);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0009C9E1 File Offset: 0x0009ABE1
	public void Close()
	{
		this.isSetForReenable = false;
		SingleUI.singleUIs.Remove(this);
		this.DisableThisUI(this.closeState);
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0009CA02 File Offset: 0x0009AC02
	public void DestroyImmediate()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0009CA0F File Offset: 0x0009AC0F
	private void OnApplicationQuit()
	{
		SingleUI.singleUIs.Clear();
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0009CA1B File Offset: 0x0009AC1B
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

	// Token: 0x06001053 RID: 4179 RVA: 0x0009CA5C File Offset: 0x0009AC5C
	private void Start()
	{
		foreach (SingleUI singleUI in base.GetComponentsInChildren<SingleUI>())
		{
			if (!(singleUI == this))
			{
				Object.Destroy(singleUI);
				Debug.LogError("SingleUI component cannot be a child of another SingleUI component");
			}
		}
		if (!this.canvasGroup)
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
		}
		if (!this.animator)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0009CACD File Offset: 0x0009ACCD
	private void Update()
	{
		SingleUI.lastTimeOpened = DateTime.Now;
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0009CADC File Offset: 0x0009ACDC
	public void SetAsChild(SingleUI singleUI)
	{
		if (singleUI == this)
		{
			Debug.LogError("Cannot set " + base.name + " as a child of itself");
			return;
		}
		if (singleUI == null)
		{
			SingleUI.singleUIs.Remove(this);
			SingleUI.singleUIs.Insert(0, this);
			return;
		}
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0009CB30 File Offset: 0x0009AD30
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

	// Token: 0x06001057 RID: 4183 RVA: 0x0009CB8C File Offset: 0x0009AD8C
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

	// Token: 0x06001058 RID: 4184 RVA: 0x0009CC54 File Offset: 0x0009AE54
	private void RemoveElement()
	{
		this.DisableThisUI(this.closeState);
		this.isSetForReenable = false;
		SingleUI.singleUIs.Remove(this);
		SingleUI.EnableNextSingleUI();
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0009CC7C File Offset: 0x0009AE7C
	private void OnDisable()
	{
		if (!this.isSetForReenable && SingleUI.singleUIs.Contains(this))
		{
			SingleUI.singleUIs.Remove(this);
			SingleUI.EnableNextSingleUI();
		}
		if (this.hideCursor && this.hidTheCursorFromThis)
		{
			DigitalCursor.main.Show();
		}
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0009CCCC File Offset: 0x0009AECC
	private static void EnableNextSingleUI()
	{
		SingleUI.CheckList();
		while (SingleUI.singleUIs.Count > 0)
		{
			SingleUI singleUI = SingleUI.singleUIs[0];
			if (singleUI.isSetForReenable)
			{
				Debug.Log("Enabling " + singleUI.name);
				singleUI.EnableThisSingleUI(singleUI.openState);
				return;
			}
			Debug.LogError("SingleUI " + singleUI.name + " is not set for reenable but it was still part of the list");
			SingleUI.singleUIs.RemoveAt(0);
		}
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0009CD4A File Offset: 0x0009AF4A
	private static IEnumerator EnableNextSingleUIAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		SingleUI.EnableNextSingleUINow();
		yield break;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0009CD59 File Offset: 0x0009AF59
	private static void EnableNextSingleUINow()
	{
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x0009CD5C File Offset: 0x0009AF5C
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
		if (this.hideCursor)
		{
			if (!DigitalCursor.main.isHidden)
			{
				this.hidTheCursorFromThis = true;
			}
			DigitalCursor.main.Hide();
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

	// Token: 0x0600105E RID: 4190 RVA: 0x0009CE88 File Offset: 0x0009B088
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

	// Token: 0x0600105F RID: 4191 RVA: 0x0009CF44 File Offset: 0x0009B144
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

	// Token: 0x06001060 RID: 4192 RVA: 0x0009CF79 File Offset: 0x0009B179
	private IEnumerator CloseWithAnimation()
	{
		yield return new WaitForEndOfFrame();
		float deltaTime = Time.deltaTime;
		if (this.animator)
		{
			AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
			if (currentAnimatorClipInfo.Length != 0)
			{
				float num = currentAnimatorClipInfo[0].clip.length / this.animator.GetCurrentAnimatorStateInfo(0).speed;
				string name = currentAnimatorClipInfo[0].clip.name;
				yield return new WaitForSeconds(num - deltaTime);
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}
		}
		this.fadeCoroutine = null;
		this.CloseThisUI();
		yield break;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0009CF88 File Offset: 0x0009B188
	private IEnumerator OpenWithAnimation()
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
		if (this.canvasGroup)
		{
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.interactable = true;
		}
		yield break;
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0009CF97 File Offset: 0x0009B197
	private IEnumerator FadeOut()
	{
		if (this.animator)
		{
			this.animator.enabled = false;
		}
		while (this.canvasGroup && this.canvasGroup.alpha > 0f)
		{
			this.canvasGroup.alpha -= Time.deltaTime * 5f;
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

	// Token: 0x06001063 RID: 4195 RVA: 0x0009CFA6 File Offset: 0x0009B1A6
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
			this.canvasGroup.alpha += Time.deltaTime * 5f;
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

	// Token: 0x06001064 RID: 4196 RVA: 0x0009CFB5 File Offset: 0x0009B1B5
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

	// Token: 0x04000D52 RID: 3410
	private static List<SingleUI> singleUIs = new List<SingleUI>();

	// Token: 0x04000D53 RID: 3411
	public bool canBeIgnoredBySomeStuff;

	// Token: 0x04000D54 RID: 3412
	public static DateTime lastTimeOpened = DateTime.Now;

	// Token: 0x04000D55 RID: 3413
	[Header("Options")]
	[SerializeField]
	public bool forceSelect = true;

	// Token: 0x04000D56 RID: 3414
	[SerializeField]
	private SingleUI.State openState = SingleUI.State.Animation;

	// Token: 0x04000D57 RID: 3415
	[SerializeField]
	private SingleUI.State closeState = SingleUI.State.Animation;

	// Token: 0x04000D58 RID: 3416
	[SerializeField]
	private bool hideCursor;

	// Token: 0x04000D59 RID: 3417
	[Tooltip("If true, this UI will be destroyed when closed, you can also set this with the CloseAndDestroy method")]
	[SerializeField]
	private bool destroyOnClose;

	// Token: 0x04000D5A RID: 3418
	[Header("Optional References")]
	[Tooltip("If this is set, the UI will be opened and closed with an animation - called from the animator \"Open\" and  \"Close\" triggers.")]
	[SerializeField]
	private Animator animator;

	// Token: 0x04000D5B RID: 3419
	[Tooltip("If this is set, the UI will be faded out and made non-interactable when closed. Animator will override this.")]
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x04000D5C RID: 3420
	private bool isSetForReenable;

	// Token: 0x04000D5D RID: 3421
	private Coroutine fadeCoroutine;

	// Token: 0x04000D5E RID: 3422
	private bool hidTheCursorFromThis;

	// Token: 0x0200046E RID: 1134
	public enum State
	{
		// Token: 0x04001A2D RID: 6701
		Instant,
		// Token: 0x04001A2E RID: 6702
		Fade,
		// Token: 0x04001A2F RID: 6703
		Animation
	}
}
