using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B1 RID: 177
	public struct StylePropertyName : IEquatable<StylePropertyName>
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001622F File Offset: 0x0001442F
		internal readonly StylePropertyId id { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00016237 File Offset: 0x00014437
		private readonly string name { get; }

		// Token: 0x060005E6 RID: 1510 RVA: 0x00016240 File Offset: 0x00014440
		internal static StylePropertyId StylePropertyIdFromString(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			StylePropertyId stylePropertyId2;
			if (flag)
			{
				stylePropertyId2 = stylePropertyId;
			}
			else
			{
				stylePropertyId2 = StylePropertyId.Unknown;
			}
			return stylePropertyId2;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001626C File Offset: 0x0001446C
		internal StylePropertyName(StylePropertyId stylePropertyId)
		{
			this.id = stylePropertyId;
			this.name = null;
			string text;
			bool flag = StylePropertyUtil.s_IdToName.TryGetValue(stylePropertyId, ref text);
			if (flag)
			{
				this.name = text;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000162A4 File Offset: 0x000144A4
		public StylePropertyName(string name)
		{
			this.id = StylePropertyName.StylePropertyIdFromString(name);
			this.name = null;
			bool flag = this.id > StylePropertyId.Unknown;
			if (flag)
			{
				this.name = name;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000162DC File Offset: 0x000144DC
		public static bool IsNullOrEmpty(StylePropertyName propertyName)
		{
			return propertyName.id == StylePropertyId.Unknown;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000162F8 File Offset: 0x000144F8
		public static bool operator ==(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001631C File Offset: 0x0001451C
		public static bool operator !=(StylePropertyName lhs, StylePropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00016344 File Offset: 0x00014544
		public static implicit operator StylePropertyName(string name)
		{
			return new StylePropertyName(name);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001635C File Offset: 0x0001455C
		public override int GetHashCode()
		{
			return (int)this.id;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00016374 File Offset: 0x00014574
		public override bool Equals(object other)
		{
			return other is StylePropertyName && this.Equals((StylePropertyName)other);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000163A0 File Offset: 0x000145A0
		public bool Equals(StylePropertyName other)
		{
			return this == other;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000163C0 File Offset: 0x000145C0
		public override string ToString()
		{
			return this.name;
		}
	}
}
