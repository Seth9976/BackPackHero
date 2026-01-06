using System;

namespace System.Data.Common
{
	// Token: 0x02000336 RID: 822
	public abstract class DbColumn
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000AE977 File Offset: 0x000ACB77
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x000AE97F File Offset: 0x000ACB7F
		public bool? AllowDBNull { get; protected set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000AE988 File Offset: 0x000ACB88
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x000AE990 File Offset: 0x000ACB90
		public string BaseCatalogName { get; protected set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000AE999 File Offset: 0x000ACB99
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000AE9A1 File Offset: 0x000ACBA1
		public string BaseColumnName { get; protected set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000AE9AA File Offset: 0x000ACBAA
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x000AE9B2 File Offset: 0x000ACBB2
		public string BaseSchemaName { get; protected set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000AE9BB File Offset: 0x000ACBBB
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x000AE9C3 File Offset: 0x000ACBC3
		public string BaseServerName { get; protected set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x000AE9CC File Offset: 0x000ACBCC
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x000AE9D4 File Offset: 0x000ACBD4
		public string BaseTableName { get; protected set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000AE9DD File Offset: 0x000ACBDD
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x000AE9E5 File Offset: 0x000ACBE5
		public string ColumnName { get; protected set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000AE9EE File Offset: 0x000ACBEE
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000AE9F6 File Offset: 0x000ACBF6
		public int? ColumnOrdinal { get; protected set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000AE9FF File Offset: 0x000ACBFF
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x000AEA07 File Offset: 0x000ACC07
		public int? ColumnSize { get; protected set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000AEA10 File Offset: 0x000ACC10
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x000AEA18 File Offset: 0x000ACC18
		public bool? IsAliased { get; protected set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000AEA21 File Offset: 0x000ACC21
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x000AEA29 File Offset: 0x000ACC29
		public bool? IsAutoIncrement { get; protected set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x000AEA32 File Offset: 0x000ACC32
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x000AEA3A File Offset: 0x000ACC3A
		public bool? IsExpression { get; protected set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000AEA43 File Offset: 0x000ACC43
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000AEA4B File Offset: 0x000ACC4B
		public bool? IsHidden { get; protected set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000AEA54 File Offset: 0x000ACC54
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x000AEA5C File Offset: 0x000ACC5C
		public bool? IsIdentity { get; protected set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000AEA65 File Offset: 0x000ACC65
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x000AEA6D File Offset: 0x000ACC6D
		public bool? IsKey { get; protected set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000AEA76 File Offset: 0x000ACC76
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x000AEA7E File Offset: 0x000ACC7E
		public bool? IsLong { get; protected set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000AEA87 File Offset: 0x000ACC87
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000AEA8F File Offset: 0x000ACC8F
		public bool? IsReadOnly { get; protected set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000AEA98 File Offset: 0x000ACC98
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x000AEAA0 File Offset: 0x000ACCA0
		public bool? IsUnique { get; protected set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x000AEAA9 File Offset: 0x000ACCA9
		// (set) Token: 0x0600272E RID: 10030 RVA: 0x000AEAB1 File Offset: 0x000ACCB1
		public int? NumericPrecision { get; protected set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000AEABA File Offset: 0x000ACCBA
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x000AEAC2 File Offset: 0x000ACCC2
		public int? NumericScale { get; protected set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000AEACB File Offset: 0x000ACCCB
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x000AEAD3 File Offset: 0x000ACCD3
		public string UdtAssemblyQualifiedName { get; protected set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000AEADC File Offset: 0x000ACCDC
		// (set) Token: 0x06002734 RID: 10036 RVA: 0x000AEAE4 File Offset: 0x000ACCE4
		public Type DataType { get; protected set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000AEAED File Offset: 0x000ACCED
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000AEAF5 File Offset: 0x000ACCF5
		public string DataTypeName { get; protected set; }

		// Token: 0x170006AE RID: 1710
		public virtual object this[string property]
		{
			get
			{
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(property);
				if (num <= 2477638934U)
				{
					if (num <= 1067318116U)
					{
						if (num <= 687909556U)
						{
							if (num != 405521230U)
							{
								if (num == 687909556U)
								{
									if (property == "ColumnOrdinal")
									{
										return this.ColumnOrdinal;
									}
								}
							}
							else if (property == "DataTypeName")
							{
								return this.DataTypeName;
							}
						}
						else if (num != 720006947U)
						{
							if (num != 1005639113U)
							{
								if (num == 1067318116U)
								{
									if (property == "ColumnName")
									{
										return this.ColumnName;
									}
								}
							}
							else if (property == "IsHidden")
							{
								return this.IsHidden;
							}
						}
						else if (property == "IsLong")
						{
							return this.IsLong;
						}
					}
					else if (num <= 2215472237U)
					{
						if (num != 1154057342U)
						{
							if (num != 1309233724U)
							{
								if (num == 2215472237U)
								{
									if (property == "DataType")
									{
										return this.DataType;
									}
								}
							}
							else if (property == "IsKey")
							{
								return this.IsKey;
							}
						}
						else if (property == "ColumnSize")
						{
							return this.ColumnSize;
						}
					}
					else if (num != 2239129947U)
					{
						if (num != 2380251540U)
						{
							if (num == 2477638934U)
							{
								if (property == "IsUnique")
								{
									return this.IsUnique;
								}
							}
						}
						else if (property == "NumericPrecision")
						{
							return this.NumericPrecision;
						}
					}
					else if (property == "IsExpression")
					{
						return this.IsExpression;
					}
				}
				else if (num <= 3042527364U)
				{
					if (num <= 2711511624U)
					{
						if (num != 2504653387U)
						{
							if (num != 2586490225U)
							{
								if (num == 2711511624U)
								{
									if (property == "BaseServerName")
									{
										return this.BaseServerName;
									}
								}
							}
							else if (property == "UdtAssemblyQualifiedName")
							{
								return this.UdtAssemblyQualifiedName;
							}
						}
						else if (property == "IsIdentity")
						{
							return this.IsIdentity;
						}
					}
					else if (num != 2741140585U)
					{
						if (num != 2757192823U)
						{
							if (num == 3042527364U)
							{
								if (property == "BaseCatalogName")
								{
									return this.BaseCatalogName;
								}
							}
						}
						else if (property == "BaseTableName")
						{
							return this.BaseTableName;
						}
					}
					else if (property == "BaseColumnName")
					{
						return this.BaseColumnName;
					}
				}
				else if (num <= 3656290791U)
				{
					if (num != 3115085976U)
					{
						if (num != 3173893005U)
						{
							if (num == 3656290791U)
							{
								if (property == "IsReadOnly")
								{
									return this.IsReadOnly;
								}
							}
						}
						else if (property == "AllowDBNull")
						{
							return this.AllowDBNull;
						}
					}
					else if (property == "BaseSchemaName")
					{
						return this.BaseSchemaName;
					}
				}
				else if (num != 3912158903U)
				{
					if (num != 3938522122U)
					{
						if (num == 4233439846U)
						{
							if (property == "IsAliased")
							{
								return this.IsAliased;
							}
						}
					}
					else if (property == "NumericScale")
					{
						return this.NumericScale;
					}
				}
				else if (property == "IsAutoIncrement")
				{
					return this.IsAutoIncrement;
				}
				return null;
			}
		}
	}
}
