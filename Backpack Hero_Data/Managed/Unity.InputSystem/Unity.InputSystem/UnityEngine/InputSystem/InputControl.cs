using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002E RID: 46
	[DebuggerDisplay("{DebuggerDisplay(),nq}")]
	public abstract class InputControl
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000F6A6 File Offset: 0x0000D8A6
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public string displayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_DisplayName != null)
				{
					return this.m_DisplayName;
				}
				if (this.m_DisplayNameFromLayout != null)
				{
					return this.m_DisplayNameFromLayout;
				}
				return this.m_Name;
			}
			protected set
			{
				this.m_DisplayName = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000F6ED File Offset: 0x0000D8ED
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000F714 File Offset: 0x0000D914
		public string shortDisplayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_ShortDisplayName != null)
				{
					return this.m_ShortDisplayName;
				}
				if (this.m_ShortDisplayNameFromLayout != null)
				{
					return this.m_ShortDisplayNameFromLayout;
				}
				return null;
			}
			protected set
			{
				this.m_ShortDisplayName = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000F71D File Offset: 0x0000D91D
		public string path
		{
			get
			{
				if (this.m_Path == null)
				{
					this.m_Path = InputControlPath.Combine(this.m_Parent, this.m_Name);
				}
				return this.m_Path;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000F749 File Offset: 0x0000D949
		public string layout
		{
			get
			{
				return this.m_Layout;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F756 File Offset: 0x0000D956
		public string variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F763 File Offset: 0x0000D963
		public InputDevice device
		{
			get
			{
				return this.m_Device;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F76B File Offset: 0x0000D96B
		public InputControl parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000F773 File Offset: 0x0000D973
		public ReadOnlyArray<InputControl> children
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Device.m_ChildrenForEachControl, this.m_ChildStartIndex, this.m_ChildCount);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F791 File Offset: 0x0000D991
		public ReadOnlyArray<InternedString> usages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_UsagesForEachControl, this.m_UsageStartIndex, this.m_UsageCount);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000F7AF File Offset: 0x0000D9AF
		public ReadOnlyArray<InternedString> aliases
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_AliasesForEachControl, this.m_AliasStartIndex, this.m_AliasCount);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F7CD File Offset: 0x0000D9CD
		public InputStateBlock stateBlock
		{
			get
			{
				return this.m_StateBlock;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public bool noisy
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsNoisy) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsNoisy;
					ReadOnlyArray<InputControl> children = this.children;
					for (int i = 0; i < children.Count; i++)
					{
						if (children[i] != null)
						{
							children[i].noisy = true;
						}
					}
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsNoisy;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000F843 File Offset: 0x0000DA43
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000F850 File Offset: 0x0000DA50
		public bool synthetic
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsSynthetic) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsSynthetic;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsSynthetic;
			}
		}

		// Token: 0x17000102 RID: 258
		public InputControl this[string path]
		{
			get
			{
				InputControl inputControl = InputControlPath.TryFindChild(this, path, 0);
				if (inputControl == null)
				{
					throw new KeyNotFoundException(string.Format("Cannot find control '{0}' as child of '{1}'", path, this));
				}
				return inputControl;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000396 RID: 918
		public abstract Type valueType { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000397 RID: 919
		public abstract int valueSizeInBytes { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000F892 File Offset: 0x0000DA92
		public float magnitude
		{
			get
			{
				return this.EvaluateMagnitude();
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000F89A File Offset: 0x0000DA9A
		public override string ToString()
		{
			return this.layout + ":" + this.path;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		private string DebuggerDisplay()
		{
			if (!this.device.added)
			{
				return this.ToString();
			}
			string text;
			try
			{
				text = string.Format("{0}:{1}={2}", this.layout, this.path, this.ReadValueAsObject());
			}
			catch (Exception)
			{
				text = this.ToString();
			}
			return text;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000F910 File Offset: 0x0000DB10
		public float EvaluateMagnitude()
		{
			return this.EvaluateMagnitude(this.currentStatePtr);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000F91E File Offset: 0x0000DB1E
		public unsafe virtual float EvaluateMagnitude(void* statePtr)
		{
			return -1f;
		}

		// Token: 0x0600039D RID: 925
		public unsafe abstract object ReadValueFromBufferAsObject(void* buffer, int bufferSize);

		// Token: 0x0600039E RID: 926
		public unsafe abstract object ReadValueFromStateAsObject(void* statePtr);

		// Token: 0x0600039F RID: 927
		public unsafe abstract void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize);

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F925 File Offset: 0x0000DB25
		public unsafe virtual void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000F937 File Offset: 0x0000DB37
		public unsafe virtual void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003A2 RID: 930
		public unsafe abstract bool CompareValue(void* firstStatePtr, void* secondStatePtr);

		// Token: 0x060003A3 RID: 931 RVA: 0x0000F949 File Offset: 0x0000DB49
		public InputControl TryGetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			return InputControlPath.TryFindChild(this, path, 0);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000F968 File Offset: 0x0000DB68
		public TControl TryGetChildControl<TControl>(string path) where TControl : InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl inputControl = this.TryGetChildControl(path);
			if (inputControl == null)
			{
				return default(TControl);
			}
			TControl tcontrol = inputControl as TControl;
			if (tcontrol == null)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					inputControl.GetType().Name,
					"' instead!"
				}));
			}
			return tcontrol;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000FA07 File Offset: 0x0000DC07
		public InputControl GetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl inputControl = this.TryGetChildControl(path);
			if (inputControl == null)
			{
				throw new ArgumentException("Cannot find input control '" + this.MakeChildPath(path) + "'", "path");
			}
			return inputControl;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public TControl GetChildControl<TControl>(string path) where TControl : InputControl
		{
			InputControl childControl = this.GetChildControl(path);
			TControl tcontrol = childControl as TControl;
			if (tcontrol == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					childControl.GetType().Name,
					"' instead!"
				}), "path");
			}
			return tcontrol;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000FACC File Offset: 0x0000DCCC
		protected InputControl()
		{
			this.m_StateBlock.byteOffset = 4294967294U;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000FAEF File Offset: 0x0000DCEF
		protected virtual void FinishSetup()
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000FAF1 File Offset: 0x0000DCF1
		protected void RefreshConfigurationIfNeeded()
		{
			if (!this.isConfigUpToDate)
			{
				this.RefreshConfiguration();
				this.isConfigUpToDate = true;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000FB08 File Offset: 0x0000DD08
		protected virtual void RefreshConfiguration()
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000FB0A File Offset: 0x0000DD0A
		protected internal unsafe void* currentStatePtr
		{
			get
			{
				return InputStateBuffers.GetFrontBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000FB17 File Offset: 0x0000DD17
		protected internal unsafe void* previousFrameStatePtr
		{
			get
			{
				return InputStateBuffers.GetBackBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000FB24 File Offset: 0x0000DD24
		protected internal unsafe void* defaultStatePtr
		{
			get
			{
				return InputStateBuffers.s_DefaultStateBuffer;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		protected internal unsafe void* noiseMaskPtr
		{
			get
			{
				return InputStateBuffers.s_NoiseMaskBuffer;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000FB34 File Offset: 0x0000DD34
		protected internal uint stateOffsetRelativeToDeviceRoot
		{
			get
			{
				uint byteOffset = this.device.m_StateBlock.byteOffset;
				return this.m_StateBlock.byteOffset - byteOffset;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000FB5F File Offset: 0x0000DD5F
		public FourCC optimizedControlDataType
		{
			get
			{
				return this.m_OptimizedControlDataType;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000FB67 File Offset: 0x0000DD67
		protected virtual FourCC CalculateOptimizedControlDataType()
		{
			return 0;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public void ApplyParameterChanges()
		{
			this.SetOptimizedControlDataTypeRecursively();
			for (InputControl inputControl = this.parent; inputControl != null; inputControl = inputControl.parent)
			{
				inputControl.SetOptimizedControlDataType();
			}
			this.MarkAsStaleRecursively();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000FBA2 File Offset: 0x0000DDA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetOptimizedControlDataType()
		{
			this.m_OptimizedControlDataType = (InputSettings.optimizedControlsFeatureEnabled ? this.CalculateOptimizedControlDataType() : 0);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void SetOptimizedControlDataTypeRecursively()
		{
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
					inputControl.SetOptimizedControlDataTypeRecursively();
				}
			}
			this.SetOptimizedControlDataType();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000FC24 File Offset: 0x0000DE24
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void EnsureOptimizationTypeHasNotChanged()
		{
			if (!InputSettings.optimizedControlsFeatureEnabled)
			{
				return;
			}
			FourCC fourCC = this.CalculateOptimizedControlDataType();
			if (fourCC != this.optimizedControlDataType)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Control '",
					this.name,
					"' / '",
					this.path,
					"' suddenly changed optimization state due to either format ",
					string.Format("change or control parameters change (was '{0}' but became '{1}'), ", this.optimizedControlDataType, fourCC),
					"this hinders control hot path optimization, please call control.ApplyParameterChanges() after the changes to the control to fix this error."
				}));
				this.m_OptimizedControlDataType = fourCC;
			}
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000FD00 File Offset: 0x0000DF00
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000FD0F File Offset: 0x0000DF0F
		internal bool isSetupFinished
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.SetupFinished) == InputControl.ControlFlags.SetupFinished;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.SetupFinished;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.SetupFinished;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000FD33 File Offset: 0x0000DF33
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000FD40 File Offset: 0x0000DF40
		internal bool isButton
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsButton) == InputControl.ControlFlags.IsButton;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsButton;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsButton;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000FD63 File Offset: 0x0000DF63
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000FD70 File Offset: 0x0000DF70
		internal bool isConfigUpToDate
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.ConfigUpToDate) == InputControl.ControlFlags.ConfigUpToDate;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.ConfigUpToDate;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.ConfigUpToDate;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000FD93 File Offset: 0x0000DF93
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000FDA2 File Offset: 0x0000DFA2
		internal bool dontReset
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.DontReset) == InputControl.ControlFlags.DontReset;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.DontReset;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.DontReset;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000FDC6 File Offset: 0x0000DFC6
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000FDD5 File Offset: 0x0000DFD5
		internal bool usesStateFromOtherControl
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.UsesStateFromOtherControl) == InputControl.ControlFlags.UsesStateFromOtherControl;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.UsesStateFromOtherControl;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.UsesStateFromOtherControl;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000FDF9 File Offset: 0x0000DFF9
		internal bool hasDefaultState
		{
			get
			{
				return !this.m_DefaultState.isEmpty;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000FE0C File Offset: 0x0000E00C
		internal void CallFinishSetupRecursive()
		{
			ReadOnlyArray<InputControl> children = this.children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].CallFinishSetupRecursive();
			}
			this.FinishSetup();
			this.SetOptimizedControlDataTypeRecursively();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000FE4B File Offset: 0x0000E04B
		internal string MakeChildPath(string path)
		{
			if (this is InputDevice)
			{
				return path;
			}
			return this.path + "/" + path;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000FE68 File Offset: 0x0000E068
		internal void BakeOffsetIntoStateBlockRecursive(uint offset)
		{
			this.m_StateBlock.byteOffset = this.m_StateBlock.byteOffset + offset;
			ReadOnlyArray<InputControl> children = this.children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].BakeOffsetIntoStateBlockRecursive(offset);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		internal int GetDeviceIndex()
		{
			int deviceIndex = this.m_Device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot query value of control '",
					this.path,
					"' before '",
					this.device.name,
					"' has been added to system!"
				}));
			}
			return deviceIndex;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000FF0E File Offset: 0x0000E10E
		internal bool IsValueConsideredPressed(float value)
		{
			if (this.isButton)
			{
				return ((ButtonControl)this).IsValueConsideredPressed(value);
			}
			return value >= ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000FF30 File Offset: 0x0000E130
		internal virtual void AddProcessor(object first)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000FF32 File Offset: 0x0000E132
		internal void MarkAsStale()
		{
			this.m_CachedValueIsStale = true;
			this.m_UnprocessedCachedValueIsStale = true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000FF44 File Offset: 0x0000E144
		internal void MarkAsStaleRecursively()
		{
			this.MarkAsStale();
			foreach (InputControl inputControl in this.children)
			{
				inputControl.MarkAsStale();
			}
		}

		// Token: 0x04000118 RID: 280
		protected internal InputStateBlock m_StateBlock;

		// Token: 0x04000119 RID: 281
		internal InternedString m_Name;

		// Token: 0x0400011A RID: 282
		internal string m_Path;

		// Token: 0x0400011B RID: 283
		internal string m_DisplayName;

		// Token: 0x0400011C RID: 284
		internal string m_DisplayNameFromLayout;

		// Token: 0x0400011D RID: 285
		internal string m_ShortDisplayName;

		// Token: 0x0400011E RID: 286
		internal string m_ShortDisplayNameFromLayout;

		// Token: 0x0400011F RID: 287
		internal InternedString m_Layout;

		// Token: 0x04000120 RID: 288
		internal InternedString m_Variants;

		// Token: 0x04000121 RID: 289
		internal InputDevice m_Device;

		// Token: 0x04000122 RID: 290
		internal InputControl m_Parent;

		// Token: 0x04000123 RID: 291
		internal int m_UsageCount;

		// Token: 0x04000124 RID: 292
		internal int m_UsageStartIndex;

		// Token: 0x04000125 RID: 293
		internal int m_AliasCount;

		// Token: 0x04000126 RID: 294
		internal int m_AliasStartIndex;

		// Token: 0x04000127 RID: 295
		internal int m_ChildCount;

		// Token: 0x04000128 RID: 296
		internal int m_ChildStartIndex;

		// Token: 0x04000129 RID: 297
		internal InputControl.ControlFlags m_ControlFlags;

		// Token: 0x0400012A RID: 298
		internal bool m_CachedValueIsStale = true;

		// Token: 0x0400012B RID: 299
		internal bool m_UnprocessedCachedValueIsStale = true;

		// Token: 0x0400012C RID: 300
		internal PrimitiveValue m_DefaultState;

		// Token: 0x0400012D RID: 301
		internal PrimitiveValue m_MinValue;

		// Token: 0x0400012E RID: 302
		internal PrimitiveValue m_MaxValue;

		// Token: 0x0400012F RID: 303
		internal FourCC m_OptimizedControlDataType;

		// Token: 0x02000182 RID: 386
		[Flags]
		internal enum ControlFlags
		{
			// Token: 0x04000837 RID: 2103
			ConfigUpToDate = 1,
			// Token: 0x04000838 RID: 2104
			IsNoisy = 2,
			// Token: 0x04000839 RID: 2105
			IsSynthetic = 4,
			// Token: 0x0400083A RID: 2106
			IsButton = 8,
			// Token: 0x0400083B RID: 2107
			DontReset = 16,
			// Token: 0x0400083C RID: 2108
			SetupFinished = 32,
			// Token: 0x0400083D RID: 2109
			UsesStateFromOtherControl = 64
		}
	}
}
