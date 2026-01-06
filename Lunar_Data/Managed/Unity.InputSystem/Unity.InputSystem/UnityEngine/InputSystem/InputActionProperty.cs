using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public struct InputActionProperty : IEquatable<InputActionProperty>, IEquatable<InputAction>, IEquatable<InputActionReference>
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006D63 File Offset: 0x00004F63
		public InputAction action
		{
			get
			{
				if (!this.m_UseReference)
				{
					return this.m_Action;
				}
				if (!(this.m_Reference != null))
				{
					return null;
				}
				return this.m_Reference.action;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006D8F File Offset: 0x00004F8F
		public InputActionReference reference
		{
			get
			{
				if (!this.m_UseReference)
				{
					return null;
				}
				return this.m_Reference;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public InputActionProperty(InputAction action)
		{
			this.m_UseReference = false;
			this.m_Action = action;
			this.m_Reference = null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public InputActionProperty(InputActionReference reference)
		{
			this.m_UseReference = true;
			this.m_Action = null;
			this.m_Reference = reference;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006DCF File Offset: 0x00004FCF
		public bool Equals(InputActionProperty other)
		{
			return this.m_Reference == other.m_Reference && this.m_UseReference == other.m_UseReference && this.m_Action == other.m_Action;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006E02 File Offset: 0x00005002
		public bool Equals(InputAction other)
		{
			return this.action == other;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006E0D File Offset: 0x0000500D
		public bool Equals(InputActionReference other)
		{
			return this.m_Reference == other;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006E1B File Offset: 0x0000501B
		public override bool Equals(object obj)
		{
			if (this.m_UseReference)
			{
				return this.Equals(obj as InputActionReference);
			}
			return this.Equals(obj as InputAction);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006E3E File Offset: 0x0000503E
		public override int GetHashCode()
		{
			if (this.m_UseReference)
			{
				if (!(this.m_Reference != null))
				{
					return 0;
				}
				return this.m_Reference.GetHashCode();
			}
			else
			{
				if (this.m_Action == null)
				{
					return 0;
				}
				return this.m_Action.GetHashCode();
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006E79 File Offset: 0x00005079
		public static bool operator ==(InputActionProperty left, InputActionProperty right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006E83 File Offset: 0x00005083
		public static bool operator !=(InputActionProperty left, InputActionProperty right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private bool m_UseReference;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private InputAction m_Action;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		private InputActionReference m_Reference;
	}
}
