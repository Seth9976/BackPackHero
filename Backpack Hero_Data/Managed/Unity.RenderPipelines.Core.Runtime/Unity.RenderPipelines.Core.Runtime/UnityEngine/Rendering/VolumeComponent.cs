using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class VolumeComponent : ScriptableObject
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001CD4E File Offset: 0x0001AF4E
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001CD56 File Offset: 0x0001AF56
		public string displayName { get; protected set; } = "";

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001CD5F File Offset: 0x0001AF5F
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001CD67 File Offset: 0x0001AF67
		public ReadOnlyCollection<VolumeParameter> parameters { get; private set; }

		// Token: 0x0600061F RID: 1567 RVA: 0x0001CD70 File Offset: 0x0001AF70
		internal static void FindParameters(object o, List<VolumeParameter> parameters, Func<FieldInfo, bool> filter = null)
		{
			if (o == null)
			{
				return;
			}
			foreach (FieldInfo fieldInfo in from t in o.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				orderby t.MetadataToken
				select t)
			{
				if (fieldInfo.FieldType.IsSubclassOf(typeof(VolumeParameter)))
				{
					if (filter == null || filter(fieldInfo))
					{
						parameters.Add((VolumeParameter)fieldInfo.GetValue(o));
					}
				}
				else if (!fieldInfo.FieldType.IsArray && fieldInfo.FieldType.IsClass)
				{
					VolumeComponent.FindParameters(fieldInfo.GetValue(o), parameters, filter);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001CE48 File Offset: 0x0001B048
		protected virtual void OnEnable()
		{
			List<VolumeParameter> list = new List<VolumeParameter>();
			VolumeComponent.FindParameters(this, list, null);
			this.parameters = list.AsReadOnly();
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				if (volumeParameter != null)
				{
					volumeParameter.OnEnable();
				}
				else
				{
					Debug.LogWarning("Volume Component " + base.GetType().Name + " contains a null parameter; please make sure all parameters are initialized to a default value. Until this is fixed the null parameters will not be considered by the system.");
				}
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		protected virtual void OnDisable()
		{
			if (this.parameters == null)
			{
				return;
			}
			foreach (VolumeParameter volumeParameter in this.parameters)
			{
				if (volumeParameter != null)
				{
					volumeParameter.OnDisable();
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001CF2C File Offset: 0x0001B12C
		public virtual void Override(VolumeComponent state, float interpFactor)
		{
			int count = this.parameters.Count;
			for (int i = 0; i < count; i++)
			{
				VolumeParameter volumeParameter = state.parameters[i];
				VolumeParameter volumeParameter2 = this.parameters[i];
				if (volumeParameter2.overrideState)
				{
					volumeParameter.overrideState = volumeParameter2.overrideState;
					volumeParameter.Interp(volumeParameter, volumeParameter2, interpFactor);
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001CF88 File Offset: 0x0001B188
		public void SetAllOverridesTo(bool state)
		{
			this.SetOverridesTo(this.parameters, state);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001CF98 File Offset: 0x0001B198
		internal void SetOverridesTo(IEnumerable<VolumeParameter> enumerable, bool state)
		{
			foreach (VolumeParameter volumeParameter in enumerable)
			{
				volumeParameter.overrideState = state;
				Type type = volumeParameter.GetType();
				if (VolumeParameter.IsObjectParameter(type))
				{
					ReadOnlyCollection<VolumeParameter> readOnlyCollection = (ReadOnlyCollection<VolumeParameter>)type.GetProperty("parameters", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(volumeParameter, null);
					if (readOnlyCollection != null)
					{
						this.SetOverridesTo(readOnlyCollection, state);
					}
				}
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001D014 File Offset: 0x0001B214
		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.parameters.Count; i++)
			{
				num = num * 23 + this.parameters[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001D052 File Offset: 0x0001B252
		protected virtual void OnDestroy()
		{
			this.Release();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001D05C File Offset: 0x0001B25C
		public void Release()
		{
			if (this.parameters == null)
			{
				return;
			}
			for (int i = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i] != null)
				{
					this.parameters[i].Release();
				}
			}
		}

		// Token: 0x04000394 RID: 916
		public bool active = true;

		// Token: 0x0200017C RID: 380
		public sealed class Indent : PropertyAttribute
		{
			// Token: 0x0600090B RID: 2315 RVA: 0x00024DBB File Offset: 0x00022FBB
			public Indent(int relativeAmount = 1)
			{
				this.relativeAmount = relativeAmount;
			}

			// Token: 0x040005B9 RID: 1465
			public readonly int relativeAmount;
		}
	}
}
