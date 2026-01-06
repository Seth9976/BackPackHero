using System;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine.Scripting;

namespace Unity.VisualScripting
{
	// Token: 0x0200000F RID: 15
	public sealed class AotDictionary : OrderedDictionary
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002ADC File Offset: 0x00000CDC
		public AotDictionary()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public AotDictionary(IEqualityComparer comparer)
			: base(comparer)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AED File Offset: 0x00000CED
		public AotDictionary(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public AotDictionary(int capacity, IEqualityComparer comparer)
			: base(capacity, comparer)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B00 File Offset: 0x00000D00
		[Preserve]
		public static void AotStubs()
		{
			AotDictionary aotDictionary = new AotDictionary();
			aotDictionary.Add(null, null);
			aotDictionary.Remove(null);
			object obj = aotDictionary[null];
			aotDictionary[null] = null;
			aotDictionary.Contains(null);
			aotDictionary.Clear();
			int count = aotDictionary.Count;
		}
	}
}
