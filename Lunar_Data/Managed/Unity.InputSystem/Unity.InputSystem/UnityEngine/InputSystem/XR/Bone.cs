using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006C RID: 108
	public struct Bone
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00035F26 File Offset: 0x00034126
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00035F2E File Offset: 0x0003412E
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00035F37 File Offset: 0x00034137
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00035F3F File Offset: 0x0003413F
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00035F48 File Offset: 0x00034148
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00035F50 File Offset: 0x00034150
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
