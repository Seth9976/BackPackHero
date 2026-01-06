using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000041 RID: 65
	internal class FastMouse : Mouse, IInputStateCallbackReceiver, IEventMerger
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0001B014 File Offset: 0x00019214
		public FastMouse()
		{
			InputControlExtensions.DeviceBuilder deviceBuilder = this.Setup(30, 10, 2).WithName("Mouse").WithDisplayName("Mouse")
				.WithChildren(0, 14)
				.WithLayout(new InternedString("Mouse"))
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1297044819),
					sizeInBits = 392U
				});
			InternedString internedString = new InternedString("Vector2");
			InternedString internedString2 = new InternedString("Delta");
			InternedString internedString3 = new InternedString("Button");
			InternedString internedString4 = new InternedString("Axis");
			InternedString internedString5 = new InternedString("Digital");
			InternedString internedString6 = new InternedString("Integer");
			Vector2Control vector2Control = this.Initialize_ctrlMouseposition(internedString, this);
			DeltaControl deltaControl = this.Initialize_ctrlMousedelta(internedString2, this);
			DeltaControl deltaControl2 = this.Initialize_ctrlMousescroll(internedString2, this);
			ButtonControl buttonControl = this.Initialize_ctrlMousepress(internedString3, this);
			ButtonControl buttonControl2 = this.Initialize_ctrlMouseleftButton(internedString3, this);
			ButtonControl buttonControl3 = this.Initialize_ctrlMouserightButton(internedString3, this);
			ButtonControl buttonControl4 = this.Initialize_ctrlMousemiddleButton(internedString3, this);
			ButtonControl buttonControl5 = this.Initialize_ctrlMouseforwardButton(internedString3, this);
			ButtonControl buttonControl6 = this.Initialize_ctrlMousebackButton(internedString3, this);
			AxisControl axisControl = this.Initialize_ctrlMousepressure(internedString4, this);
			Vector2Control vector2Control2 = this.Initialize_ctrlMouseradius(internedString, this);
			this.Initialize_ctrlMousepointerId(internedString5, this);
			IntegerControl integerControl = this.Initialize_ctrlMousedisplayIndex(internedString6, this);
			IntegerControl integerControl2 = this.Initialize_ctrlMouseclickCount(internedString6, this);
			AxisControl axisControl2 = this.Initialize_ctrlMousepositionx(internedString4, vector2Control);
			AxisControl axisControl3 = this.Initialize_ctrlMousepositiony(internedString4, vector2Control);
			AxisControl axisControl4 = this.Initialize_ctrlMousedeltaup(internedString4, deltaControl);
			AxisControl axisControl5 = this.Initialize_ctrlMousedeltadown(internedString4, deltaControl);
			AxisControl axisControl6 = this.Initialize_ctrlMousedeltaleft(internedString4, deltaControl);
			AxisControl axisControl7 = this.Initialize_ctrlMousedeltaright(internedString4, deltaControl);
			AxisControl axisControl8 = this.Initialize_ctrlMousedeltax(internedString4, deltaControl);
			AxisControl axisControl9 = this.Initialize_ctrlMousedeltay(internedString4, deltaControl);
			AxisControl axisControl10 = this.Initialize_ctrlMousescrollup(internedString4, deltaControl2);
			AxisControl axisControl11 = this.Initialize_ctrlMousescrolldown(internedString4, deltaControl2);
			AxisControl axisControl12 = this.Initialize_ctrlMousescrollleft(internedString4, deltaControl2);
			AxisControl axisControl13 = this.Initialize_ctrlMousescrollright(internedString4, deltaControl2);
			AxisControl axisControl14 = this.Initialize_ctrlMousescrollx(internedString4, deltaControl2);
			AxisControl axisControl15 = this.Initialize_ctrlMousescrolly(internedString4, deltaControl2);
			AxisControl axisControl16 = this.Initialize_ctrlMouseradiusx(internedString4, vector2Control2);
			AxisControl axisControl17 = this.Initialize_ctrlMouseradiusy(internedString4, vector2Control2);
			deviceBuilder.WithControlUsage(0, new InternedString("Point"), vector2Control);
			deviceBuilder.WithControlUsage(1, new InternedString("Secondary2DMotion"), deltaControl);
			deviceBuilder.WithControlUsage(2, new InternedString("ScrollHorizontal"), axisControl14);
			deviceBuilder.WithControlUsage(3, new InternedString("ScrollVertical"), axisControl15);
			deviceBuilder.WithControlUsage(4, new InternedString("PrimaryAction"), buttonControl2);
			deviceBuilder.WithControlUsage(5, new InternedString("SecondaryAction"), buttonControl3);
			deviceBuilder.WithControlUsage(6, new InternedString("Forward"), buttonControl5);
			deviceBuilder.WithControlUsage(7, new InternedString("Back"), buttonControl6);
			deviceBuilder.WithControlUsage(8, new InternedString("Pressure"), axisControl);
			deviceBuilder.WithControlUsage(9, new InternedString("Radius"), vector2Control2);
			deviceBuilder.WithControlAlias(0, new InternedString("horizontal"));
			deviceBuilder.WithControlAlias(1, new InternedString("vertical"));
			base.scroll = deltaControl2;
			base.leftButton = buttonControl2;
			base.middleButton = buttonControl4;
			base.rightButton = buttonControl3;
			base.backButton = buttonControl6;
			base.forwardButton = buttonControl5;
			base.clickCount = integerControl2;
			base.position = vector2Control;
			base.delta = deltaControl;
			base.radius = vector2Control2;
			base.pressure = axisControl;
			base.press = buttonControl;
			base.displayIndex = integerControl;
			vector2Control.x = axisControl2;
			vector2Control.y = axisControl3;
			deltaControl.up = axisControl4;
			deltaControl.down = axisControl5;
			deltaControl.left = axisControl6;
			deltaControl.right = axisControl7;
			deltaControl.x = axisControl8;
			deltaControl.y = axisControl9;
			deltaControl2.up = axisControl10;
			deltaControl2.down = axisControl11;
			deltaControl2.left = axisControl12;
			deltaControl2.right = axisControl13;
			deltaControl2.x = axisControl14;
			deltaControl2.y = axisControl15;
			vector2Control2.x = axisControl16;
			vector2Control2.y = axisControl17;
			deviceBuilder.WithStateOffsetToControlIndexMap(new uint[]
			{
				32782U, 16809999U, 33587218U, 33587219U, 33587220U, 50364432U, 50364433U, 50364437U, 67141656U, 67141657U,
				67141658U, 83918870U, 83918871U, 83918875U, 100664323U, 100664324U, 101188613U, 101712902U, 102237191U, 102761480U,
				109068300U, 117456909U, 134250505U, 167804956U, 184582173U, 201327627U
			});
			deviceBuilder.WithControlTree(new byte[]
			{
				135, 1, 1, 0, 0, 0, 0, 196, 0, 3,
				0, 0, 0, 0, 135, 1, 23, 0, 0, 0,
				0, 128, 0, 5, 0, 0, 0, 0, 196, 0,
				11, 0, 0, 0, 0, 64, 0, 7, 0, 0,
				0, 1, 128, 0, 9, 0, 3, 0, 1, 32,
				0, byte.MaxValue, byte.MaxValue, 1, 0, 1, 64, 0, byte.MaxValue, byte.MaxValue,
				2, 0, 1, 96, 0, byte.MaxValue, byte.MaxValue, 7, 0, 3,
				128, 0, byte.MaxValue, byte.MaxValue, 4, 0, 3, 193, 0, 13,
				0, 0, 0, 0, 196, 0, 19, 0, 0, 0,
				0, 161, 0, 15, 0, 10, 0, 4, 193, 0,
				17, 0, 14, 0, 4, 145, 0, byte.MaxValue, byte.MaxValue, 18,
				0, 3, 161, 0, byte.MaxValue, byte.MaxValue, 21, 0, 3, 192,
				0, byte.MaxValue, byte.MaxValue, 0, 0, 0, 193, 0, byte.MaxValue, byte.MaxValue,
				24, 0, 2, 195, 0, 21, 0, 0, 0, 0,
				196, 0, byte.MaxValue, byte.MaxValue, 28, 0, 1, 194, 0, byte.MaxValue,
				byte.MaxValue, 26, 0, 1, 195, 0, byte.MaxValue, byte.MaxValue, 27, 0,
				1, 32, 1, 25, 0, 0, 0, 0, 135, 1,
				41, 0, 0, 0, 0, 240, 0, 27, 0, 0,
				0, 0, 32, 1, 39, 0, 0, 0, 0, 224,
				0, 29, 0, 0, 0, 0, 240, 0, byte.MaxValue, byte.MaxValue,
				41, 0, 1, 210, 0, 31, 0, 39, 0, 1,
				224, 0, byte.MaxValue, byte.MaxValue, 40, 0, 1, 203, 0, 33,
				0, 0, 0, 0, 210, 0, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 200, 0, 35, 0, 0, 0, 0, 203, 0,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 198, 0, 37, 0, 0,
				0, 0, 200, 0, byte.MaxValue, byte.MaxValue, 0, 0, 0, 197,
				0, byte.MaxValue, byte.MaxValue, 29, 0, 1, 198, 0, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 8, 1, byte.MaxValue, byte.MaxValue, 30, 0, 1,
				32, 1, byte.MaxValue, byte.MaxValue, 31, 0, 1, 128, 1, 43,
				0, 0, 0, 0, 135, 1, 47, 0, 0, 0,
				0, 80, 1, byte.MaxValue, byte.MaxValue, 32, 0, 2, 128, 1,
				45, 0, 34, 0, 2, 104, 1, byte.MaxValue, byte.MaxValue, 36,
				0, 1, 128, 1, byte.MaxValue, byte.MaxValue, 37, 0, 1, 132,
				1, 49, 0, 0, 0, 0, 135, 1, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 130, 1, 51, 0, 0, 0, 0,
				132, 1, byte.MaxValue, byte.MaxValue, 0, 0, 0, 129, 1, byte.MaxValue,
				byte.MaxValue, 38, 0, 1, 130, 1, byte.MaxValue, byte.MaxValue, 0, 0,
				0
			}, new ushort[]
			{
				0, 14, 15, 1, 16, 17, 21, 18, 19, 20,
				2, 22, 23, 27, 2, 22, 23, 27, 24, 25,
				26, 24, 25, 26, 3, 4, 5, 6, 7, 8,
				9, 9, 10, 28, 10, 28, 29, 29, 11, 12,
				12, 13
			});
			deviceBuilder.Finish();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B478 File Offset: 0x00019678
		private Vector2Control Initialize_ctrlMouseposition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 0).WithParent(parent)
				.WithChildren(14, 2)
				.WithName("position")
				.WithDisplayName("Position")
				.WithLayout(kVector2Layout)
				.WithUsages(0, 1)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B528 File Offset: 0x00019728
		private DeltaControl Initialize_ctrlMousedelta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 1).WithParent(parent)
				.WithChildren(16, 6)
				.WithName("delta")
				.WithDisplayName("Delta")
				.WithLayout(kDeltaLayout)
				.WithUsages(1, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B5D0 File Offset: 0x000197D0
		private DeltaControl Initialize_ctrlMousescroll(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 2).WithParent(parent)
				.WithChildren(22, 6)
				.WithName("scroll")
				.WithDisplayName("Scroll")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B670 File Offset: 0x00019870
		private ButtonControl Initialize_ctrlMousepress(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 3).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Press")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B728 File Offset: 0x00019928
		private ButtonControl Initialize_ctrlMouseleftButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 4).WithParent(parent)
				.WithName("leftButton")
				.WithDisplayName("Left Button")
				.WithShortDisplayName("LMB")
				.WithLayout(kButtonLayout)
				.WithUsages(4, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B7F0 File Offset: 0x000199F0
		private ButtonControl Initialize_ctrlMouserightButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 5).WithParent(parent)
				.WithName("rightButton")
				.WithDisplayName("Right Button")
				.WithShortDisplayName("RMB")
				.WithLayout(kButtonLayout)
				.WithUsages(5, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 1U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		private ButtonControl Initialize_ctrlMousemiddleButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 6).WithParent(parent)
				.WithName("middleButton")
				.WithDisplayName("Middle Button")
				.WithShortDisplayName("MMB")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 2U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B974 File Offset: 0x00019B74
		private ButtonControl Initialize_ctrlMouseforwardButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 7).WithParent(parent)
				.WithName("forwardButton")
				.WithDisplayName("Forward")
				.WithLayout(kButtonLayout)
				.WithUsages(6, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 3U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001BA2C File Offset: 0x00019C2C
		private ButtonControl Initialize_ctrlMousebackButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 8).WithParent(parent)
				.WithName("backButton")
				.WithDisplayName("Back")
				.WithLayout(kButtonLayout)
				.WithUsages(7, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		private AxisControl Initialize_ctrlMousepressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 9).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Pressure")
				.WithLayout(kAxisLayout)
				.WithUsages(8, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 32U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.WithDefaultState(1)
				.Finish();
			return axisControl;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001BB90 File Offset: 0x00019D90
		private Vector2Control Initialize_ctrlMouseradius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 10).WithParent(parent)
				.WithChildren(28, 2)
				.WithName("radius")
				.WithDisplayName("Radius")
				.WithLayout(kVector2Layout)
				.WithUsages(9, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 40U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001BC3C File Offset: 0x00019E3C
		private IntegerControl Initialize_ctrlMousepointerId(InternedString kDigitalLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 11).WithParent(parent)
				.WithName("pointerId")
				.WithDisplayName("pointerId")
				.WithLayout(kDigitalLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 48U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		private IntegerControl Initialize_ctrlMousedisplayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 12).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1431521364),
					byteOffset = 26U,
					bitOffset = 0U,
					sizeInBits = 16U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001BD64 File Offset: 0x00019F64
		private IntegerControl Initialize_ctrlMouseclickCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 13).WithParent(parent)
				.WithName("clickCount")
				.WithDisplayName("Click Count")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1431521364),
					byteOffset = 28U,
					bitOffset = 0U,
					sizeInBits = 16U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001BE00 File Offset: 0x0001A000
		private AxisControl Initialize_ctrlMousepositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 14).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Position X")
				.WithShortDisplayName("Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		private AxisControl Initialize_ctrlMousepositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 15).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Position Y")
				.WithShortDisplayName("Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 4U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001BF50 File Offset: 0x0001A150
		private AxisControl Initialize_ctrlMousedeltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 16).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Delta Up")
				.WithShortDisplayName("Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001C00C File Offset: 0x0001A20C
		private AxisControl Initialize_ctrlMousedeltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 17).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Delta Down")
				.WithShortDisplayName("Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		private AxisControl Initialize_ctrlMousedeltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 18).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Delta Left")
				.WithShortDisplayName("Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001C194 File Offset: 0x0001A394
		private AxisControl Initialize_ctrlMousedeltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 19).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Delta Right")
				.WithShortDisplayName("Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001C250 File Offset: 0x0001A450
		private AxisControl Initialize_ctrlMousedeltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 20).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Delta X")
				.WithShortDisplayName("Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		private AxisControl Initialize_ctrlMousedeltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 21).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Delta Y")
				.WithShortDisplayName("Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001C390 File Offset: 0x0001A590
		private AxisControl Initialize_ctrlMousescrollup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 22).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Scroll Up")
				.WithShortDisplayName("Scroll Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001C44C File Offset: 0x0001A64C
		private AxisControl Initialize_ctrlMousescrolldown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 23).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Scroll Down")
				.WithShortDisplayName("Scroll Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001C510 File Offset: 0x0001A710
		private AxisControl Initialize_ctrlMousescrollleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 24).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Scroll Left")
				.WithShortDisplayName("Scroll Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		private AxisControl Initialize_ctrlMousescrollright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 25).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Scroll Right")
				.WithShortDisplayName("Scroll Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001C690 File Offset: 0x0001A890
		private AxisControl Initialize_ctrlMousescrollx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 26).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Scroll Left/Right")
				.WithShortDisplayName("Scroll Left/Right")
				.WithLayout(kAxisLayout)
				.WithUsages(2, 1)
				.WithAliases(0, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001C744 File Offset: 0x0001A944
		private AxisControl Initialize_ctrlMousescrolly(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 27).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Scroll Up/Down")
				.WithShortDisplayName("Scroll Wheel")
				.WithLayout(kAxisLayout)
				.WithUsages(3, 1)
				.WithAliases(1, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private AxisControl Initialize_ctrlMouseradiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 28).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Radius X")
				.WithShortDisplayName("Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 40U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001C898 File Offset: 0x0001AA98
		private AxisControl Initialize_ctrlMouseradiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 29).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Radius Y")
				.WithShortDisplayName("Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 44U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001C938 File Offset: 0x0001AB38
		protected new void OnNextUpdate()
		{
			InputState.Change<Vector2>(base.delta, Vector2.zero, InputState.currentUpdateType, default(InputEventPtr));
			InputState.Change<Vector2>(base.scroll, Vector2.zero, InputState.currentUpdateType, default(InputEventPtr));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001C984 File Offset: 0x0001AB84
		protected new unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != MouseState.Format)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			MouseState mouseState = *(MouseState*)ptr->state;
			MouseState* ptr2 = (MouseState*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
			mouseState.delta += ptr2->delta;
			mouseState.scroll += ptr2->scroll;
			InputState.Change<MouseState>(this, ref mouseState, InputState.currentUpdateType, eventPtr);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001CA3A File Offset: 0x0001AC3A
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001CA42 File Offset: 0x0001AC42
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		internal unsafe static bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* ptr2 = StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != MouseState.Format || ptr2->stateFormat != MouseState.Format)
			{
				return false;
			}
			MouseState* state = (MouseState*)ptr->state;
			MouseState* state2 = (MouseState*)ptr2->state;
			if (state->buttons != state2->buttons || state->clickCount != state2->clickCount)
			{
				return false;
			}
			state2->delta += state->delta;
			state2->scroll += state->scroll;
			return true;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001CB24 File Offset: 0x0001AD24
		bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			return FastMouse.MergeForward(currentEventPtr, nextEventPtr);
		}

		// Token: 0x04000225 RID: 549
		public const string metadata = "AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer";
	}
}
