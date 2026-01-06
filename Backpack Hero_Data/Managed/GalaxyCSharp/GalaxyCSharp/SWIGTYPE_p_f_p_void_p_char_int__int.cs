using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020001A0 RID: 416
	public class SWIGTYPE_p_f_p_void_p_char_int__int
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x0001CF4A File Offset: 0x0001B14A
		internal SWIGTYPE_p_f_p_void_p_char_int__int(IntPtr cPtr, bool futureUse)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0001CF5F File Offset: 0x0001B15F
		protected SWIGTYPE_p_f_p_void_p_char_int__int()
		{
			this.swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0001CF78 File Offset: 0x0001B178
		internal static HandleRef getCPtr(SWIGTYPE_p_f_p_void_p_char_int__int obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0400039F RID: 927
		private HandleRef swigCPtr;
	}
}
