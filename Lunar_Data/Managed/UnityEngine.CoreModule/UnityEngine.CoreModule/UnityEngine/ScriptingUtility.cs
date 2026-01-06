using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000214 RID: 532
	internal class ScriptingUtility
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x000256E4 File Offset: 0x000238E4
		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}

		// Token: 0x02000215 RID: 533
		private struct TestClass
		{
			// Token: 0x040007FE RID: 2046
			public int value;
		}
	}
}
