using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200005A RID: 90
	public class UnexpectedEnumValueException<T> : Exception
	{
		// Token: 0x0600028A RID: 650 RVA: 0x000065A4 File Offset: 0x000047A4
		public UnexpectedEnumValueException(T value)
		{
			string[] array = new string[5];
			array[0] = "Value ";
			int num = 1;
			T t = value;
			array[num] = ((t != null) ? t.ToString() : null);
			array[2] = " of enum ";
			array[3] = typeof(T).Name;
			array[4] = " is unexpected.";
			base..ctor(string.Concat(array));
			this.Value = value;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00006610 File Offset: 0x00004810
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00006618 File Offset: 0x00004818
		public T Value { get; private set; }
	}
}
