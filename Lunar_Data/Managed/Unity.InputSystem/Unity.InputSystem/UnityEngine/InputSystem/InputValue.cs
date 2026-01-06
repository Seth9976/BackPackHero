using System;
using System.Diagnostics;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005A RID: 90
	[DebuggerDisplay("Value = {Get()}")]
	public class InputValue
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0003227C File Offset: 0x0003047C
		public object Get()
		{
			return this.m_Context.Value.ReadValueAsObject();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003229C File Offset: 0x0003049C
		public TValue Get<TValue>() where TValue : struct
		{
			if (this.m_Context == null)
			{
				throw new InvalidOperationException("Values can only be retrieved while in message callbacks");
			}
			return this.m_Context.Value.ReadValue<TValue>();
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000322D4 File Offset: 0x000304D4
		public bool isPressed
		{
			get
			{
				return this.Get<float>() >= ButtonControl.s_GlobalDefaultButtonPressPoint;
			}
		}

		// Token: 0x040002B7 RID: 695
		internal InputAction.CallbackContext? m_Context;
	}
}
