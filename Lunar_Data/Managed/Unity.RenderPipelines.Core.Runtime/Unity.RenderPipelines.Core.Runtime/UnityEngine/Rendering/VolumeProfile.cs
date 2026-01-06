using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E5 RID: 229
	public sealed class VolumeProfile : ScriptableObject
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0001E5DE File Offset: 0x0001C7DE
		private void OnEnable()
		{
			this.components.RemoveAll((VolumeComponent x) => x == null);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001E60C File Offset: 0x0001C80C
		internal void OnDisable()
		{
			if (this.components == null)
			{
				return;
			}
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i] != null)
				{
					this.components[i].Release();
				}
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E65D File Offset: 0x0001C85D
		public void Reset()
		{
			this.isDirty = true;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001E666 File Offset: 0x0001C866
		public T Add<T>(bool overrides = false) where T : VolumeComponent
		{
			return (T)((object)this.Add(typeof(T), overrides));
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001E680 File Offset: 0x0001C880
		public VolumeComponent Add(Type type, bool overrides = false)
		{
			if (this.Has(type))
			{
				throw new InvalidOperationException("Component already exists in the volume");
			}
			VolumeComponent volumeComponent = (VolumeComponent)ScriptableObject.CreateInstance(type);
			volumeComponent.SetAllOverridesTo(overrides);
			this.components.Add(volumeComponent);
			this.isDirty = true;
			return volumeComponent;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public void Remove<T>() where T : VolumeComponent
		{
			this.Remove(typeof(T));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		public void Remove(Type type)
		{
			int num = -1;
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				this.components.RemoveAt(num);
				this.isDirty = true;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E735 File Offset: 0x0001C935
		public bool Has<T>() where T : VolumeComponent
		{
			return this.Has(typeof(T));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E748 File Offset: 0x0001C948
		public bool Has(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public bool HasSubclassOf(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().IsSubclassOf(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E808 File Offset: 0x0001CA08
		public bool TryGet<T>(out T component) where T : VolumeComponent
		{
			return this.TryGet<T>(typeof(T), out component);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E81C File Offset: 0x0001CA1C
		public bool TryGet<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType() == type)
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001E890 File Offset: 0x0001CA90
		public bool TryGetSubclassOf<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001E904 File Offset: 0x0001CB04
		public bool TryGetAllSubclassOf<T>(Type type, List<T> result) where T : VolumeComponent
		{
			int count = result.Count;
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					result.Add((T)((object)volumeComponent));
				}
			}
			return count != result.Count;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001E980 File Offset: 0x0001CB80
		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				num = num * 23 + this.components[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		internal int GetComponentListHashCode()
		{
			int num = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				num = num * 23 + this.components[i].GetType().GetHashCode();
			}
			return num;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001EA04 File Offset: 0x0001CC04
		internal void Sanitize()
		{
			for (int i = this.components.Count - 1; i >= 0; i--)
			{
				if (this.components[i] == null)
				{
					this.components.RemoveAt(i);
				}
			}
		}

		// Token: 0x040003C0 RID: 960
		public List<VolumeComponent> components = new List<VolumeComponent>();

		// Token: 0x040003C1 RID: 961
		[NonSerialized]
		public bool isDirty = true;
	}
}
