using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BD RID: 189
	public abstract class VolumeParameter
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001D9CD File Offset: 0x0001BBCD
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0001D9D5 File Offset: 0x0001BBD5
		public virtual bool overrideState
		{
			get
			{
				return this.m_OverrideState;
			}
			set
			{
				this.m_OverrideState = value;
			}
		}

		// Token: 0x0600064A RID: 1610
		internal abstract void Interp(VolumeParameter from, VolumeParameter to, float t);

		// Token: 0x0600064B RID: 1611 RVA: 0x0001D9DE File Offset: 0x0001BBDE
		public T GetValue<T>()
		{
			return ((VolumeParameter<T>)this).value;
		}

		// Token: 0x0600064C RID: 1612
		public abstract void SetValue(VolumeParameter parameter);

		// Token: 0x0600064D RID: 1613 RVA: 0x0001D9EB File Offset: 0x0001BBEB
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001D9ED File Offset: 0x0001BBED
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001D9EF File Offset: 0x0001BBEF
		public static bool IsObjectParameter(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectParameter<>)) || (type.BaseType != null && VolumeParameter.IsObjectParameter(type.BaseType));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001DA2D File Offset: 0x0001BC2D
		public virtual void Release()
		{
		}

		// Token: 0x040003A1 RID: 929
		public const string k_DebuggerDisplay = "{m_Value} ({m_OverrideState})";

		// Token: 0x040003A2 RID: 930
		[SerializeField]
		protected bool m_OverrideState;
	}
}
