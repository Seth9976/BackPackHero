using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006C RID: 108
	public struct Bone
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00035F62 File Offset: 0x00034162
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00035F6A File Offset: 0x0003416A
		public uint parentBoneIndex
		{
			get
			{
				return this.m_ParentBoneIndex;
			}
			set
			{
				this.m_ParentBoneIndex = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00035F73 File Offset: 0x00034173
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00035F7B File Offset: 0x0003417B
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00035F84 File Offset: 0x00034184
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00035F8C File Offset: 0x0003418C
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x04000344 RID: 836
		public uint m_ParentBoneIndex;

		// Token: 0x04000345 RID: 837
		public Vector3 m_Position;

		// Token: 0x04000346 RID: 838
		public Quaternion m_Rotation;
	}
}
