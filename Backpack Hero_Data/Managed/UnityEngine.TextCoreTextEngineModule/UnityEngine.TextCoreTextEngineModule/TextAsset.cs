using System;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001D RID: 29
	[ExcludeFromObjectFactory]
	[Serializable]
	public abstract class TextAsset : ScriptableObject
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007A98 File Offset: 0x00005C98
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007ABC File Offset: 0x00005CBC
		public int instanceID
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				if (flag)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00007AF0 File Offset: 0x00005CF0
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00007B26 File Offset: 0x00005D26
		public int hashCode
		{
			get
			{
				bool flag = this.m_HashCode == 0;
				if (flag)
				{
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
				}
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00007B2F File Offset: 0x00005D2F
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00007B37 File Offset: 0x00005D37
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007B40 File Offset: 0x00005D40
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00007B91 File Offset: 0x00005D91
		public int materialHashCode
		{
			get
			{
				bool flag = this.m_MaterialHashCode == 0;
				if (flag)
				{
					bool flag2 = this.m_Material == null;
					if (flag2)
					{
						return 0;
					}
					this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
				}
				return this.m_MaterialHashCode;
			}
			set
			{
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		internal string m_Version;

		// Token: 0x040000B4 RID: 180
		internal int m_InstanceID;

		// Token: 0x040000B5 RID: 181
		internal int m_HashCode;

		// Token: 0x040000B6 RID: 182
		[FormerlySerializedAs("material")]
		[SerializeField]
		internal Material m_Material;

		// Token: 0x040000B7 RID: 183
		internal int m_MaterialHashCode;
	}
}
