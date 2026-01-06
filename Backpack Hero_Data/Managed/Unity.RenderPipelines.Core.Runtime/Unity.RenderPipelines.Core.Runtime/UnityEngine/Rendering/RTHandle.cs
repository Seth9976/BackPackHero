using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000090 RID: 144
	public class RTHandle
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00015419 File Offset: 0x00013619
		public void SetCustomHandleProperties(in RTHandleProperties properties)
		{
			this.m_UseCustomHandleScales = true;
			this.m_CustomHandleProperties = properties;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001542E File Offset: 0x0001362E
		public void ClearCustomHandleProperties()
		{
			this.m_UseCustomHandleScales = false;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00015437 File Offset: 0x00013637
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001543F File Offset: 0x0001363F
		public Vector2 scaleFactor { get; internal set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015448 File Offset: 0x00013648
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00015450 File Offset: 0x00013650
		public bool useScaling { get; internal set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00015459 File Offset: 0x00013659
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00015461 File Offset: 0x00013661
		public Vector2Int referenceSize { get; internal set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001546A File Offset: 0x0001366A
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				if (!this.m_UseCustomHandleScales)
				{
					return this.m_Owner.rtHandleProperties;
				}
				return this.m_CustomHandleProperties;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00015486 File Offset: 0x00013686
		public RenderTexture rt
		{
			get
			{
				return this.m_RT;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001548E File Offset: 0x0001368E
		public RenderTargetIdentifier nameID
		{
			get
			{
				return this.m_NameID;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00015496 File Offset: 0x00013696
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001549E File Offset: 0x0001369E
		public bool isMSAAEnabled
		{
			get
			{
				return this.m_EnableMSAA;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000154A6 File Offset: 0x000136A6
		internal RTHandle(RTHandleSystem owner)
		{
			this.m_Owner = owner;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000154B8 File Offset: 0x000136B8
		public static implicit operator RenderTargetIdentifier(RTHandle handle)
		{
			if (handle == null)
			{
				return default(RenderTargetIdentifier);
			}
			return handle.nameID;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000154D8 File Offset: 0x000136D8
		public static implicit operator Texture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			if (!(handle.rt != null))
			{
				return handle.m_ExternalTexture;
			}
			return handle.rt;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000154FA File Offset: 0x000136FA
		public static implicit operator RenderTexture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			return handle.rt;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00015507 File Offset: 0x00013707
		internal void SetRenderTexture(RenderTexture rt)
		{
			this.m_RT = rt;
			this.m_ExternalTexture = null;
			this.m_NameID = new RenderTargetIdentifier(rt);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00015523 File Offset: 0x00013723
		internal void SetTexture(Texture tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = tex;
			this.m_NameID = new RenderTargetIdentifier(tex);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001553F File Offset: 0x0001373F
		internal void SetTexture(RenderTargetIdentifier tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = null;
			this.m_NameID = tex;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015558 File Offset: 0x00013758
		public int GetInstanceID()
		{
			if (this.m_RT != null)
			{
				return this.m_RT.GetInstanceID();
			}
			if (this.m_ExternalTexture != null)
			{
				return this.m_ExternalTexture.GetInstanceID();
			}
			return this.m_NameID.GetHashCode();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000155AA File Offset: 0x000137AA
		public void Release()
		{
			this.m_Owner.Remove(this);
			CoreUtils.Destroy(this.m_RT);
			this.m_NameID = BuiltinRenderTextureType.None;
			this.m_RT = null;
			this.m_ExternalTexture = null;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000155E0 File Offset: 0x000137E0
		public Vector2Int GetScaledSize(Vector2Int refSize)
		{
			if (!this.useScaling)
			{
				return refSize;
			}
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(refSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)refSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)refSize.y));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015644 File Offset: 0x00013844
		public Vector2Int GetScaledSize()
		{
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(this.referenceSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)this.referenceSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)this.referenceSize.y));
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000156B0 File Offset: 0x000138B0
		public void SwitchToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop, bool copyContents = false)
		{
			residencyFraction = Mathf.Clamp01(residencyFraction);
			cmd.SwitchIntoFastMemory(this.m_RT, flags, residencyFraction, copyContents);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000156CF File Offset: 0x000138CF
		public void CopyToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop)
		{
			this.SwitchToFastMemory(cmd, residencyFraction, flags, true);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000156DB File Offset: 0x000138DB
		public void SwitchOutFastMemory(CommandBuffer cmd, bool copyContents = true)
		{
			cmd.SwitchOutOfFastMemory(this.m_RT, copyContents);
		}

		// Token: 0x040002EE RID: 750
		internal RTHandleSystem m_Owner;

		// Token: 0x040002EF RID: 751
		internal RenderTexture m_RT;

		// Token: 0x040002F0 RID: 752
		internal Texture m_ExternalTexture;

		// Token: 0x040002F1 RID: 753
		internal RenderTargetIdentifier m_NameID;

		// Token: 0x040002F2 RID: 754
		internal bool m_EnableMSAA;

		// Token: 0x040002F3 RID: 755
		internal bool m_EnableRandomWrite;

		// Token: 0x040002F4 RID: 756
		internal bool m_EnableHWDynamicScale;

		// Token: 0x040002F5 RID: 757
		internal string m_Name;

		// Token: 0x040002F6 RID: 758
		internal bool m_UseCustomHandleScales;

		// Token: 0x040002F7 RID: 759
		internal RTHandleProperties m_CustomHandleProperties;

		// Token: 0x040002F9 RID: 761
		internal ScaleFunc scaleFunc;
	}
}
