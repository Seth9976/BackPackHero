using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F2 RID: 754
	internal class BasicFieldNameLookup
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x0009E961 File Offset: 0x0009CB61
		public BasicFieldNameLookup(string[] fieldNames)
		{
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0009E980 File Offset: 0x0009CB80
		public BasicFieldNameLookup(ReadOnlyCollection<string> columnNames)
		{
			int count = columnNames.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = columnNames[i];
			}
			this._fieldNames = array;
			this.GenerateLookup();
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0009E9C4 File Offset: 0x0009CBC4
		public BasicFieldNameLookup(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0009EA04 File Offset: 0x0009CC04
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw ADP.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (-1 == num)
			{
				throw ADP.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0009EA34 File Offset: 0x0009CC34
		public int IndexOfName(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0009EA64 File Offset: 0x0009CC64
		public int IndexOf(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (-1 == num)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0009EAA7 File Offset: 0x0009CCA7
		protected virtual CompareInfo GetCompareInfo()
		{
			return CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0009EAB4 File Offset: 0x0009CCB4
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			if (this._compareInfo == null)
			{
				this._compareInfo = this.GetCompareInfo();
			}
			int num = this._fieldNames.Length;
			for (int i = 0; i < num; i++)
			{
				if (this._compareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0009EB14 File Offset: 0x0009CD14
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(num);
			int num2 = num - 1;
			while (0 <= num2)
			{
				string text = this._fieldNames[num2];
				dictionary[text] = num2;
				num2--;
			}
			this._fieldNameLookup = dictionary;
		}

		// Token: 0x04001718 RID: 5912
		private Dictionary<string, int> _fieldNameLookup;

		// Token: 0x04001719 RID: 5913
		private readonly string[] _fieldNames;

		// Token: 0x0400171A RID: 5914
		private CompareInfo _compareInfo;
	}
}
