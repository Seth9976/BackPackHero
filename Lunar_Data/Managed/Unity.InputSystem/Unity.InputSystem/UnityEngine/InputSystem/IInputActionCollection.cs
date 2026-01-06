using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000015 RID: 21
	public interface IInputActionCollection : IEnumerable<InputAction>, IEnumerable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000120 RID: 288
		// (set) Token: 0x06000121 RID: 289
		InputBinding? bindingMask { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000122 RID: 290
		// (set) Token: 0x06000123 RID: 291
		ReadOnlyArray<InputDevice>? devices { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000124 RID: 292
		ReadOnlyArray<InputControlScheme> controlSchemes { get; }

		// Token: 0x06000125 RID: 293
		bool Contains(InputAction action);

		// Token: 0x06000126 RID: 294
		void Enable();

		// Token: 0x06000127 RID: 295
		void Disable();
	}
}
