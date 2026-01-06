using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000005 RID: 5
	public sealed class DictionaryCloner : Cloner<IDictionary>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000021A1 File Offset: 0x000003A1
		public override bool Handles(Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021B4 File Offset: 0x000003B4
		public override void FillClone(Type type, ref IDictionary clone, IDictionary original, CloningContext context)
		{
			IDictionaryEnumerator enumerator = original.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object key = enumerator.Key;
				object value = enumerator.Value;
				object obj = Cloning.Clone(context, key);
				object obj2 = Cloning.Clone(context, value);
				clone.Add(obj, obj2);
			}
		}
	}
}
