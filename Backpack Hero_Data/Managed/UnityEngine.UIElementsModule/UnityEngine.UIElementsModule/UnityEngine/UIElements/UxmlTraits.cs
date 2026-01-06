using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E1 RID: 737
	public abstract class UxmlTraits
	{
		// Token: 0x06001846 RID: 6214 RVA: 0x0006142D File Offset: 0x0005F62D
		protected UxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0006143F File Offset: 0x0005F63F
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x00061447 File Offset: 0x0005F647
		public bool canHaveAnyAttribute { get; protected set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00061450 File Offset: 0x0005F650
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription attributeDescription in this.GetAllAttributeDescriptionForType(base.GetType()))
				{
					yield return attributeDescription;
					attributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00061470 File Offset: 0x0005F670
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000020E6 File Offset: 0x000002E6
		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006148F File Offset: 0x0005F68F
		private IEnumerable<UxmlAttributeDescription> GetAllAttributeDescriptionForType(Type t)
		{
			Type baseType = t.BaseType;
			bool flag = baseType != null;
			if (flag)
			{
				foreach (UxmlAttributeDescription ident in this.GetAllAttributeDescriptionForType(baseType))
				{
					yield return ident;
					ident = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
			}
			foreach (FieldInfo fieldInfo in Enumerable.Where<FieldInfo>(t.GetFields(54), (FieldInfo f) => typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType)))
			{
				yield return (UxmlAttributeDescription)fieldInfo.GetValue(this);
				fieldInfo = null;
			}
			IEnumerator<FieldInfo> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
