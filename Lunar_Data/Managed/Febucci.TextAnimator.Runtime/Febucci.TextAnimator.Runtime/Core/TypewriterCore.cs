using System;
using System.Collections;
using Febucci.UI.Actions;
using Febucci.UI.Core.Parsing;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Core
{
	// Token: 0x02000039 RID: 57
	[DisallowMultipleComponent]
	[RequireComponent(typeof(TAnimCore))]
	public abstract class TypewriterCore : MonoBehaviour
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000064A0 File Offset: 0x000046A0
		public TAnimCore TextAnimator
		{
			get
			{
				if (this._textAnimator != null)
				{
					return this._textAnimator;
				}
				if (!base.TryGetComponent<TAnimCore>(out this._textAnimator))
				{
					Debug.LogError("TextAnimator: Text Animator component is null on GameObject " + base.gameObject.name + ". Please add a component that inherits from TAnimCore");
				}
				return this._textAnimator;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000064F8 File Offset: 0x000046F8
		public void ShowText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.TextAnimator.SetText(string.Empty, true);
				return;
			}
			this.TextAnimator.SetText(text, this.useTypeWriter);
			this.TextAnimator.firstVisibleCharacter = 0;
			if (this.useTypeWriter)
			{
				if (this.startTypewriterMode.HasFlag(TypewriterCore.StartTypewriterMode.OnShowText))
				{
					this.StartShowingText(true);
				}
				return;
			}
			UnityEvent unityEvent = this.onTextShowed;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006578 File Offset: 0x00004778
		public void SkipTypewriter()
		{
			if (this.isShowingText)
			{
				base.StopAllCoroutines();
				this.isShowingText = false;
				this.TextAnimator.SetVisibilityEntireText(true, !this.hideAppearancesOnSkip);
				if (this.triggerEventsOnSkip)
				{
					this.TriggerEventsUntil(int.MaxValue);
				}
				UnityEvent unityEvent = this.onTextShowed;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
			}
			if (this.isHidingText)
			{
				base.StopAllCoroutines();
				this.isHidingText = false;
				UnityEvent unityEvent2 = this.onTextDisappeared;
				if (unityEvent2 != null)
				{
					unityEvent2.Invoke();
				}
				this.TextAnimator.SetVisibilityEntireText(false, !this.hideDisappearancesOnSkip);
				UnityEvent unityEvent3 = this.onTextDisappeared;
				if (unityEvent3 == null)
				{
					return;
				}
				unityEvent3.Invoke();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000661E File Offset: 0x0000481E
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006626 File Offset: 0x00004826
		public bool isShowingText { get; private set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00006630 File Offset: 0x00004830
		public void StartShowingText(bool restart = false)
		{
			if (this.TextAnimator.CharactersCount == 0)
			{
				return;
			}
			if (!this.useTypeWriter)
			{
				Debug.LogWarning("TextAnimator: couldn't start coroutine because 'useTypewriter' is disabled");
				return;
			}
			if (this.isShowingText)
			{
				this.StopShowingText();
			}
			if (restart)
			{
				this.TextAnimator.SetVisibilityEntireText(false, false);
				this.latestActionTriggered = 0;
				this.latestEventTriggered = 0;
			}
			if (this.resetTypingSpeedAtStartup)
			{
				this.internalSpeed = 1f;
			}
			this.isShowingText = true;
			this.showRoutine = base.StartCoroutine(this.ShowTextRoutine());
		}

		// Token: 0x0600013C RID: 316
		protected abstract float GetWaitAppearanceTimeOf(int charIndex);

		// Token: 0x0600013D RID: 317 RVA: 0x000066B6 File Offset: 0x000048B6
		private float GetDeltaTime(TypingInfo typingInfo)
		{
			return this.TextAnimator.time.deltaTime * this.internalSpeed * typingInfo.speed;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000066D6 File Offset: 0x000048D6
		private IEnumerator ShowTextRoutine()
		{
			this.isShowingText = true;
			TypingInfo typingInfo = new TypingInfo();
			UnityEvent unityEvent = this.onTypewriterStart;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			TextAnimatorSettings instance = TextAnimatorSettings.Instance;
			bool actionsEnabled = instance && instance.actions.enabled;
			int num;
			for (int i = 0; i < this.TextAnimator.CharactersCount; i = num + 1)
			{
				if (actionsEnabled)
				{
					int maxIndex = i + 1;
					int a = this.latestActionTriggered;
					while (a < this.TextAnimator.Actions.Length && this.TextAnimator.Actions[a].index < maxIndex)
					{
						ActionMarker actionMarker = this.TextAnimator.Actions[a];
						this.TriggerEventsBeforeAction(maxIndex, actionMarker);
						ActionScriptableBase actionScriptableBase = this.TextAnimator.DatabaseActions[actionMarker.name];
						yield return this.nestedActionRoutine = base.StartCoroutine((actionScriptableBase != null) ? actionScriptableBase.DoAction(actionMarker, this, typingInfo) : null);
						this.latestActionTriggered = a + 1;
						num = a;
						a = num + 1;
					}
				}
				this.TriggerEventsUntil(i + 1);
				if (!this.TextAnimator.Characters[i].isVisible)
				{
					this.TextAnimator.SetVisibilityChar(i, true);
					CharacterEvent characterEvent = this.onCharacterVisible;
					if (characterEvent != null)
					{
						characterEvent.Invoke(this.TextAnimator.latestCharacterShown.info.character);
					}
					float timeToWait = this.GetWaitAppearanceTimeOf(i);
					float deltaTime = this.GetDeltaTime(typingInfo);
					if (timeToWait < 0f)
					{
						timeToWait = 0f;
					}
					if (timeToWait < deltaTime)
					{
						typingInfo.timePassed += timeToWait;
						if (typingInfo.timePassed >= deltaTime)
						{
							yield return null;
							typingInfo.timePassed %= deltaTime;
						}
					}
					else
					{
						while (typingInfo.timePassed < timeToWait)
						{
							typingInfo.timePassed += deltaTime;
							yield return null;
							deltaTime = this.GetDeltaTime(typingInfo);
						}
						typingInfo.timePassed %= timeToWait;
					}
				}
				num = i;
			}
			if (actionsEnabled)
			{
				int i = this.latestActionTriggered;
				while (i < this.TextAnimator.Actions.Length && this.TextAnimator.Actions[i].index < 2147483647)
				{
					ActionMarker actionMarker2 = this.TextAnimator.Actions[i];
					this.TriggerEventsBeforeAction(int.MaxValue, actionMarker2);
					ActionScriptableBase actionScriptableBase2 = this.TextAnimator.DatabaseActions[actionMarker2.name];
					yield return this.nestedActionRoutine = base.StartCoroutine((actionScriptableBase2 != null) ? actionScriptableBase2.DoAction(actionMarker2, this, typingInfo) : null);
					this.latestActionTriggered = i + 1;
					num = i;
					i = num + 1;
				}
			}
			this.TriggerEventsUntil(int.MaxValue);
			UnityEvent unityEvent2 = this.onTextShowed;
			if (unityEvent2 != null)
			{
				unityEvent2.Invoke();
			}
			this.isShowingText = false;
			yield break;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000066E5 File Offset: 0x000048E5
		public void StopShowingText()
		{
			if (!this.isShowingText)
			{
				return;
			}
			this.isShowingText = false;
			if (this.showRoutine != null)
			{
				base.StopCoroutine(this.showRoutine);
			}
			if (this.nestedActionRoutine != null)
			{
				base.StopCoroutine(this.nestedActionRoutine);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000671F File Offset: 0x0000491F
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006727 File Offset: 0x00004927
		public bool isHidingText { get; private set; }

		// Token: 0x06000142 RID: 322 RVA: 0x00006730 File Offset: 0x00004930
		[ContextMenu("Start Disappearing Text")]
		public void StartDisappearingText()
		{
			if (this.disappearanceOrientation == TypewriterCore.DisappearanceOrientation.Inverted && this.isShowingText)
			{
				Debug.LogWarning("TextAnimatorPlayer: Can't start disappearance routine in the opposite direction of the typewriter, because you're still showing the text! (the typewriter might get stuck trying to show and override letters that keep disappearing)");
				return;
			}
			if (this.isHidingText)
			{
				return;
			}
			this.hideRoutine = base.StartCoroutine(this.HideTextRoutine());
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006769 File Offset: 0x00004969
		[ContextMenu("Stop Disappearing Text")]
		public void StopDisappearingText()
		{
			if (!this.isHidingText)
			{
				return;
			}
			this.isHidingText = false;
			if (this.hideRoutine != null)
			{
				base.StopCoroutine(this.hideRoutine);
			}
			if (this.nestedHideRoutine != null)
			{
				base.StopCoroutine(this.nestedHideRoutine);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000067A3 File Offset: 0x000049A3
		protected virtual float GetWaitDisappearanceTimeOf(int charIndex)
		{
			return this.GetWaitAppearanceTimeOf(charIndex);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000067AC File Offset: 0x000049AC
		private static int[] ShuffleArray(int[] array)
		{
			Random random = new Random();
			int i = array.Length;
			while (i > 1)
			{
				int num = random.Next(i--);
				ref int ptr = ref array[i];
				int num2 = num;
				int num3 = array[num];
				int num4 = array[i];
				ptr = num3;
				array[num2] = num4;
			}
			return array;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000067F6 File Offset: 0x000049F6
		private IEnumerator HideTextRoutine()
		{
			this.isHidingText = true;
			TypingInfo typingInfo = new TypingInfo();
			int[] indexes = new int[this.TextAnimator.CharactersCount];
			switch (this.disappearanceOrientation)
			{
			default:
			{
				for (int j = 0; j < this.TextAnimator.CharactersCount; j++)
				{
					indexes[j] = j;
				}
				break;
			}
			case TypewriterCore.DisappearanceOrientation.Inverted:
			{
				for (int k = 0; k < this.TextAnimator.CharactersCount; k++)
				{
					indexes[k] = this.TextAnimator.CharactersCount - k - 1;
				}
				break;
			}
			case TypewriterCore.DisappearanceOrientation.Random:
			{
				for (int l = 0; l < this.TextAnimator.CharactersCount; l++)
				{
					indexes[l] = l;
				}
				indexes = TypewriterCore.ShuffleArray(indexes);
				break;
			}
			}
			int num2;
			for (int i = 0; i < this.TextAnimator.CharactersCount; i = num2 + 1)
			{
				int num = indexes[i];
				if (this.TextAnimator.Characters[num].isVisible)
				{
					this.TextAnimator.SetVisibilityChar(num, false);
					float timeToWait = this.GetWaitDisappearanceTimeOf(num);
					float deltaTime = this.GetDeltaTime(typingInfo);
					if (timeToWait < 0f)
					{
						timeToWait = 0f;
					}
					if (timeToWait < deltaTime)
					{
						typingInfo.timePassed += timeToWait;
						if (typingInfo.timePassed >= deltaTime)
						{
							yield return null;
							typingInfo.timePassed %= deltaTime;
						}
					}
					else
					{
						while (typingInfo.timePassed < timeToWait)
						{
							typingInfo.timePassed += deltaTime;
							yield return null;
							deltaTime = this.GetDeltaTime(typingInfo);
						}
						typingInfo.timePassed %= timeToWait;
					}
				}
				num2 = i;
			}
			UnityEvent unityEvent = this.onTextDisappeared;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.isHidingText = false;
			yield break;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006805 File Offset: 0x00004A05
		public void SetTypewriterSpeed(float value)
		{
			this.internalSpeed = Mathf.Clamp(value, 0.001f, value);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000681C File Offset: 0x00004A1C
		private void TriggerEventsBeforeAction(int maxIndex, ActionMarker action)
		{
			int num = this.latestEventTriggered;
			while (num < this.TextAnimator.Events.Length && this.TextAnimator.Events[num].index < maxIndex && this.TextAnimator.Events[num].internalOrder < action.internalOrder)
			{
				MessageEvent messageEvent = this.onMessage;
				if (messageEvent != null)
				{
					messageEvent.Invoke(this.TextAnimator.Events[num]);
				}
				this.latestEventTriggered = num + 1;
				num++;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000689C File Offset: 0x00004A9C
		private void TriggerEventsUntil(int maxIndex)
		{
			int num = this.latestEventTriggered;
			while (num < this.TextAnimator.Events.Length && this.TextAnimator.Events[num].index < maxIndex)
			{
				MessageEvent messageEvent = this.onMessage;
				if (messageEvent != null)
				{
					messageEvent.Invoke(this.TextAnimator.Events[num]);
				}
				this.latestEventTriggered = num + 1;
				num++;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006902 File Offset: 0x00004B02
		public void TriggerRemainingEvents()
		{
			this.TriggerEventsUntil(int.MaxValue);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000690F File Offset: 0x00004B0F
		public void TriggerVisibleEvents()
		{
			this.TriggerEventsUntil(this.TextAnimator.latestCharacterShown.index);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006927 File Offset: 0x00004B27
		protected virtual void OnEnable()
		{
			if (!this.useTypeWriter)
			{
				return;
			}
			if (!this.startTypewriterMode.HasFlag(TypewriterCore.StartTypewriterMode.OnEnable))
			{
				return;
			}
			this.StartShowingText(false);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006952 File Offset: 0x00004B52
		protected virtual void OnDisable()
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006954 File Offset: 0x00004B54
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000695C File Offset: 0x00004B5C
		[Obsolete("Please set the speed through 'SetTypewriterSpeed' method instead")]
		protected float typewriterPlayerSpeed
		{
			get
			{
				return this.internalSpeed;
			}
			set
			{
				this.SetTypewriterSpeed(value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006965 File Offset: 0x00004B65
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000696C File Offset: 0x00004B6C
		[Obsolete("Please skip the typewriter via the 'SkipTypewriter' method instead")]
		protected bool wantsToSkip
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value)
				{
					this.SkipTypewriter();
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006977 File Offset: 0x00004B77
		[Obsolete("Please use 'isShowingText' instead")]
		protected bool isBaseInsideRoutine
		{
			get
			{
				return this.isShowingText;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000697F File Offset: 0x00004B7F
		[Obsolete("Please use 'TextAnimator' instead")]
		public TAnimCore textAnimator
		{
			get
			{
				return this.TextAnimator;
			}
		}

		// Token: 0x040000CE RID: 206
		private TAnimCore _textAnimator;

		// Token: 0x040000CF RID: 207
		[Tooltip("True if you want to shows the text dynamically")]
		[SerializeField]
		public bool useTypeWriter = true;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		[Tooltip("Controls from which method(s) the typewriter will automatically start/resume. Default is 'Automatic'")]
		public TypewriterCore.StartTypewriterMode startTypewriterMode = TypewriterCore.StartTypewriterMode.AutomaticallyFromAllEvents;

		// Token: 0x040000D1 RID: 209
		public bool hideAppearancesOnSkip;

		// Token: 0x040000D2 RID: 210
		public bool hideDisappearancesOnSkip;

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		[Tooltip("True = plays all remaining events once the typewriter has been skipped during a show routine")]
		private bool triggerEventsOnSkip;

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		[Tooltip("True = resets the typewriter speed every time a new text is set/shown")]
		public bool resetTypingSpeedAtStartup = true;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		public TypewriterCore.DisappearanceOrientation disappearanceOrientation;

		// Token: 0x040000D6 RID: 214
		public UnityEvent onTextShowed = new UnityEvent();

		// Token: 0x040000D7 RID: 215
		public UnityEvent onTypewriterStart = new UnityEvent();

		// Token: 0x040000D8 RID: 216
		public UnityEvent onTextDisappeared = new UnityEvent();

		// Token: 0x040000D9 RID: 217
		public CharacterEvent onCharacterVisible = new CharacterEvent();

		// Token: 0x040000DA RID: 218
		public MessageEvent onMessage = new MessageEvent();

		// Token: 0x040000DC RID: 220
		private Coroutine showRoutine;

		// Token: 0x040000DD RID: 221
		private Coroutine nestedActionRoutine;

		// Token: 0x040000DF RID: 223
		private Coroutine hideRoutine;

		// Token: 0x040000E0 RID: 224
		private Coroutine nestedHideRoutine;

		// Token: 0x040000E1 RID: 225
		private float internalSpeed = 1f;

		// Token: 0x040000E2 RID: 226
		private int latestActionTriggered;

		// Token: 0x040000E3 RID: 227
		private int latestEventTriggered;

		// Token: 0x0200005D RID: 93
		[Flags]
		public enum StartTypewriterMode
		{
			// Token: 0x04000147 RID: 327
			FromScriptOnly = 0,
			// Token: 0x04000148 RID: 328
			OnEnable = 1,
			// Token: 0x04000149 RID: 329
			OnShowText = 2,
			// Token: 0x0400014A RID: 330
			AutomaticallyFromAllEvents = 3
		}

		// Token: 0x0200005E RID: 94
		public enum DisappearanceOrientation
		{
			// Token: 0x0400014C RID: 332
			SameAsTypewriter,
			// Token: 0x0400014D RID: 333
			Inverted,
			// Token: 0x0400014E RID: 334
			Random
		}
	}
}
