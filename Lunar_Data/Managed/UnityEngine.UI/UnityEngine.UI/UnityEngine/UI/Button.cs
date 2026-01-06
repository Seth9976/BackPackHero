using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000003 RID: 3
	[AddComponentMenu("UI/Button", 30)]
	public class Button : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020E4 File Offset: 0x000002E4
		protected Button()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020FF File Offset: 0x000002FF
		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return this.m_OnClick;
			}
			set
			{
				this.m_OnClick = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002108 File Offset: 0x00000308
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			UISystemProfilerApi.AddMarker("Button.onClick", this);
			this.m_OnClick.Invoke();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002131 File Offset: 0x00000331
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002142 File Offset: 0x00000342
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002170 File Offset: 0x00000370
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x0400000B RID: 11
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		// Token: 0x02000077 RID: 119
		[Serializable]
		public class ButtonClickedEvent : UnityEvent
		{
		}
	}
}
