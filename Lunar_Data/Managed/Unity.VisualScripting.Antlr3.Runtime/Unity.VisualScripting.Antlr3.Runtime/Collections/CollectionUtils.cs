using System;
using System.Collections;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Collections
{
	// Token: 0x02000047 RID: 71
	public class CollectionUtils
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000854C File Offset: 0x0000754C
		public static string ListToString(IList coll)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (coll != null)
			{
				stringBuilder.Append("[");
				for (int i = 0; i < coll.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					object obj = coll[i];
					if (obj == null)
					{
						stringBuilder.Append("null");
					}
					else if (obj is IDictionary)
					{
						stringBuilder.Append(CollectionUtils.DictionaryToString((IDictionary)obj));
					}
					else if (obj is IList)
					{
						stringBuilder.Append(CollectionUtils.ListToString((IList)obj));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
				}
				stringBuilder.Append("]");
			}
			else
			{
				stringBuilder.Insert(0, "null");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008614 File Offset: 0x00007614
		public static string DictionaryToString(IDictionary dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dict != null)
			{
				stringBuilder.Append("{");
				int num = 0;
				foreach (object obj in dict)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (num > 0)
					{
						stringBuilder.Append(", ");
					}
					if (dictionaryEntry.Value is IDictionary)
					{
						stringBuilder.AppendFormat("{0}={1}", dictionaryEntry.Key.ToString(), CollectionUtils.DictionaryToString((IDictionary)dictionaryEntry.Value));
					}
					else if (dictionaryEntry.Value is IList)
					{
						stringBuilder.AppendFormat("{0}={1}", dictionaryEntry.Key.ToString(), CollectionUtils.ListToString((IList)dictionaryEntry.Value));
					}
					else
					{
						stringBuilder.AppendFormat("{0}={1}", dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
					}
					num++;
				}
				stringBuilder.Append("}");
			}
			else
			{
				stringBuilder.Insert(0, "null");
			}
			return stringBuilder.ToString();
		}
	}
}
