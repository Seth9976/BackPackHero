using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x06000283 RID: 643
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A5FC File Offset: 0x000087FC
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000A614 File Offset: 0x00008814
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A620 File Offset: 0x00008820
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000A638 File Offset: 0x00008838
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A644 File Offset: 0x00008844
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000A65C File Offset: 0x0000885C
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A668 File Offset: 0x00008868
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000A69D File Offset: 0x0000889D
		public float cursorFlashSpeed
		{
			get
			{
				bool flag = this.m_CursorFlashSpeed >= 0f;
				float num;
				if (flag)
				{
					num = this.m_CursorFlashSpeed;
				}
				else
				{
					num = GUISettings.Internal_GetCursorFlashSpeed();
				}
				return num;
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A6A8 File Offset: 0x000088A8
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
