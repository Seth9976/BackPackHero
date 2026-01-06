using System;
using System.Collections.Generic;

namespace SaveSystem.States
{
	// Token: 0x020000B5 RID: 181
	public class State
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x0001714E File Offset: 0x0001534E
		public object Migrate()
		{
			throw new Exception("Migrate() on base state object called. This should never happen!! Tell BinaryCounter!");
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001715A File Offset: 0x0001535A
		public object Capture()
		{
			throw new Exception("Capture() on base state object called. This should never happen!! Tell BinaryCounter!");
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00017166 File Offset: 0x00015366
		public void Restore()
		{
			throw new Exception("Restore() on base state object called. This should never happen!! Tell BinaryCounter!");
		}

		// Token: 0x040003AE RID: 942
		public static readonly Dictionary<StateType, Type> stateClasses = new Dictionary<StateType, Type>
		{
			{
				StateType.Options,
				typeof(OptionsState)
			},
			{
				StateType.Progress,
				typeof(ProgressState)
			}
		};

		// Token: 0x040003AF RID: 943
		public int version;

		// Token: 0x040003B0 RID: 944
		public StateType type;
	}
}
