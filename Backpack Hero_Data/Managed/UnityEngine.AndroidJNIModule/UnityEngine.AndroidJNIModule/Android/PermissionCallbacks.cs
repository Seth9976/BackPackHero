using System;
using System.Diagnostics;

namespace UnityEngine.Android
{
	// Token: 0x0200001D RID: 29
	public class PermissionCallbacks : AndroidJavaProxy
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001C1 RID: 449 RVA: 0x00008238 File Offset: 0x00006438
		// (remove) Token: 0x060001C2 RID: 450 RVA: 0x00008270 File Offset: 0x00006470
		[field: DebuggerBrowsable(0)]
		public event Action<string> PermissionGranted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001C3 RID: 451 RVA: 0x000082A8 File Offset: 0x000064A8
		// (remove) Token: 0x060001C4 RID: 452 RVA: 0x000082E0 File Offset: 0x000064E0
		[field: DebuggerBrowsable(0)]
		public event Action<string> PermissionDenied;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001C5 RID: 453 RVA: 0x00008318 File Offset: 0x00006518
		// (remove) Token: 0x060001C6 RID: 454 RVA: 0x00008350 File Offset: 0x00006550
		[field: DebuggerBrowsable(0)]
		public event Action<string> PermissionDeniedAndDontAskAgain;

		// Token: 0x060001C7 RID: 455 RVA: 0x00008385 File Offset: 0x00006585
		public PermissionCallbacks()
			: base("com.unity3d.player.IPermissionRequestCallbacks")
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008394 File Offset: 0x00006594
		private void onPermissionGranted(string permissionName)
		{
			Action<string> permissionGranted = this.PermissionGranted;
			if (permissionGranted != null)
			{
				permissionGranted.Invoke(permissionName);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000083AA File Offset: 0x000065AA
		private void onPermissionDenied(string permissionName)
		{
			Action<string> permissionDenied = this.PermissionDenied;
			if (permissionDenied != null)
			{
				permissionDenied.Invoke(permissionName);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000083C0 File Offset: 0x000065C0
		private void onPermissionDeniedAndDontAskAgain(string permissionName)
		{
			bool flag = this.PermissionDeniedAndDontAskAgain != null;
			if (flag)
			{
				this.PermissionDeniedAndDontAskAgain.Invoke(permissionName);
			}
			else
			{
				Action<string> permissionDenied = this.PermissionDenied;
				if (permissionDenied != null)
				{
					permissionDenied.Invoke(permissionName);
				}
			}
		}
	}
}
