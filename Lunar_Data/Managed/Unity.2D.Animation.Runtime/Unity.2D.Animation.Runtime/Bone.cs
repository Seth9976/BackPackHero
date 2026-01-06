using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000002 RID: 2
	[AddComponentMenu("")]
	internal class Bone : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string guid
		{
			get
			{
				return this.m_Guid;
			}
			set
			{
				this.m_Guid = value;
			}
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		[HideInInspector]
		private string m_Guid;
	}
}
