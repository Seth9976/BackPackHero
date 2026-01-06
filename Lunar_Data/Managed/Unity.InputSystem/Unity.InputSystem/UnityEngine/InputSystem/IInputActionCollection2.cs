using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000016 RID: 22
	public interface IInputActionCollection2 : IInputActionCollection, IEnumerable<InputAction>, IEnumerable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000128 RID: 296
		IEnumerable<InputBinding> bindings { get; }

		// Token: 0x06000129 RID: 297
		InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false);

		// Token: 0x0600012A RID: 298
		int FindBinding(InputBinding mask, out InputAction action);
	}
}
