using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct EnableIMECompositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00041694 File Offset: 0x0003F894
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'M');
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x000416A3 File Offset: 0x0003F8A3
		public bool imeEnabled
		{
			get
			{
				return this.m_ImeEnabled > 0;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x000416AE File Offset: 0x0003F8AE
		public FourCC typeStatic
		{
			get
			{
				return EnableIMECompositionCommand.Type;
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000416B8 File Offset: 0x0003F8B8
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
