using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000270 RID: 624
	[NativeHeader("Runtime/2D/Common/SpriteDataMarshalling.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "ScriptingSpriteBone")]
	[Serializable]
	public struct SpriteBone
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0002B61C File Offset: 0x0002981C
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0002B634 File Offset: 0x00029834
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0002B640 File Offset: 0x00029840
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0002B658 File Offset: 0x00029858
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0002B664 File Offset: 0x00029864
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0002B67C File Offset: 0x0002987C
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

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0002B688 File Offset: 0x00029888
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0002B6A0 File Offset: 0x000298A0
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0002B6AC File Offset: 0x000298AC
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0002B6C4 File Offset: 0x000298C4
		public float length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0002B6D0 File Offset: 0x000298D0
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0002B6E8 File Offset: 0x000298E8
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0002B6F4 File Offset: 0x000298F4
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x0002B70C File Offset: 0x0002990C
		public Color32 color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x040008E9 RID: 2281
		[SerializeField]
		[NativeName("name")]
		private string m_Name;

		// Token: 0x040008EA RID: 2282
		[NativeName("guid")]
		[SerializeField]
		private string m_Guid;

		// Token: 0x040008EB RID: 2283
		[SerializeField]
		[NativeName("position")]
		private Vector3 m_Position;

		// Token: 0x040008EC RID: 2284
		[SerializeField]
		[NativeName("rotation")]
		private Quaternion m_Rotation;

		// Token: 0x040008ED RID: 2285
		[SerializeField]
		[NativeName("length")]
		private float m_Length;

		// Token: 0x040008EE RID: 2286
		[NativeName("parentId")]
		[SerializeField]
		private int m_ParentId;

		// Token: 0x040008EF RID: 2287
		[NativeName("color")]
		[SerializeField]
		private Color32 m_Color;
	}
}
