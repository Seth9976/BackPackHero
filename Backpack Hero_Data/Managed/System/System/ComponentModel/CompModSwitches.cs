using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000721 RID: 1825
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000C94CF File Offset: 0x000C76CF
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000C94F7 File Offset: 0x000C76F7
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x0400217F RID: 8575
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x04002180 RID: 8576
		private static volatile TraceSwitch eventLog;
	}
}
