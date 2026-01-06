using System;

namespace UnityEngine
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00025007 File Offset: 0x00023207
		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00025012 File Offset: 0x00023212
		public bool isBroken
		{
			get
			{
				return this.m_InstanceID != 0 && !Object.DoesObjectWithInstanceIDExist(this.m_InstanceID);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00025030 File Offset: 0x00023230
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00025070 File Offset: 0x00023270
		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T t;
				if (flag)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return t;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x000250CA File Offset: 0x000232CA
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x000250D2 File Offset: 0x000232D2
		public int instanceID
		{
			get
			{
				return this.m_InstanceID;
			}
			set
			{
				this.m_InstanceID = value;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000250DC File Offset: 0x000232DC
		public LazyLoadReference(T asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				this.m_InstanceID = 0;
			}
			else
			{
				bool flag2 = !Object.IsPersistent(asset);
				if (flag2)
				{
					throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
				}
				this.m_InstanceID = asset.GetInstanceID();
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00025136 File Offset: 0x00023336
		public LazyLoadReference(int instanceID)
		{
			this.m_InstanceID = instanceID;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00025140 File Offset: 0x00023340
		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00025164 File Offset: 0x00023364
		public static implicit operator LazyLoadReference<T>(int instanceID)
		{
			return new LazyLoadReference<T>
			{
				instanceID = instanceID
			};
		}

		// Token: 0x040007F3 RID: 2035
		private const int kInstanceID_None = 0;

		// Token: 0x040007F4 RID: 2036
		[SerializeField]
		private int m_InstanceID;
	}
}
