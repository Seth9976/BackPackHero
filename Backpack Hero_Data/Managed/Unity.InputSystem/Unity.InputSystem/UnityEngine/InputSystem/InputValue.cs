using System;
using System.Diagnostics;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005A RID: 90
	[DebuggerDisplay("Value = {Get()}")]
	public class InputValue
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x000322B8 File Offset: 0x000304B8
		public object Get()
		{
			return this.m_Context.Value.ReadValueAsObject();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000322D8 File Offset: 0x000304D8
		public TValue Get<TValue>() where TValue : struct
		{
			if (this.m_Context == null)
			{
				throw new InvalidOperationException("Values can only be retrieved while in message callbacks");
			}
			return this.m_Context.Value.ReadValue<TValue>();
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00032310 File Offset: 0x00030510
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
