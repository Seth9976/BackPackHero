using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E6 RID: 230
	public sealed class VolumeStack : IDisposable
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001EA63 File Offset: 0x0001CC63
		internal VolumeStack()
		{
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001EA80 File Offset: 0x0001CC80
		internal void Reload(List<VolumeComponent> componentDefaultStates)
		{
			this.components.Clear();
			this.requiresReset = true;
			List<ValueTuple<VolumeParameter, VolumeParameter>> list = new List<ValueTuple<VolumeParameter, VolumeParameter>>();
			foreach (VolumeComponent volumeComponent in componentDefaultStates)
			{
				Type type = volumeComponent.GetType();
				VolumeComponent volumeComponent2 = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.components.Add(type, volumeComponent2);
				int count = volumeComponent2.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					list.Add(new ValueTuple<VolumeParameter, VolumeParameter>
					{
						Item1 = volumeComponent2.parameters[i],
						Item2 = volumeComponent.parameters[i]
					});
				}
			}
			this.defaultParameters = list.ToArray();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		public T GetComponent<T>() where T : VolumeComponent
		{
			return (T)((object)this.GetComponent(typeof(T)));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001EB84 File Offset: 0x0001CD84
		public VolumeComponent GetComponent(Type type)
		{
			VolumeComponent volumeComponent;
			this.components.TryGetValue(type, out volumeComponent);
			return volumeComponent;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		public void Dispose()
		{
			foreach (KeyValuePair<Type, VolumeComponent> keyValuePair in this.components)
			{
				CoreUtils.Destroy(keyValuePair.Value);
			}
			this.components.Clear();
		}

		// Token: 0x040003C2 RID: 962
		internal readonly Dictionary<Type, VolumeComponent> components = new Dictionary<Type, VolumeComponent>();

		// Token: 0x040003C3 RID: 963
		[TupleElementNames(new string[] { "parameter", "defaultValue" })]
		internal ValueTuple<VolumeParameter, VolumeParameter>[] defaultParameters;

		// Token: 0x040003C4 RID: 964
		internal bool requiresReset = true;
	}
}
