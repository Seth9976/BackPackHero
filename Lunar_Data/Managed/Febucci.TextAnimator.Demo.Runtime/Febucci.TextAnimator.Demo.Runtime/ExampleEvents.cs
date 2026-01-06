using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Examples
{
	// Token: 0x02000003 RID: 3
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	internal class ExampleEvents : MonoBehaviour
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002240 File Offset: 0x00000440
		private void Start()
		{
			this.typewriter.onMessage.AddListener(new UnityAction<EventMarker>(this.OnMessage));
			this.dialogueIndex = 0;
			this.CurrentLineShown = false;
			this.typewriter.ShowText(this.dialoguesLines[this.dialogueIndex]);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000228F File Offset: 0x0000048F
		private void OnDestroy()
		{
			if (this.typewriter)
			{
				this.typewriter.onMessage.RemoveListener(new UnityAction<EventMarker>(this.OnMessage));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022BC File Offset: 0x000004BC
		private bool TryGetInt(string parameter, out int result)
		{
			float num;
			if (FormatUtils.TryGetFloat(parameter, 0f, out num))
			{
				result = (int)num;
				return true;
			}
			result = -1;
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E4 File Offset: 0x000004E4
		private void OnMessage(EventMarker eventData)
		{
			string name = eventData.name;
			if (!(name == "face"))
			{
				if (!(name == "crate"))
				{
					return;
				}
				if (eventData.parameters.Length == 0)
				{
					Debug.LogWarning(string.Format("You need to specify a crate index! Dialogue: {0}", this.dialogueIndex));
					return;
				}
				int num;
				if (this.TryGetInt(eventData.parameters[0], out num))
				{
					if (num >= 0 && num < this.crates.Length)
					{
						base.StartCoroutine(this.AnimateCrate(num));
						return;
					}
					Debug.Log(string.Format("Sprite index was out of range. Dialogue: {0}", this.dialogueIndex));
				}
			}
			else
			{
				if (eventData.parameters.Length == 0)
				{
					Debug.LogWarning(string.Format("You need to specify a sprite index! Dialogue: {0}", this.dialogueIndex));
					return;
				}
				int num2;
				if (this.TryGetInt(eventData.parameters[0], out num2))
				{
					if (num2 >= 0 && num2 < this.faces.Length)
					{
						this.faceRenderer.sprite = this.faces[num2];
						return;
					}
					Debug.Log(string.Format("Sprite index was out of range. Dialogue: {0}", this.dialogueIndex));
					return;
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002400 File Offset: 0x00000600
		private bool CurrentLineShown
		{
			get
			{
				return this.currentLineShown;
			}
			set
			{
				this.currentLineShown = value;
				this.continueText.SetActive(value);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002418 File Offset: 0x00000618
		private void Awake()
		{
			this.cratesInitialScale = new Vector3[this.crates.Length];
			for (int i = 0; i < this.crates.Length; i++)
			{
				this.cratesInitialScale[i] = this.crates[i].localScale;
			}
			this.dialogueLength = this.dialoguesLines.Length;
			this.typewriter.onTextShowed.AddListener(delegate
			{
				this.CurrentLineShown = true;
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002490 File Offset: 0x00000690
		private void ContinueSequence()
		{
			this.CurrentLineShown = false;
			this.dialogueIndex++;
			if (this.dialogueIndex < this.dialogueLength)
			{
				this.typewriter.ShowText(this.dialoguesLines[this.dialogueIndex]);
				return;
			}
			this.typewriter.StartDisappearingText();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024E4 File Offset: 0x000006E4
		private void Update()
		{
			if (Input.anyKeyDown && this.CurrentLineShown)
			{
				this.ContinueSequence();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024FB File Offset: 0x000006FB
		private IEnumerator AnimateCrate(int crateIndex)
		{
			Transform crate = this.crates[crateIndex];
			Vector3 initialScale = this.cratesInitialScale[crateIndex];
			Vector3 targetScale = new Vector3(initialScale.x * 1.2f, initialScale.y * 0.6f, initialScale.z);
			float t = 0f;
			while (t <= 0.4f)
			{
				t += Time.unscaledDeltaTime;
				float num = t / 0.4f;
				if (num < 0.5f)
				{
					num /= 0.5f;
				}
				else
				{
					num = 1f - (num - 0.5f) / 0.5f;
				}
				crate.localScale = Vector3.LerpUnclamped(initialScale, targetScale, num);
				yield return null;
			}
			crate.localScale = initialScale;
			yield break;
		}

		// Token: 0x04000003 RID: 3
		[SerializeField]
		private TypewriterCore typewriter;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[TextArea(1, 5)]
		private string[] dialoguesLines;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private Sprite[] faces;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private SpriteRenderer faceRenderer;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private GameObject continueText;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private Transform[] crates;

		// Token: 0x04000009 RID: 9
		private Vector3[] cratesInitialScale;

		// Token: 0x0400000A RID: 10
		private int dialogueIndex;

		// Token: 0x0400000B RID: 11
		private int dialogueLength;

		// Token: 0x0400000C RID: 12
		private bool currentLineShown;
	}
}
