using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000226 RID: 550
	public struct StylePropertyNameCollection : IEnumerable<StylePropertyName>, IEnumerable
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x0004022A File Offset: 0x0003E42A
		internal StylePropertyNameCollection(List<StylePropertyName> list)
		{
			this.propertiesList = list;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00040234 File Offset: 0x0003E434
		public StylePropertyNameCollection.Enumerator GetEnumerator()
		{
			return new StylePropertyNameCollection.Enumerator(this.propertiesList.GetEnumerator());
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00040258 File Offset: 0x0003E458
		IEnumerator<StylePropertyName> IEnumerable<StylePropertyName>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00040278 File Offset: 0x0003E478
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00040298 File Offset: 0x0003E498
		public bool Contains(StylePropertyName stylePropertyName)
		{
			bool flag2;
			using (List<StylePropertyName>.Enumerator enumerator = this.propertiesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StylePropertyName stylePropertyName2 = enumerator.Current;
					bool flag = stylePropertyName2 == stylePropertyName;
					if (flag)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0400072E RID: 1838
		internal List<StylePropertyName> propertiesList;

		// Token: 0x02000227 RID: 551
		public struct Enumerator : IEnumerator<StylePropertyName>, IEnumerator, IDisposable
		{
			// Token: 0x06001083 RID: 4227 RVA: 0x000402FC File Offset: 0x0003E4FC
			internal Enumerator(List<StylePropertyName>.Enumerator enumerator)
			{
				this.m_Enumerator = enumerator;
			}

			// Token: 0x06001084 RID: 4228 RVA: 0x00040306 File Offset: 0x0003E506
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x06001085 RID: 4229 RVA: 0x00040313 File Offset: 0x0003E513
			public StylePropertyName Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x06001086 RID: 4230 RVA: 0x00040320 File Offset: 0x0003E520
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001087 RID: 4231 RVA: 0x000020E6 File Offset: 0x000002E6
			public void Reset()
			{
			}

			// Token: 0x06001088 RID: 4232 RVA: 0x0004032D File Offset: 0x0003E52D
			public void Dispose()
			{
				this.m_Enumerator.Dispose();
			}

			// Token: 0x0400072F RID: 1839
			private List<StylePropertyName>.Enumerator m_Enumerator;
		}
	}
}
