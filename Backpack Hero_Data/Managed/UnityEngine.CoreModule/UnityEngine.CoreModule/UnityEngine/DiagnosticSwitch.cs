using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200010A RID: 266
	[NativeClass("DiagnosticSwitch", "struct DiagnosticSwitch;")]
	[NativeAsStruct]
	[NativeHeader("Runtime/Utilities/DiagnosticSwitch.h")]
	[StructLayout(0)]
	internal class DiagnosticSwitch
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00008C2F File Offset: 0x00006E2F
		private DiagnosticSwitch()
		{
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000667 RID: 1639
		public extern string name
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000668 RID: 1640
		public extern string description
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000669 RID: 1641
		[NativeName("OwningModuleName")]
		public extern string owningModule
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600066A RID: 1642
		public extern DiagnosticSwitch.Flags flags
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00008C39 File Offset: 0x00006E39
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00008C41 File Offset: 0x00006E41
		public object value
		{
			get
			{
				return this.GetScriptingValue();
			}
			set
			{
				this.SetScriptingValue(value, false);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600066D RID: 1645
		[NativeName("ScriptingDefaultValue")]
		public extern object defaultValue
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600066E RID: 1646
		[NativeName("ScriptingMinValue")]
		public extern object minValue
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600066F RID: 1647
		[NativeName("ScriptingMaxValue")]
		public extern object maxValue
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00008C4C File Offset: 0x00006E4C
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00008C54 File Offset: 0x00006E54
		public object persistentValue
		{
			get
			{
				return this.GetScriptingPersistentValue();
			}
			set
			{
				this.SetScriptingValue(value, true);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000672 RID: 1650
		[NativeName("ScriptingEnumInfo")]
		public extern EnumInfo enumInfo
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000673 RID: 1651
		[MethodImpl(4096)]
		private extern object GetScriptingValue();

		// Token: 0x06000674 RID: 1652
		[MethodImpl(4096)]
		private extern object GetScriptingPersistentValue();

		// Token: 0x06000675 RID: 1653
		[NativeThrows]
		[MethodImpl(4096)]
		private extern void SetScriptingValue(object value, bool setPersistent);

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00008C5F File Offset: 0x00006E5F
		public bool isSetToDefault
		{
			get
			{
				return object.Equals(this.persistentValue, this.defaultValue);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00008C72 File Offset: 0x00006E72
		public bool needsRestart
		{
			get
			{
				return !object.Equals(this.value, this.persistentValue);
			}
		}

		// Token: 0x0400037D RID: 893
		private IntPtr m_Ptr;

		// Token: 0x0200010B RID: 267
		[Flags]
		internal enum Flags
		{
			// Token: 0x0400037F RID: 895
			None = 0,
			// Token: 0x04000380 RID: 896
			CanChangeAfterEngineStart = 1
		}
	}
}
