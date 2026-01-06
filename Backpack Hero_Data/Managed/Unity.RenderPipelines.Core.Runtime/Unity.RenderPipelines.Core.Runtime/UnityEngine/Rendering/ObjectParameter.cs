using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E3 RID: 227
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ObjectParameter<T> : VolumeParameter<T>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001E44A File Offset: 0x0001C64A
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001E452 File Offset: 0x0001C652
		internal ReadOnlyCollection<VolumeParameter> parameters { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001E45B File Offset: 0x0001C65B
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001E45E File Offset: 0x0001C65E
		public sealed override bool overrideState
		{
			get
			{
				return true;
			}
			set
			{
				this.m_OverrideState = true;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001E467 File Offset: 0x0001C667
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001E470 File Offset: 0x0001C670
		public sealed override T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
				if (this.m_Value == null)
				{
					this.parameters = null;
					return;
				}
				this.parameters = (from t in this.m_Value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
					where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
					orderby t.MetadataToken
					select (VolumeParameter)t.GetValue(this.m_Value)).ToList<VolumeParameter>().AsReadOnly();
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001E51A File Offset: 0x0001C71A
		public ObjectParameter(T value)
		{
			this.m_OverrideState = true;
			this.value = value;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001E530 File Offset: 0x0001C730
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			if (this.m_Value == null)
			{
				return;
			}
			ReadOnlyCollection<VolumeParameter> parameters = this.parameters;
			ReadOnlyCollection<VolumeParameter> parameters2 = ((ObjectParameter<T>)from).parameters;
			ReadOnlyCollection<VolumeParameter> parameters3 = ((ObjectParameter<T>)to).parameters;
			for (int i = 0; i < parameters2.Count; i++)
			{
				parameters[i].overrideState = parameters3[i].overrideState;
				if (parameters3[i].overrideState)
				{
					parameters[i].Interp(parameters2[i], parameters3[i], t);
				}
			}
		}
	}
}
