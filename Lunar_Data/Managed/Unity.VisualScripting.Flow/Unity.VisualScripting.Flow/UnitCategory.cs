using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;

namespace Unity.VisualScripting
{
	// Token: 0x0200017E RID: 382
	[AttributeUsage(AttributeTargets.Class)]
	[fsObject(Converter = typeof(UnitCategoryConverter))]
	public class UnitCategory : Attribute
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00012610 File Offset: 0x00010810
		public UnitCategory(string fullName)
		{
			Ensure.That("fullName").IsNotNull(fullName);
			fullName = fullName.Replace('\\', '/');
			this.fullName = fullName;
			string[] array = fullName.Split('/', StringSplitOptions.None);
			this.name = array[array.Length - 1];
			if (array.Length > 1)
			{
				this.root = new UnitCategory(array[0]);
				this.parent = new UnitCategory(fullName.Substring(0, fullName.LastIndexOf('/')));
				return;
			}
			this.root = this;
			this.isRoot = true;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001269A File Offset: 0x0001089A
		public UnitCategory root { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000126A2 File Offset: 0x000108A2
		public UnitCategory parent { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000126AA File Offset: 0x000108AA
		public string fullName { get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000126B2 File Offset: 0x000108B2
		public string name { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000126BA File Offset: 0x000108BA
		public bool isRoot { get; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000126C2 File Offset: 0x000108C2
		public IEnumerable<UnitCategory> ancestors
		{
			get
			{
				UnitCategory ancestor = this.parent;
				while (ancestor != null)
				{
					yield return ancestor;
					ancestor = ancestor.parent;
				}
				yield break;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000126D2 File Offset: 0x000108D2
		public IEnumerable<UnitCategory> AndAncestors()
		{
			yield return this;
			foreach (UnitCategory unitCategory in this.ancestors)
			{
				yield return unitCategory;
			}
			IEnumerator<UnitCategory> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000126E2 File Offset: 0x000108E2
		public override bool Equals(object obj)
		{
			return obj is UnitCategory && ((UnitCategory)obj).fullName == this.fullName;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00012704 File Offset: 0x00010904
		public override int GetHashCode()
		{
			return this.fullName.GetHashCode();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00012711 File Offset: 0x00010911
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00012719 File Offset: 0x00010919
		public static bool operator ==(UnitCategory a, UnitCategory b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00012730 File Offset: 0x00010930
		public static bool operator !=(UnitCategory a, UnitCategory b)
		{
			return !(a == b);
		}
	}
}
