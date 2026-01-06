using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B6 RID: 182
	[ExecuteAlways]
	[AddComponentMenu("Miscellaneous/Volume")]
	public class Volume : MonoBehaviour, IVolume
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		[Tooltip("When enabled, the Volume is applied to the entire Scene.")]
		public bool isGlobal
		{
			get
			{
				return this.m_IsGlobal;
			}
			set
			{
				this.m_IsGlobal = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		public VolumeProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<VolumeProfile>();
					if (this.sharedProfile != null)
					{
						this.m_InternalProfile.name = this.sharedProfile.name;
						foreach (VolumeComponent volumeComponent in this.sharedProfile.components)
						{
							VolumeComponent volumeComponent2 = Object.Instantiate<VolumeComponent>(volumeComponent);
							this.m_InternalProfile.components.Add(volumeComponent2);
						}
					}
				}
				return this.m_InternalProfile;
			}
			set
			{
				this.m_InternalProfile = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001CBB1 File Offset: 0x0001ADB1
		public List<Collider> colliders
		{
			get
			{
				return this.m_Colliders;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001CBB9 File Offset: 0x0001ADB9
		internal VolumeProfile profileRef
		{
			get
			{
				if (!(this.m_InternalProfile == null))
				{
					return this.m_InternalProfile;
				}
				return this.sharedProfile;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001CBD6 File Offset: 0x0001ADD6
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		private void OnEnable()
		{
			this.m_PreviousLayer = base.gameObject.layer;
			VolumeManager.instance.Register(this, this.m_PreviousLayer);
			base.GetComponents<Collider>(this.m_Colliders);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001CC14 File Offset: 0x0001AE14
		private void OnDisable()
		{
			VolumeManager.instance.Unregister(this, base.gameObject.layer);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		private void Update()
		{
			this.UpdateLayer();
			if (this.priority != this.m_PreviousPriority)
			{
				VolumeManager.instance.SetLayerDirty(base.gameObject.layer);
				this.m_PreviousPriority = this.priority;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001CC64 File Offset: 0x0001AE64
		internal void UpdateLayer()
		{
			int layer = base.gameObject.layer;
			if (layer != this.m_PreviousLayer)
			{
				VolumeManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
				this.m_PreviousLayer = layer;
			}
		}

		// Token: 0x04000389 RID: 905
		[SerializeField]
		[FormerlySerializedAs("isGlobal")]
		private bool m_IsGlobal = true;

		// Token: 0x0400038A RID: 906
		[Tooltip("When multiple Volumes affect the same settings, Unity uses this value to determine which Volume to use. A Volume with the highest Priority value takes precedence.")]
		public float priority;

		// Token: 0x0400038B RID: 907
		[Tooltip("Sets the outer distance to start blending from. A value of 0 means no blending and Unity applies the Volume overrides immediately upon entry.")]
		public float blendDistance;

		// Token: 0x0400038C RID: 908
		[Range(0f, 1f)]
		[Tooltip("Sets the total weight of this Volume in the Scene. 0 means no effect and 1 means full effect.")]
		public float weight = 1f;

		// Token: 0x0400038D RID: 909
		public VolumeProfile sharedProfile;

		// Token: 0x0400038E RID: 910
		internal List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x0400038F RID: 911
		private int m_PreviousLayer;

		// Token: 0x04000390 RID: 912
		private float m_PreviousPriority;

		// Token: 0x04000391 RID: 913
		private VolumeProfile m_InternalProfile;
	}
}
