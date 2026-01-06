using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct EnableIMECompositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0004164C File Offset: 0x0003F84C
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'M');
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0004165B File Offset: 0x0003F85B
		public bool imeEnabled
		{
			get
			{
				return this.m_ImeEnabled > 0;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00041666 File Offset: 0x0003F866
		public FourCC typeStatic
		{
			get
			{
				return EnableIMECompositionCommand.Type;
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00041670 File Offset: 0x0003F870
		public static EnableIMECompositionCommand Create(bool enabled)
		{
			return new EnableIMECompositionCommand
			{
				baseCommand = new InputDeviceCommand(EnableIMECompositionCommand.Type, 9),
				m_ImeEnabled = (enabled ? byte.MaxValue : 0)
			};
		}

		// Token: 0x040004AC RID: 1196
		internal const int kSize = 12;

		// Token: 0x040004AD RID: 1197
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004AE RID: 1198
		[FieldOffset(8)]
		private byte m_ImeEnabled;
	}
}
