using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000196 RID: 406
	public sealed class fsData
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002CEE9 File Offset: 0x0002B0E9
		public override string ToString()
		{
			return fsJsonPrinter.CompressedJson(this);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002CEF1 File Offset: 0x0002B0F1
		public fsData()
		{
			this._value = null;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002CF00 File Offset: 0x0002B100
		public fsData(bool boolean)
		{
			this._value = boolean;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002CF14 File Offset: 0x0002B114
		public fsData(double f)
		{
			this._value = f;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002CF28 File Offset: 0x0002B128
		public fsData(long i)
		{
			this._value = i;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002CF3C File Offset: 0x0002B13C
		public fsData(string str)
		{
			this._value = str;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002CF4B File Offset: 0x0002B14B
		public fsData(Dictionary<string, fsData> dict)
		{
			this._value = dict;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002CF5A File Offset: 0x0002B15A
		public fsData(List<fsData> list)
		{
			this._value = list;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002CF69 File Offset: 0x0002B169
		public static fsData CreateDictionary()
		{
			return new fsData(new Dictionary<string, fsData>(fsGlobalConfig.IsCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002CF88 File Offset: 0x0002B188
		public static fsData CreateList()
		{
			return new fsData(new List<fsData>());
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002CF94 File Offset: 0x0002B194
		public static fsData CreateList(int capacity)
		{
			return new fsData(new List<fsData>(capacity));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002CFA1 File Offset: 0x0002B1A1
		internal void BecomeDictionary()
		{
			this._value = new Dictionary<string, fsData>();
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002CFAE File Offset: 0x0002B1AE
		internal fsData Clone()
		{
			return new fsData
			{
				_value = this._value
			};
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
		public fsDataType Type
		{
			get
			{
				if (this._value == null)
				{
					return fsDataType.Null;
				}
				if (this._value is double)
				{
					return fsDataType.Double;
				}
				if (this._value is long)
				{
					return fsDataType.Int64;
				}
				if (this._value is bool)
				{
					return fsDataType.Boolean;
				}
				if (this._value is string)
				{
					return fsDataType.String;
				}
				if (this._value is Dictionary<string, fsData>)
				{
					return fsDataType.Object;
				}
				if (this._value is List<fsData>)
				{
					return fsDataType.Array;
				}
				throw new InvalidOperationException("unknown JSON data type");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0002D03F File Offset: 0x0002B23F
		public bool IsNull
		{
			get
			{
				return this._value == null;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002D04A File Offset: 0x0002B24A
		public bool IsDouble
		{
			get
			{
				return this._value is double;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002D05A File Offset: 0x0002B25A
		public bool IsInt64
		{
			get
			{
				return this._value is long;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002D06A File Offset: 0x0002B26A
		public bool IsBool
		{
			get
			{
				return this._value is bool;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D07A File Offset: 0x0002B27A
		public bool IsString
		{
			get
			{
				return this._value is string;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002D08A File Offset: 0x0002B28A
		public bool IsDictionary
		{
			get
			{
				return this._value is Dictionary<string, fsData>;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002D09A File Offset: 0x0002B29A
		public bool IsList
		{
			get
			{
				return this._value is List<fsData>;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002D0AA File Offset: 0x0002B2AA
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public double AsDouble
		{
			get
			{
				return this.Cast<double>();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002D0B2 File Offset: 0x0002B2B2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public long AsInt64
		{
			get
			{
				return this.Cast<long>();
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002D0BA File Offset: 0x0002B2BA
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool AsBool
		{
			get
			{
				return this.Cast<bool>();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002D0C2 File Offset: 0x0002B2C2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string AsString
		{
			get
			{
				return this.Cast<string>();
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002D0CA File Offset: 0x0002B2CA
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public Dictionary<string, fsData> AsDictionary
		{
			get
			{
				return this.Cast<Dictionary<string, fsData>>();
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002D0D2 File Offset: 0x0002B2D2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public List<fsData> AsList
		{
			get
			{
				return this.Cast<List<fsData>>();
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		private T Cast<T>()
		{
			if (this._value is T)
			{
				return (T)((object)this._value);
			}
			string[] array = new string[6];
			array[0] = "Unable to cast <";
			array[1] = ((this != null) ? this.ToString() : null);
			array[2] = "> (with type = ";
			int num = 3;
			Type type = this._value.GetType();
			array[num] = ((type != null) ? type.ToString() : null);
			array[4] = ") to type ";
			int num2 = 5;
			Type typeFromHandle = typeof(T);
			array[num2] = ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
			throw new InvalidCastException(string.Concat(array));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002D16D File Offset: 0x0002B36D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as fsData);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002D17C File Offset: 0x0002B37C
		public bool Equals(fsData other)
		{
			if (other == null || this.Type != other.Type)
			{
				return false;
			}
			switch (this.Type)
			{
			case fsDataType.Array:
			{
				List<fsData> asList = this.AsList;
				List<fsData> asList2 = other.AsList;
				if (asList.Count != asList2.Count)
				{
					return false;
				}
				for (int i = 0; i < asList.Count; i++)
				{
					if (!asList[i].Equals(asList2[i]))
					{
						return false;
					}
				}
				return true;
			}
			case fsDataType.Object:
			{
				Dictionary<string, fsData> asDictionary = this.AsDictionary;
				Dictionary<string, fsData> asDictionary2 = other.AsDictionary;
				if (asDictionary.Count != asDictionary2.Count)
				{
					return false;
				}
				foreach (string text in asDictionary.Keys)
				{
					if (!asDictionary2.ContainsKey(text))
					{
						return false;
					}
					if (!asDictionary[text].Equals(asDictionary2[text]))
					{
						return false;
					}
				}
				return true;
			}
			case fsDataType.Double:
				return this.AsDouble == other.AsDouble || Math.Abs(this.AsDouble - other.AsDouble) < double.Epsilon;
			case fsDataType.Int64:
				return this.AsInt64 == other.AsInt64;
			case fsDataType.Boolean:
				return this.AsBool == other.AsBool;
			case fsDataType.String:
				return this.AsString == other.AsString;
			case fsDataType.Null:
				return true;
			default:
				throw new Exception("Unknown data type");
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002D31C File Offset: 0x0002B51C
		public static bool operator ==(fsData a, fsData b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.IsDouble && b.IsDouble)
			{
				return Math.Abs(a.AsDouble - b.AsDouble) < double.Epsilon;
			}
			return a.Equals(b);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002D36C File Offset: 0x0002B56C
		public static bool operator !=(fsData a, fsData b)
		{
			return !(a == b);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002D378 File Offset: 0x0002B578
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x04000282 RID: 642
		private object _value;

		// Token: 0x04000283 RID: 643
		public static readonly fsData True = new fsData(true);

		// Token: 0x04000284 RID: 644
		public static readonly fsData False = new fsData(false);

		// Token: 0x04000285 RID: 645
		public static readonly fsData Null = new fsData();
	}
}
