using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Pool;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000093 RID: 147
	public class HID : InputDevice
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003E114 File Offset: 0x0003C314
		public static FourCC QueryHIDReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'D');
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0003E123 File Offset: 0x0003C323
		public static FourCC QueryHIDReportDescriptorSizeDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'S');
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0003E132 File Offset: 0x0003C332
		public static FourCC QueryHIDParsedReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'P');
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0003E144 File Offset: 0x0003C344
		public HID.HIDDeviceDescriptor hidDescriptor
		{
			get
			{
				if (!this.m_HaveParsedHIDDescriptor)
				{
					if (!string.IsNullOrEmpty(base.description.capabilities))
					{
						this.m_HIDDescriptor = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(base.description.capabilities);
					}
					this.m_HaveParsedHIDDescriptor = true;
				}
				return this.m_HIDDescriptor;
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0003E194 File Offset: 0x0003C394
		internal static string OnFindLayoutForDevice(ref InputDeviceDescription description, string matchedLayout, InputDeviceExecuteCommandDelegate executeDeviceCommand)
		{
			if (!string.IsNullOrEmpty(matchedLayout))
			{
				return null;
			}
			if (description.interfaceName != "HID")
			{
				return null;
			}
			HID.HIDDeviceDescriptor hiddeviceDescriptor = HID.ReadHIDDeviceDescriptor(ref description, executeDeviceCommand);
			if (!HIDSupport.supportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hiddeviceDescriptor.usagePage, hiddeviceDescriptor.usage)))
			{
				return null;
			}
			bool flag = false;
			if (hiddeviceDescriptor.elements != null)
			{
				foreach (HID.HIDElementDescriptor hidelementDescriptor in hiddeviceDescriptor.elements)
				{
					if (hidelementDescriptor.IsUsableElement())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			Type type = typeof(HID);
			string text = "HID";
			if (hiddeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop && (hiddeviceDescriptor.usage == 4 || hiddeviceDescriptor.usage == 5))
			{
				text = "Joystick";
				type = typeof(Joystick);
			}
			string text2 = "";
			if (text != "Joystick")
			{
				text2 = ((hiddeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop) ? string.Format(" {0}", (HID.GenericDesktop)hiddeviceDescriptor.usage) : string.Format(" {0}-{1}", hiddeviceDescriptor.usagePage, hiddeviceDescriptor.usage));
			}
			InputDeviceMatcher inputDeviceMatcher = InputDeviceMatcher.FromDeviceDescription(description);
			string text3;
			if (!string.IsNullOrEmpty(description.product) && !string.IsNullOrEmpty(description.manufacturer))
			{
				text3 = string.Concat(new string[] { "HID::", description.manufacturer, " ", description.product, text2 });
			}
			else if (!string.IsNullOrEmpty(description.product))
			{
				text3 = "HID::" + description.product + text2;
			}
			else
			{
				if (hiddeviceDescriptor.vendorId == 0)
				{
					return null;
				}
				text3 = string.Format("{0}::{1:X}-{2:X}{3}", new object[] { "HID", hiddeviceDescriptor.vendorId, hiddeviceDescriptor.productId, text2 });
				inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("productId", hiddeviceDescriptor.productId).WithCapability<int>("vendorId", hiddeviceDescriptor.vendorId);
			}
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("usage", hiddeviceDescriptor.usage).WithCapability<HID.UsagePage>("usagePage", hiddeviceDescriptor.usagePage);
			HID.HIDLayoutBuilder layout = new HID.HIDLayoutBuilder
			{
				displayName = description.product,
				hidDescriptor = hiddeviceDescriptor,
				parentLayout = text,
				deviceType = (type ?? typeof(HID))
			};
			InputSystem.RegisterLayoutBuilder(() => layout.Build(), text3, text, new InputDeviceMatcher?(inputDeviceMatcher));
			return text3;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003E43C File Offset: 0x0003C63C
		internal unsafe static HID.HIDDeviceDescriptor ReadHIDDeviceDescriptor(ref InputDeviceDescription deviceDescription, InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (deviceDescription.interfaceName != "HID")
			{
				throw new ArgumentException(string.Format("Device '{0}' is not a HID", deviceDescription));
			}
			bool flag = true;
			HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
			if (!string.IsNullOrEmpty(deviceDescription.capabilities))
			{
				try
				{
					hiddeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(deviceDescription.capabilities);
					if (hiddeviceDescriptor.elements != null && hiddeviceDescriptor.elements.Length != 0)
					{
						flag = false;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
					Debug.LogException(ex);
				}
			}
			if (flag)
			{
				InputDeviceCommand inputDeviceCommand = new InputDeviceCommand(HID.QueryHIDReportDescriptorSizeDeviceCommandType, 8);
				long num = executeCommandDelegate(ref inputDeviceCommand);
				if (num > 0L)
				{
					using (NativeArray<byte> nativeArray = InputDeviceCommand.AllocateNative(HID.QueryHIDReportDescriptorDeviceCommandType, (int)num))
					{
						InputDeviceCommand* unsafePtr = (InputDeviceCommand*)nativeArray.GetUnsafePtr<byte>();
						if (executeCommandDelegate(ref *unsafePtr) != num)
						{
							HID.HIDDeviceDescriptor hiddeviceDescriptor2 = default(HID.HIDDeviceDescriptor);
							return hiddeviceDescriptor2;
						}
						if (!HIDParser.ParseReportDescriptor((byte*)unsafePtr->payloadPtr, (int)num, ref hiddeviceDescriptor))
						{
							return default(HID.HIDDeviceDescriptor);
						}
					}
					deviceDescription.capabilities = hiddeviceDescriptor.ToJson();
					return hiddeviceDescriptor;
				}
				using (NativeArray<byte> nativeArray2 = InputDeviceCommand.AllocateNative(HID.QueryHIDParsedReportDescriptorDeviceCommandType, 2097152))
				{
					InputDeviceCommand* unsafePtr2 = (InputDeviceCommand*)nativeArray2.GetUnsafePtr<byte>();
					long num2 = executeCommandDelegate(ref *unsafePtr2);
					if (num2 < 0L)
					{
						return default(HID.HIDDeviceDescriptor);
					}
					byte[] array = new byte[num2];
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						UnsafeUtility.MemCpy((void*)ptr, unsafePtr2->payloadPtr, num2);
					}
					finally
					{
						byte[] array2 = null;
					}
					string @string = Encoding.UTF8.GetString(array, 0, (int)num2);
					try
					{
						hiddeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(@string);
					}
					catch (Exception ex2)
					{
						Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
						Debug.LogException(ex2);
						return default(HID.HIDDeviceDescriptor);
					}
					deviceDescription.capabilities = @string;
				}
				return hiddeviceDescriptor;
			}
			return hiddeviceDescriptor;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003E688 File Offset: 0x0003C888
		public static string UsagePageToString(HID.UsagePage usagePage)
		{
			if (usagePage < HID.UsagePage.VendorDefined)
			{
				return usagePage.ToString();
			}
			return "Vendor-Defined";
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		public static string UsageToString(HID.UsagePage usagePage, int usage)
		{
			if (usagePage == HID.UsagePage.GenericDesktop)
			{
				HID.GenericDesktop genericDesktop = (HID.GenericDesktop)usage;
				return genericDesktop.ToString();
			}
			if (usagePage != HID.UsagePage.Simulation)
			{
				return null;
			}
			HID.Simulation simulation = (HID.Simulation)usage;
			return simulation.ToString();
		}

		// Token: 0x0400042C RID: 1068
		internal const string kHIDInterface = "HID";

		// Token: 0x0400042D RID: 1069
		internal const string kHIDNamespace = "HID";

		// Token: 0x0400042E RID: 1070
		private bool m_HaveParsedHIDDescriptor;

		// Token: 0x0400042F RID: 1071
		private HID.HIDDeviceDescriptor m_HIDDescriptor;

		// Token: 0x020001D9 RID: 473
		[Serializable]
		private class HIDLayoutBuilder
		{
			// Token: 0x0600142D RID: 5165 RVA: 0x0005D2D4 File Offset: 0x0005B4D4
			public InputControlLayout Build()
			{
				InputControlLayout.Builder builder = new InputControlLayout.Builder
				{
					displayName = this.displayName,
					type = this.deviceType,
					extendsLayout = this.parentLayout,
					stateFormat = new FourCC('H', 'I', 'D', ' ')
				};
				HID.HIDElementDescriptor hidelementDescriptor = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 48);
				HID.HIDElementDescriptor hidelementDescriptor2 = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 49);
				bool flag = hidelementDescriptor.usage == 48 && hidelementDescriptor2.usage == 49;
				if (flag)
				{
					int num;
					int num2;
					int num3;
					if (hidelementDescriptor.reportOffsetInBits <= hidelementDescriptor2.reportOffsetInBits)
					{
						num = hidelementDescriptor.reportOffsetInBits % 8;
						num2 = hidelementDescriptor.reportOffsetInBits / 8;
						num3 = hidelementDescriptor2.reportOffsetInBits + hidelementDescriptor2.reportSizeInBits - hidelementDescriptor.reportOffsetInBits;
					}
					else
					{
						num = hidelementDescriptor2.reportOffsetInBits % 8;
						num2 = hidelementDescriptor2.reportOffsetInBits / 8;
						num3 = hidelementDescriptor.reportOffsetInBits + hidelementDescriptor.reportSizeInBits - hidelementDescriptor2.reportSizeInBits;
					}
					InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl("stick");
					controlBuilder = controlBuilder.WithDisplayName("Stick");
					controlBuilder = controlBuilder.WithLayout("Stick");
					controlBuilder = controlBuilder.WithBitOffset((uint)num);
					controlBuilder = controlBuilder.WithByteOffset((uint)num2);
					controlBuilder = controlBuilder.WithSizeInBits((uint)num3);
					controlBuilder.WithUsages(new InternedString[] { CommonUsages.Primary2DMotion });
					string text = hidelementDescriptor.DetermineParameters();
					string text2 = hidelementDescriptor2.DetermineParameters();
					controlBuilder = builder.AddControl("stick/x");
					controlBuilder = controlBuilder.WithFormat(hidelementDescriptor.isSigned ? InputStateBlock.FormatSBit : InputStateBlock.FormatBit);
					controlBuilder = controlBuilder.WithByteOffset((uint)(hidelementDescriptor.reportOffsetInBits / 8 - num2));
					controlBuilder = controlBuilder.WithBitOffset((uint)(hidelementDescriptor.reportOffsetInBits % 8));
					controlBuilder = controlBuilder.WithSizeInBits((uint)hidelementDescriptor.reportSizeInBits);
					controlBuilder = controlBuilder.WithParameters(text);
					controlBuilder = controlBuilder.WithDefaultState(hidelementDescriptor.DetermineDefaultState());
					controlBuilder.WithProcessors(hidelementDescriptor.DetermineProcessors());
					controlBuilder = builder.AddControl("stick/y");
					controlBuilder = controlBuilder.WithFormat(hidelementDescriptor2.isSigned ? InputStateBlock.FormatSBit : InputStateBlock.FormatBit);
					controlBuilder = controlBuilder.WithByteOffset((uint)(hidelementDescriptor2.reportOffsetInBits / 8 - num2));
					controlBuilder = controlBuilder.WithBitOffset((uint)(hidelementDescriptor2.reportOffsetInBits % 8));
					controlBuilder = controlBuilder.WithSizeInBits((uint)hidelementDescriptor2.reportSizeInBits);
					controlBuilder = controlBuilder.WithParameters(text2);
					controlBuilder = controlBuilder.WithDefaultState(hidelementDescriptor2.DetermineDefaultState());
					controlBuilder.WithProcessors(hidelementDescriptor2.DetermineProcessors());
					controlBuilder = builder.AddControl("stick/up");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { text2, "clamp=2,clampMin=-1,clampMax=0,invert=true" }));
					controlBuilder = builder.AddControl("stick/down");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { text2, "clamp=2,clampMin=0,clampMax=1,invert=false" }));
					controlBuilder = builder.AddControl("stick/left");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { text, "clamp=2,clampMin=-1,clampMax=0,invert" }));
					controlBuilder = builder.AddControl("stick/right");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { text, "clamp=2,clampMin=0,clampMax=1" }));
				}
				HID.HIDElementDescriptor[] elements = this.hidDescriptor.elements;
				int num4 = elements.Length;
				for (int i = 0; i < num4; i++)
				{
					ref HID.HIDElementDescriptor ptr = ref elements[i];
					if (ptr.reportType == HID.HIDReportType.Input && (!flag || (!ptr.Is(HID.UsagePage.GenericDesktop, 48) && !ptr.Is(HID.UsagePage.GenericDesktop, 49))))
					{
						string text3 = ptr.DetermineLayout();
						if (text3 != null)
						{
							string text4 = ptr.DetermineName();
							text4 = StringHelpers.MakeUniqueName<InputControlLayout.ControlItem>(text4, builder.controls, (InputControlLayout.ControlItem x) => x.name);
							InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl(text4);
							controlBuilder = controlBuilder.WithDisplayName(ptr.DetermineDisplayName());
							controlBuilder = controlBuilder.WithLayout(text3);
							controlBuilder = controlBuilder.WithByteOffset((uint)(ptr.reportOffsetInBits / 8));
							controlBuilder = controlBuilder.WithBitOffset((uint)(ptr.reportOffsetInBits % 8));
							controlBuilder = controlBuilder.WithSizeInBits((uint)ptr.reportSizeInBits);
							controlBuilder = controlBuilder.WithFormat(ptr.DetermineFormat());
							controlBuilder = controlBuilder.WithDefaultState(ptr.DetermineDefaultState());
							InputControlLayout.Builder.ControlBuilder controlBuilder2 = controlBuilder.WithProcessors(ptr.DetermineProcessors());
							string text5 = ptr.DetermineParameters();
							if (!string.IsNullOrEmpty(text5))
							{
								controlBuilder2.WithParameters(text5);
							}
							InternedString[] array = ptr.DetermineUsages();
							if (array != null)
							{
								controlBuilder2.WithUsages(array);
							}
							ptr.AddChildControls(ref ptr, text4, ref builder);
						}
					}
				}
				return builder.Build();
			}

			// Token: 0x040009A7 RID: 2471
			public string displayName;

			// Token: 0x040009A8 RID: 2472
			public HID.HIDDeviceDescriptor hidDescriptor;

			// Token: 0x040009A9 RID: 2473
			public string parentLayout;

			// Token: 0x040009AA RID: 2474
			public Type deviceType;
		}

		// Token: 0x020001DA RID: 474
		public enum HIDReportType
		{
			// Token: 0x040009AC RID: 2476
			Unknown,
			// Token: 0x040009AD RID: 2477
			Input,
			// Token: 0x040009AE RID: 2478
			Output,
			// Token: 0x040009AF RID: 2479
			Feature
		}

		// Token: 0x020001DB RID: 475
		public enum HIDCollectionType
		{
			// Token: 0x040009B1 RID: 2481
			Physical,
			// Token: 0x040009B2 RID: 2482
			Application,
			// Token: 0x040009B3 RID: 2483
			Logical,
			// Token: 0x040009B4 RID: 2484
			Report,
			// Token: 0x040009B5 RID: 2485
			NamedArray,
			// Token: 0x040009B6 RID: 2486
			UsageSwitch,
			// Token: 0x040009B7 RID: 2487
			UsageModifier
		}

		// Token: 0x020001DC RID: 476
		[Flags]
		public enum HIDElementFlags
		{
			// Token: 0x040009B9 RID: 2489
			Constant = 1,
			// Token: 0x040009BA RID: 2490
			Variable = 2,
			// Token: 0x040009BB RID: 2491
			Relative = 4,
			// Token: 0x040009BC RID: 2492
			Wrap = 8,
			// Token: 0x040009BD RID: 2493
			NonLinear = 16,
			// Token: 0x040009BE RID: 2494
			NoPreferred = 32,
			// Token: 0x040009BF RID: 2495
			NullState = 64,
			// Token: 0x040009C0 RID: 2496
			Volatile = 128,
			// Token: 0x040009C1 RID: 2497
			BufferedBytes = 256
		}

		// Token: 0x020001DD RID: 477
		[Serializable]
		public struct HIDElementDescriptor
		{
			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x0600142F RID: 5167 RVA: 0x0005D7D6 File Offset: 0x0005B9D6
			public bool hasNullState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NullState) == HID.HIDElementFlags.NullState;
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001430 RID: 5168 RVA: 0x0005D7E5 File Offset: 0x0005B9E5
			public bool hasPreferredState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NoPreferred) != HID.HIDElementFlags.NoPreferred;
				}
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005D7F7 File Offset: 0x0005B9F7
			public bool isArray
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Variable) != HID.HIDElementFlags.Variable;
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x06001432 RID: 5170 RVA: 0x0005D807 File Offset: 0x0005BA07
			public bool isNonLinear
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NonLinear) == HID.HIDElementFlags.NonLinear;
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005D816 File Offset: 0x0005BA16
			public bool isRelative
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Relative) == HID.HIDElementFlags.Relative;
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001434 RID: 5172 RVA: 0x0005D823 File Offset: 0x0005BA23
			public bool isConstant
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Constant) == HID.HIDElementFlags.Constant;
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06001435 RID: 5173 RVA: 0x0005D830 File Offset: 0x0005BA30
			public bool isWrapping
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Wrap) == HID.HIDElementFlags.Wrap;
				}
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x06001436 RID: 5174 RVA: 0x0005D83D File Offset: 0x0005BA3D
			internal bool isSigned
			{
				get
				{
					return this.logicalMin < 0;
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001437 RID: 5175 RVA: 0x0005D848 File Offset: 0x0005BA48
			internal float minFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int num = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int num2 = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMin, num, num2) * 2f - 1f;
					}
					uint num3 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMin, 0U, num3);
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001438 RID: 5176 RVA: 0x0005D8BC File Offset: 0x0005BABC
			internal float maxFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int num = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int num2 = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMax, num, num2) * 2f - 1f;
					}
					uint num3 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMax, 0U, num3);
				}
			}

			// Token: 0x06001439 RID: 5177 RVA: 0x0005D92D File Offset: 0x0005BB2D
			public bool Is(HID.UsagePage usagePage, int usage)
			{
				return usagePage == this.usagePage && usage == this.usage;
			}

			// Token: 0x0600143A RID: 5178 RVA: 0x0005D944 File Offset: 0x0005BB44
			internal string DetermineName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage != HID.UsagePage.GenericDesktop)
				{
					if (usagePage != HID.UsagePage.Button)
					{
						return string.Format("UsagePage({0:X}) Usage({1:X})", this.usagePage, this.usage);
					}
					if (this.usage == 1)
					{
						return "trigger";
					}
					return string.Format("button{0}", this.usage);
				}
				else
				{
					if (this.usage == 57)
					{
						return "hat";
					}
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					string text = genericDesktop.ToString();
					return char.ToLowerInvariant(text[0]).ToString() + text.Substring(1);
				}
			}

			// Token: 0x0600143B RID: 5179 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
			internal string DetermineDisplayName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					return genericDesktop.ToString();
				}
				if (usagePage != HID.UsagePage.Button)
				{
					return null;
				}
				if (this.usage == 1)
				{
					return "Trigger";
				}
				return string.Format("Button {0}", this.usage);
			}

			// Token: 0x0600143C RID: 5180 RVA: 0x0005DA48 File Offset: 0x0005BC48
			internal bool IsUsableElement()
			{
				int num = this.usage;
				if (num - 48 <= 1)
				{
					return this.usagePage == HID.UsagePage.GenericDesktop;
				}
				return this.DetermineLayout() != null;
			}

			// Token: 0x0600143D RID: 5181 RVA: 0x0005DA78 File Offset: 0x0005BC78
			internal string DetermineLayout()
			{
				if (this.reportType != HID.HIDReportType.Input)
				{
					return null;
				}
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					switch (num)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						return "Axis";
					case 57:
						if (this.logicalMax - this.logicalMin + 1 == 8)
						{
							return "Dpad";
						}
						goto IL_00BC;
					case 58:
					case 59:
					case 60:
					case 63:
						goto IL_00BC;
					case 61:
					case 62:
						break;
					default:
						if (num - 144 > 3)
						{
							goto IL_00BC;
						}
						break;
					}
					return "Button";
				}
				if (usagePage == HID.UsagePage.Button)
				{
					return "Button";
				}
				IL_00BC:
				return null;
			}

			// Token: 0x0600143E RID: 5182 RVA: 0x0005DB44 File Offset: 0x0005BD44
			internal FourCC DetermineFormat()
			{
				int num = this.reportSizeInBits;
				if (num != 8)
				{
					if (num != 16)
					{
						if (num != 32)
						{
							return InputStateBlock.FormatBit;
						}
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUInt;
						}
						return InputStateBlock.FormatInt;
					}
					else
					{
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUShort;
						}
						return InputStateBlock.FormatShort;
					}
				}
				else
				{
					if (!this.isSigned)
					{
						return InputStateBlock.FormatByte;
					}
					return InputStateBlock.FormatSByte;
				}
			}

			// Token: 0x0600143F RID: 5183 RVA: 0x0005DBAC File Offset: 0x0005BDAC
			internal InternedString[] DetermineUsages()
			{
				if (this.usagePage == HID.UsagePage.Button && this.usage == 1)
				{
					return new InternedString[]
					{
						CommonUsages.PrimaryTrigger,
						CommonUsages.PrimaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.Button && this.usage == 2)
				{
					return new InternedString[]
					{
						CommonUsages.SecondaryTrigger,
						CommonUsages.SecondaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 53)
				{
					return new InternedString[] { CommonUsages.Twist };
				}
				return null;
			}

			// Token: 0x06001440 RID: 5184 RVA: 0x0005DC44 File Offset: 0x0005BE44
			internal string DetermineParameters()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 50:
					case 51:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 66:
					case 67:
					case 69:
						return this.DetermineAxisNormalizationParameters();
					case 49:
					case 52:
					case 65:
					case 68:
						return StringHelpers.Join<string>(",", new string[]
						{
							"invert",
							this.DetermineAxisNormalizationParameters()
						});
					}
				}
				return null;
			}

			// Token: 0x06001441 RID: 5185 RVA: 0x0005DCF4 File Offset: 0x0005BEF4
			private string DetermineAxisNormalizationParameters()
			{
				if (this.logicalMin == 0 && this.logicalMax == 0)
				{
					return "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5";
				}
				float minFloatValue = this.minFloatValue;
				float maxFloatValue = this.maxFloatValue;
				if (Mathf.Approximately(0f, minFloatValue) && Mathf.Approximately(0f, maxFloatValue))
				{
					return null;
				}
				float num = minFloatValue + (maxFloatValue - minFloatValue) / 2f;
				return string.Format(CultureInfo.InvariantCulture, "normalize,normalizeMin={0},normalizeMax={1},normalizeZero={2}", minFloatValue, maxFloatValue, num);
			}

			// Token: 0x06001442 RID: 5186 RVA: 0x0005DD70 File Offset: 0x0005BF70
			internal string DetermineProcessors()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					if (num - 48 <= 8 || num - 64 <= 5)
					{
						return "axisDeadzone";
					}
				}
				return null;
			}

			// Token: 0x06001443 RID: 5187 RVA: 0x0005DDA4 File Offset: 0x0005BFA4
			internal PrimitiveValue DetermineDefaultState()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						if (!this.isSigned)
						{
							int num = this.logicalMin + (this.logicalMax - this.logicalMin) / 2;
							if (num != 0)
							{
								return new PrimitiveValue(num);
							}
						}
						break;
					case 57:
						if (this.hasNullState)
						{
							if (this.logicalMin >= 1)
							{
								return new PrimitiveValue(this.logicalMin - 1);
							}
							ulong num2 = (1UL << this.reportSizeInBits) - 1UL;
							if ((long)this.logicalMax < (long)num2)
							{
								return new PrimitiveValue(this.logicalMax + 1);
							}
						}
						break;
					}
				}
				return default(PrimitiveValue);
			}

			// Token: 0x06001444 RID: 5188 RVA: 0x0005DEA0 File Offset: 0x0005C0A0
			internal void AddChildControls(ref HID.HIDElementDescriptor element, string controlName, ref InputControlLayout.Builder builder)
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 57)
				{
					PrimitiveValue primitiveValue = this.DetermineDefaultState();
					if (primitiveValue.isEmpty)
					{
						return;
					}
					builder.AddControl(controlName + "/up").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1},nullValue={2},wrapAtValue={3}", new object[]
						{
							this.logicalMax,
							this.logicalMin + 1,
							primitiveValue.ToString(),
							this.logicalMax
						}))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/right").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 1, this.logicalMin + 3))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/down").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 3, this.logicalMin + 5))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/left").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 5, this.logicalMin + 7))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
				}
			}

			// Token: 0x040009C2 RID: 2498
			public int usage;

			// Token: 0x040009C3 RID: 2499
			public HID.UsagePage usagePage;

			// Token: 0x040009C4 RID: 2500
			public int unit;

			// Token: 0x040009C5 RID: 2501
			public int unitExponent;

			// Token: 0x040009C6 RID: 2502
			public int logicalMin;

			// Token: 0x040009C7 RID: 2503
			public int logicalMax;

			// Token: 0x040009C8 RID: 2504
			public int physicalMin;

			// Token: 0x040009C9 RID: 2505
			public int physicalMax;

			// Token: 0x040009CA RID: 2506
			public HID.HIDReportType reportType;

			// Token: 0x040009CB RID: 2507
			public int collectionIndex;

			// Token: 0x040009CC RID: 2508
			public int reportId;

			// Token: 0x040009CD RID: 2509
			public int reportSizeInBits;

			// Token: 0x040009CE RID: 2510
			public int reportOffsetInBits;

			// Token: 0x040009CF RID: 2511
			public HID.HIDElementFlags flags;

			// Token: 0x040009D0 RID: 2512
			public int? usageMin;

			// Token: 0x040009D1 RID: 2513
			public int? usageMax;
		}

		// Token: 0x020001DE RID: 478
		[Serializable]
		public struct HIDCollectionDescriptor
		{
			// Token: 0x040009D2 RID: 2514
			public HID.HIDCollectionType type;

			// Token: 0x040009D3 RID: 2515
			public int usage;

			// Token: 0x040009D4 RID: 2516
			public HID.UsagePage usagePage;

			// Token: 0x040009D5 RID: 2517
			public int parent;

			// Token: 0x040009D6 RID: 2518
			public int childCount;

			// Token: 0x040009D7 RID: 2519
			public int firstChild;
		}

		// Token: 0x020001DF RID: 479
		[Serializable]
		public struct HIDDeviceDescriptor
		{
			// Token: 0x06001445 RID: 5189 RVA: 0x0005E0EF File Offset: 0x0005C2EF
			public string ToJson()
			{
				return JsonUtility.ToJson(this, true);
			}

			// Token: 0x06001446 RID: 5190 RVA: 0x0005E104 File Offset: 0x0005C304
			public static HID.HIDDeviceDescriptor FromJson(string json)
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor2;
				try
				{
					HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
					ReadOnlySpan<char> readOnlySpan = json.AsSpan();
					PredictiveParser predictiveParser = default(PredictiveParser);
					predictiveParser.ExpectSingleChar(readOnlySpan, '{');
					ReadOnlySpan<char> readOnlySpan2;
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.vendorId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.productId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.usage = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.usagePage = (HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.inputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.outputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan2);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					hiddeviceDescriptor.featureReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					ReadOnlySpan<char> readOnlySpan3;
					predictiveParser.AcceptString(readOnlySpan, out readOnlySpan3);
					if (readOnlySpan3.ToString() != "elements")
					{
						hiddeviceDescriptor2 = hiddeviceDescriptor;
					}
					else
					{
						predictiveParser.ExpectSingleChar(readOnlySpan, ':');
						predictiveParser.ExpectSingleChar(readOnlySpan, '[');
						List<HID.HIDElementDescriptor> list;
						using (CollectionPool<List<HID.HIDElementDescriptor>, HID.HIDElementDescriptor>.Get(out list))
						{
							while (!predictiveParser.AcceptSingleChar(readOnlySpan, ']'))
							{
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectSingleChar(readOnlySpan, '{');
								HID.HIDElementDescriptor hidelementDescriptor = default(HID.HIDElementDescriptor);
								predictiveParser.AcceptSingleChar(readOnlySpan, '}');
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.usage = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.usagePage = (HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.unit = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.unitExponent = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.logicalMin = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.logicalMax = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.physicalMin = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.physicalMax = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.collectionIndex = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportType = (HID.HIDReportType)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportId = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								predictiveParser.AcceptInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportSizeInBits = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.reportOffsetInBits = predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.AcceptSingleChar(readOnlySpan, ',');
								predictiveParser.ExpectString(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, ':');
								hidelementDescriptor.flags = (HID.HIDElementFlags)predictiveParser.ExpectInt(readOnlySpan);
								predictiveParser.ExpectSingleChar(readOnlySpan, '}');
								list.Add(hidelementDescriptor);
							}
							hiddeviceDescriptor.elements = list.ToArray();
							hiddeviceDescriptor2 = hiddeviceDescriptor;
						}
					}
				}
				catch (Exception)
				{
					Debug.LogWarning("Couldn't parse HID descriptor with fast parser. Using fallback");
					hiddeviceDescriptor2 = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(json);
				}
				return hiddeviceDescriptor2;
			}

			// Token: 0x040009D8 RID: 2520
			public int vendorId;

			// Token: 0x040009D9 RID: 2521
			public int productId;

			// Token: 0x040009DA RID: 2522
			public int usage;

			// Token: 0x040009DB RID: 2523
			public HID.UsagePage usagePage;

			// Token: 0x040009DC RID: 2524
			public int inputReportSize;

			// Token: 0x040009DD RID: 2525
			public int outputReportSize;

			// Token: 0x040009DE RID: 2526
			public int featureReportSize;

			// Token: 0x040009DF RID: 2527
			public HID.HIDElementDescriptor[] elements;

			// Token: 0x040009E0 RID: 2528
			public HID.HIDCollectionDescriptor[] collections;
		}

		// Token: 0x020001E0 RID: 480
		public struct HIDDeviceDescriptorBuilder
		{
			// Token: 0x06001447 RID: 5191 RVA: 0x0005E61C File Offset: 0x0005C81C
			public HIDDeviceDescriptorBuilder(HID.UsagePage usagePage, int usage)
			{
				this = default(HID.HIDDeviceDescriptorBuilder);
				this.usagePage = usagePage;
				this.usage = usage;
			}

			// Token: 0x06001448 RID: 5192 RVA: 0x0005E633 File Offset: 0x0005C833
			public HIDDeviceDescriptorBuilder(HID.GenericDesktop usage)
			{
				this = new HID.HIDDeviceDescriptorBuilder(HID.UsagePage.GenericDesktop, (int)usage);
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x0005E63D File Offset: 0x0005C83D
			public HID.HIDDeviceDescriptorBuilder StartReport(HID.HIDReportType reportType, int reportId = 1)
			{
				this.m_CurrentReportId = reportId;
				this.m_CurrentReportType = reportType;
				this.m_CurrentReportOffsetInBits = 8;
				return this;
			}

			// Token: 0x0600144A RID: 5194 RVA: 0x0005E65C File Offset: 0x0005C85C
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.UsagePage usagePage, int usage, int sizeInBits)
			{
				if (this.m_Elements == null)
				{
					this.m_Elements = new List<HID.HIDElementDescriptor>();
				}
				else
				{
					foreach (HID.HIDElementDescriptor hidelementDescriptor in this.m_Elements)
					{
						if (hidelementDescriptor.reportId == this.m_CurrentReportId && hidelementDescriptor.reportType == this.m_CurrentReportType && hidelementDescriptor.usagePage == usagePage && hidelementDescriptor.usage == usage)
						{
							throw new InvalidOperationException(string.Format("Cannot add two elements with the same usage page '{0}' and usage '0x{1:X} the to same device", usagePage, usage));
						}
					}
				}
				this.m_Elements.Add(new HID.HIDElementDescriptor
				{
					usage = usage,
					usagePage = usagePage,
					reportOffsetInBits = this.m_CurrentReportOffsetInBits,
					reportSizeInBits = sizeInBits,
					reportType = this.m_CurrentReportType,
					reportId = this.m_CurrentReportId
				});
				this.m_CurrentReportOffsetInBits += sizeInBits;
				return this;
			}

			// Token: 0x0600144B RID: 5195 RVA: 0x0005E770 File Offset: 0x0005C970
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.GenericDesktop usage, int sizeInBits)
			{
				return this.AddElement(HID.UsagePage.GenericDesktop, (int)usage, sizeInBits);
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x0005E77C File Offset: 0x0005C97C
			public HID.HIDDeviceDescriptorBuilder WithPhysicalMinMax(int min, int max)
			{
				int num = this.m_Elements.Count - 1;
				if (num < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor hidelementDescriptor = this.m_Elements[num];
				hidelementDescriptor.physicalMin = min;
				hidelementDescriptor.physicalMax = max;
				this.m_Elements[num] = hidelementDescriptor;
				return this;
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x0005E7D8 File Offset: 0x0005C9D8
			public HID.HIDDeviceDescriptorBuilder WithLogicalMinMax(int min, int max)
			{
				int num = this.m_Elements.Count - 1;
				if (num < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor hidelementDescriptor = this.m_Elements[num];
				hidelementDescriptor.logicalMin = min;
				hidelementDescriptor.logicalMax = max;
				this.m_Elements[num] = hidelementDescriptor;
				return this;
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x0005E834 File Offset: 0x0005CA34
			public HID.HIDDeviceDescriptor Finish()
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
				hiddeviceDescriptor.usage = this.usage;
				hiddeviceDescriptor.usagePage = this.usagePage;
				List<HID.HIDElementDescriptor> elements = this.m_Elements;
				hiddeviceDescriptor.elements = ((elements != null) ? elements.ToArray() : null);
				List<HID.HIDCollectionDescriptor> collections = this.m_Collections;
				hiddeviceDescriptor.collections = ((collections != null) ? collections.ToArray() : null);
				return hiddeviceDescriptor;
			}

			// Token: 0x040009E1 RID: 2529
			public HID.UsagePage usagePage;

			// Token: 0x040009E2 RID: 2530
			public int usage;

			// Token: 0x040009E3 RID: 2531
			private int m_CurrentReportId;

			// Token: 0x040009E4 RID: 2532
			private HID.HIDReportType m_CurrentReportType;

			// Token: 0x040009E5 RID: 2533
			private int m_CurrentReportOffsetInBits;

			// Token: 0x040009E6 RID: 2534
			private List<HID.HIDElementDescriptor> m_Elements;

			// Token: 0x040009E7 RID: 2535
			private List<HID.HIDCollectionDescriptor> m_Collections;

			// Token: 0x040009E8 RID: 2536
			private int m_InputReportSize;

			// Token: 0x040009E9 RID: 2537
			private int m_OutputReportSize;

			// Token: 0x040009EA RID: 2538
			private int m_FeatureReportSize;
		}

		// Token: 0x020001E1 RID: 481
		public enum UsagePage
		{
			// Token: 0x040009EC RID: 2540
			Undefined,
			// Token: 0x040009ED RID: 2541
			GenericDesktop,
			// Token: 0x040009EE RID: 2542
			Simulation,
			// Token: 0x040009EF RID: 2543
			VRControls,
			// Token: 0x040009F0 RID: 2544
			SportControls,
			// Token: 0x040009F1 RID: 2545
			GameControls,
			// Token: 0x040009F2 RID: 2546
			GenericDeviceControls,
			// Token: 0x040009F3 RID: 2547
			Keyboard,
			// Token: 0x040009F4 RID: 2548
			LEDs,
			// Token: 0x040009F5 RID: 2549
			Button,
			// Token: 0x040009F6 RID: 2550
			Ordinal,
			// Token: 0x040009F7 RID: 2551
			Telephony,
			// Token: 0x040009F8 RID: 2552
			Consumer,
			// Token: 0x040009F9 RID: 2553
			Digitizer,
			// Token: 0x040009FA RID: 2554
			PID = 15,
			// Token: 0x040009FB RID: 2555
			Unicode,
			// Token: 0x040009FC RID: 2556
			AlphanumericDisplay = 20,
			// Token: 0x040009FD RID: 2557
			MedicalInstruments = 64,
			// Token: 0x040009FE RID: 2558
			Monitor = 128,
			// Token: 0x040009FF RID: 2559
			Power = 132,
			// Token: 0x04000A00 RID: 2560
			BarCodeScanner = 140,
			// Token: 0x04000A01 RID: 2561
			MagneticStripeReader = 142,
			// Token: 0x04000A02 RID: 2562
			Camera = 144,
			// Token: 0x04000A03 RID: 2563
			Arcade,
			// Token: 0x04000A04 RID: 2564
			VendorDefined = 65280
		}

		// Token: 0x020001E2 RID: 482
		public enum GenericDesktop
		{
			// Token: 0x04000A06 RID: 2566
			Undefined,
			// Token: 0x04000A07 RID: 2567
			Pointer,
			// Token: 0x04000A08 RID: 2568
			Mouse,
			// Token: 0x04000A09 RID: 2569
			Joystick = 4,
			// Token: 0x04000A0A RID: 2570
			Gamepad,
			// Token: 0x04000A0B RID: 2571
			Keyboard,
			// Token: 0x04000A0C RID: 2572
			Keypad,
			// Token: 0x04000A0D RID: 2573
			MultiAxisController,
			// Token: 0x04000A0E RID: 2574
			TabletPCControls,
			// Token: 0x04000A0F RID: 2575
			AssistiveControl,
			// Token: 0x04000A10 RID: 2576
			X = 48,
			// Token: 0x04000A11 RID: 2577
			Y,
			// Token: 0x04000A12 RID: 2578
			Z,
			// Token: 0x04000A13 RID: 2579
			Rx,
			// Token: 0x04000A14 RID: 2580
			Ry,
			// Token: 0x04000A15 RID: 2581
			Rz,
			// Token: 0x04000A16 RID: 2582
			Slider,
			// Token: 0x04000A17 RID: 2583
			Dial,
			// Token: 0x04000A18 RID: 2584
			Wheel,
			// Token: 0x04000A19 RID: 2585
			HatSwitch,
			// Token: 0x04000A1A RID: 2586
			CountedBuffer,
			// Token: 0x04000A1B RID: 2587
			ByteCount,
			// Token: 0x04000A1C RID: 2588
			MotionWakeup,
			// Token: 0x04000A1D RID: 2589
			Start,
			// Token: 0x04000A1E RID: 2590
			Select,
			// Token: 0x04000A1F RID: 2591
			Vx = 64,
			// Token: 0x04000A20 RID: 2592
			Vy,
			// Token: 0x04000A21 RID: 2593
			Vz,
			// Token: 0x04000A22 RID: 2594
			Vbrx,
			// Token: 0x04000A23 RID: 2595
			Vbry,
			// Token: 0x04000A24 RID: 2596
			Vbrz,
			// Token: 0x04000A25 RID: 2597
			Vno,
			// Token: 0x04000A26 RID: 2598
			FeatureNotification,
			// Token: 0x04000A27 RID: 2599
			ResolutionMultiplier,
			// Token: 0x04000A28 RID: 2600
			SystemControl = 128,
			// Token: 0x04000A29 RID: 2601
			SystemPowerDown,
			// Token: 0x04000A2A RID: 2602
			SystemSleep,
			// Token: 0x04000A2B RID: 2603
			SystemWakeUp,
			// Token: 0x04000A2C RID: 2604
			SystemContextMenu,
			// Token: 0x04000A2D RID: 2605
			SystemMainMenu,
			// Token: 0x04000A2E RID: 2606
			SystemAppMenu,
			// Token: 0x04000A2F RID: 2607
			SystemMenuHelp,
			// Token: 0x04000A30 RID: 2608
			SystemMenuExit,
			// Token: 0x04000A31 RID: 2609
			SystemMenuSelect,
			// Token: 0x04000A32 RID: 2610
			SystemMenuRight,
			// Token: 0x04000A33 RID: 2611
			SystemMenuLeft,
			// Token: 0x04000A34 RID: 2612
			SystemMenuUp,
			// Token: 0x04000A35 RID: 2613
			SystemMenuDown,
			// Token: 0x04000A36 RID: 2614
			SystemColdRestart,
			// Token: 0x04000A37 RID: 2615
			SystemWarmRestart,
			// Token: 0x04000A38 RID: 2616
			DpadUp,
			// Token: 0x04000A39 RID: 2617
			DpadDown,
			// Token: 0x04000A3A RID: 2618
			DpadRight,
			// Token: 0x04000A3B RID: 2619
			DpadLeft,
			// Token: 0x04000A3C RID: 2620
			SystemDock = 160,
			// Token: 0x04000A3D RID: 2621
			SystemUndock,
			// Token: 0x04000A3E RID: 2622
			SystemSetup,
			// Token: 0x04000A3F RID: 2623
			SystemBreak,
			// Token: 0x04000A40 RID: 2624
			SystemDebuggerBreak,
			// Token: 0x04000A41 RID: 2625
			ApplicationBreak,
			// Token: 0x04000A42 RID: 2626
			ApplicationDebuggerBreak,
			// Token: 0x04000A43 RID: 2627
			SystemSpeakerMute,
			// Token: 0x04000A44 RID: 2628
			SystemHibernate,
			// Token: 0x04000A45 RID: 2629
			SystemDisplayInvert = 176,
			// Token: 0x04000A46 RID: 2630
			SystemDisplayInternal,
			// Token: 0x04000A47 RID: 2631
			SystemDisplayExternal,
			// Token: 0x04000A48 RID: 2632
			SystemDisplayBoth,
			// Token: 0x04000A49 RID: 2633
			SystemDisplayDual,
			// Token: 0x04000A4A RID: 2634
			SystemDisplayToggleIntExt,
			// Token: 0x04000A4B RID: 2635
			SystemDisplaySwapPrimarySecondary,
			// Token: 0x04000A4C RID: 2636
			SystemDisplayLCDAutoScale
		}

		// Token: 0x020001E3 RID: 483
		public enum Simulation
		{
			// Token: 0x04000A4E RID: 2638
			Undefined,
			// Token: 0x04000A4F RID: 2639
			FlightSimulationDevice,
			// Token: 0x04000A50 RID: 2640
			AutomobileSimulationDevice,
			// Token: 0x04000A51 RID: 2641
			TankSimulationDevice,
			// Token: 0x04000A52 RID: 2642
			SpaceshipSimulationDevice,
			// Token: 0x04000A53 RID: 2643
			SubmarineSimulationDevice,
			// Token: 0x04000A54 RID: 2644
			SailingSimulationDevice,
			// Token: 0x04000A55 RID: 2645
			MotorcycleSimulationDevice,
			// Token: 0x04000A56 RID: 2646
			SportsSimulationDevice,
			// Token: 0x04000A57 RID: 2647
			AirplaneSimulationDevice,
			// Token: 0x04000A58 RID: 2648
			HelicopterSimulationDevice,
			// Token: 0x04000A59 RID: 2649
			MagicCarpetSimulationDevice,
			// Token: 0x04000A5A RID: 2650
			BicylcleSimulationDevice,
			// Token: 0x04000A5B RID: 2651
			FlightControlStick = 32,
			// Token: 0x04000A5C RID: 2652
			FlightStick,
			// Token: 0x04000A5D RID: 2653
			CyclicControl,
			// Token: 0x04000A5E RID: 2654
			CyclicTrim,
			// Token: 0x04000A5F RID: 2655
			FlightYoke,
			// Token: 0x04000A60 RID: 2656
			TrackControl,
			// Token: 0x04000A61 RID: 2657
			Aileron = 176,
			// Token: 0x04000A62 RID: 2658
			AileronTrim,
			// Token: 0x04000A63 RID: 2659
			AntiTorqueControl,
			// Token: 0x04000A64 RID: 2660
			AutopilotEnable,
			// Token: 0x04000A65 RID: 2661
			ChaffRelease,
			// Token: 0x04000A66 RID: 2662
			CollectiveControl,
			// Token: 0x04000A67 RID: 2663
			DiveBreak,
			// Token: 0x04000A68 RID: 2664
			ElectronicCountermeasures,
			// Token: 0x04000A69 RID: 2665
			Elevator,
			// Token: 0x04000A6A RID: 2666
			ElevatorTrim,
			// Token: 0x04000A6B RID: 2667
			Rudder,
			// Token: 0x04000A6C RID: 2668
			Throttle,
			// Token: 0x04000A6D RID: 2669
			FlightCommunications,
			// Token: 0x04000A6E RID: 2670
			FlareRelease,
			// Token: 0x04000A6F RID: 2671
			LandingGear,
			// Token: 0x04000A70 RID: 2672
			ToeBreak,
			// Token: 0x04000A71 RID: 2673
			Trigger,
			// Token: 0x04000A72 RID: 2674
			WeaponsArm,
			// Token: 0x04000A73 RID: 2675
			WeaponsSelect,
			// Token: 0x04000A74 RID: 2676
			WingFlaps,
			// Token: 0x04000A75 RID: 2677
			Accelerator,
			// Token: 0x04000A76 RID: 2678
			Brake,
			// Token: 0x04000A77 RID: 2679
			Clutch,
			// Token: 0x04000A78 RID: 2680
			Shifter,
			// Token: 0x04000A79 RID: 2681
			Steering,
			// Token: 0x04000A7A RID: 2682
			TurretDirection,
			// Token: 0x04000A7B RID: 2683
			BarrelElevation,
			// Token: 0x04000A7C RID: 2684
			DivePlane,
			// Token: 0x04000A7D RID: 2685
			Ballast,
			// Token: 0x04000A7E RID: 2686
			BicycleCrank,
			// Token: 0x04000A7F RID: 2687
			HandleBars,
			// Token: 0x04000A80 RID: 2688
			FrontBrake,
			// Token: 0x04000A81 RID: 2689
			RearBrake
		}

		// Token: 0x020001E4 RID: 484
		public enum Button
		{
			// Token: 0x04000A83 RID: 2691
			Undefined,
			// Token: 0x04000A84 RID: 2692
			Primary,
			// Token: 0x04000A85 RID: 2693
			Secondary,
			// Token: 0x04000A86 RID: 2694
			Tertiary
		}
	}
}
