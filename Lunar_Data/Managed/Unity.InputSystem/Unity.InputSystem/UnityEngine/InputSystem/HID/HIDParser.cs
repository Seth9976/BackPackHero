using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000094 RID: 148
	internal static class HIDParser
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0003E6AC File Offset: 0x0003C8AC
		public unsafe static bool ParseReportDescriptor(byte[] buffer, ref HID.HIDDeviceDescriptor deviceDescriptor)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return HIDParser.ParseReportDescriptor(ptr, buffer.Length, ref deviceDescriptor);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003E6EC File Offset: 0x0003C8EC
		public unsafe static bool ParseReportDescriptor(byte* bufferPtr, int bufferLength, ref HID.HIDDeviceDescriptor deviceDescriptor)
		{
			HIDParser.HIDItemStateLocal hiditemStateLocal = default(HIDParser.HIDItemStateLocal);
			HIDParser.HIDItemStateGlobal hiditemStateGlobal = default(HIDParser.HIDItemStateGlobal);
			List<HIDParser.HIDReportData> list = new List<HIDParser.HIDReportData>();
			List<HID.HIDElementDescriptor> list2 = new List<HID.HIDElementDescriptor>();
			List<HID.HIDCollectionDescriptor> list3 = new List<HID.HIDCollectionDescriptor>();
			int num = -1;
			byte* ptr = bufferPtr + bufferLength;
			byte* ptr2 = bufferPtr;
			while (ptr2 < ptr)
			{
				byte b = *ptr2;
				if (b == 254)
				{
					throw new NotImplementedException("long item support");
				}
				byte b2 = b & 3;
				byte b3 = b & 252;
				ptr2++;
				if (b3 <= 84)
				{
					if (b3 <= 24)
					{
						if (b3 <= 8)
						{
							if (b3 != 4)
							{
								if (b3 == 8)
								{
									hiditemStateLocal.SetUsage(HIDParser.ReadData((int)b2, ptr2, ptr));
								}
							}
							else
							{
								hiditemStateGlobal.usagePage = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							}
						}
						else if (b3 != 20)
						{
							if (b3 == 24)
							{
								hiditemStateLocal.usageMinimum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							}
						}
						else
						{
							hiditemStateGlobal.logicalMinimum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
						}
					}
					else if (b3 <= 40)
					{
						if (b3 != 36)
						{
							if (b3 == 40)
							{
								hiditemStateLocal.usageMaximum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							}
						}
						else
						{
							hiditemStateGlobal.logicalMaximum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
						}
					}
					else if (b3 != 52)
					{
						if (b3 != 68)
						{
							if (b3 == 84)
							{
								hiditemStateGlobal.unitExponent = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							}
						}
						else
						{
							hiditemStateGlobal.physicalMaximum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
						}
					}
					else
					{
						hiditemStateGlobal.physicalMinimum = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
					}
				}
				else
				{
					if (b3 <= 132)
					{
						if (b3 <= 116)
						{
							if (b3 == 100)
							{
								hiditemStateGlobal.unit = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
								goto IL_0521;
							}
							if (b3 != 116)
							{
								goto IL_0521;
							}
							hiditemStateGlobal.reportSize = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							goto IL_0521;
						}
						else if (b3 != 128)
						{
							if (b3 != 132)
							{
								goto IL_0521;
							}
							hiditemStateGlobal.reportId = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							goto IL_0521;
						}
					}
					else if (b3 <= 148)
					{
						if (b3 != 144)
						{
							if (b3 != 148)
							{
								goto IL_0521;
							}
							hiditemStateGlobal.reportCount = new int?(HIDParser.ReadData((int)b2, ptr2, ptr));
							goto IL_0521;
						}
					}
					else
					{
						if (b3 == 160)
						{
							int num2 = num;
							num = list3.Count;
							list3.Add(new HID.HIDCollectionDescriptor
							{
								type = (HID.HIDCollectionType)HIDParser.ReadData((int)b2, ptr2, ptr),
								parent = num2,
								usagePage = hiditemStateGlobal.GetUsagePage(0, ref hiditemStateLocal),
								usage = hiditemStateLocal.GetUsage(0),
								firstChild = list2.Count
							});
							HIDParser.HIDItemStateLocal.Reset(ref hiditemStateLocal);
							goto IL_0521;
						}
						if (b3 != 176)
						{
							if (b3 != 192)
							{
								goto IL_0521;
							}
							if (num == -1)
							{
								return false;
							}
							HID.HIDCollectionDescriptor hidcollectionDescriptor = list3[num];
							hidcollectionDescriptor.childCount = list2.Count - hidcollectionDescriptor.firstChild;
							list3[num] = hidcollectionDescriptor;
							num = hidcollectionDescriptor.parent;
							HIDParser.HIDItemStateLocal.Reset(ref hiditemStateLocal);
							goto IL_0521;
						}
					}
					HID.HIDReportType hidreportType = ((b3 == 128) ? HID.HIDReportType.Input : ((b3 == 144) ? HID.HIDReportType.Output : HID.HIDReportType.Feature));
					int num3 = HIDParser.HIDReportData.FindOrAddReport(hiditemStateGlobal.reportId, hidreportType, list);
					HIDParser.HIDReportData hidreportData = list[num3];
					if (hidreportData.currentBitOffset == 0 && hiditemStateGlobal.reportId != null)
					{
						hidreportData.currentBitOffset = 8;
					}
					int valueOrDefault = hiditemStateGlobal.reportCount.GetValueOrDefault(1);
					int num4 = HIDParser.ReadData((int)b2, ptr2, ptr);
					for (int i = 0; i < valueOrDefault; i++)
					{
						HID.HIDElementDescriptor hidelementDescriptor = new HID.HIDElementDescriptor
						{
							usage = (hiditemStateLocal.GetUsage(i) & 65535),
							usagePage = hiditemStateGlobal.GetUsagePage(i, ref hiditemStateLocal),
							reportType = hidreportType,
							reportSizeInBits = hiditemStateGlobal.reportSize.GetValueOrDefault(8),
							reportOffsetInBits = hidreportData.currentBitOffset,
							reportId = hiditemStateGlobal.reportId.GetValueOrDefault(1),
							flags = (HID.HIDElementFlags)num4,
							logicalMin = hiditemStateGlobal.logicalMinimum.GetValueOrDefault(0),
							logicalMax = hiditemStateGlobal.logicalMaximum.GetValueOrDefault(0),
							physicalMin = hiditemStateGlobal.GetPhysicalMin(),
							physicalMax = hiditemStateGlobal.GetPhysicalMax(),
							unitExponent = hiditemStateGlobal.unitExponent.GetValueOrDefault(0),
							unit = hiditemStateGlobal.unit.GetValueOrDefault(0)
						};
						hidreportData.currentBitOffset += hidelementDescriptor.reportSizeInBits;
						list2.Add(hidelementDescriptor);
					}
					list[num3] = hidreportData;
					HIDParser.HIDItemStateLocal.Reset(ref hiditemStateLocal);
				}
				IL_0521:
				if (b2 == 3)
				{
					ptr2 += 4;
				}
				else
				{
					ptr2 += b2;
				}
			}
			deviceDescriptor.elements = list2.ToArray();
			deviceDescriptor.collections = list3.ToArray();
			foreach (HID.HIDCollectionDescriptor hidcollectionDescriptor2 in list3)
			{
				if (hidcollectionDescriptor2.parent == -1 && hidcollectionDescriptor2.type == HID.HIDCollectionType.Application)
				{
					deviceDescriptor.usage = hidcollectionDescriptor2.usage;
					deviceDescriptor.usagePage = hidcollectionDescriptor2.usagePage;
					break;
				}
			}
			return true;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003ECC0 File Offset: 0x0003CEC0
		private unsafe static int ReadData(int itemSize, byte* currentPtr, byte* endPtr)
		{
			if (itemSize == 0)
			{
				return 0;
			}
			if (itemSize == 1)
			{
				if (currentPtr >= endPtr)
				{
					return 0;
				}
				return (int)(*currentPtr);
			}
			else if (itemSize == 2)
			{
				if (currentPtr + 2 >= endPtr)
				{
					return 0;
				}
				byte b = *currentPtr;
				return ((int)currentPtr[1] << 8) | (int)b;
			}
			else
			{
				if (itemSize != 3)
				{
					return 0;
				}
				if (currentPtr + 4 >= endPtr)
				{
					return 0;
				}
				byte b2 = *currentPtr;
				byte b3 = currentPtr[1];
				byte b4 = currentPtr[2];
				return ((int)currentPtr[3] << 24) | ((int)b4 << 24) | ((int)b3 << 8) | (int)b2;
			}
		}

		// Token: 0x020001E6 RID: 486
		private struct HIDReportData
		{
			// Token: 0x0600144A RID: 5194 RVA: 0x0005E698 File Offset: 0x0005C898
			public static int FindOrAddReport(int? reportId, HID.HIDReportType reportType, List<HIDParser.HIDReportData> reports)
			{
				int num = 1;
				if (reportId != null)
				{
					num = reportId.Value;
				}
				for (int i = 0; i < reports.Count; i++)
				{
					if (reports[i].reportId == num && reports[i].reportType == reportType)
					{
						return i;
					}
				}
				reports.Add(new HIDParser.HIDReportData
				{
					reportId = num,
					reportType = reportType
				});
				return reports.Count - 1;
			}

			// Token: 0x04000A87 RID: 2695
			public int reportId;

			// Token: 0x04000A88 RID: 2696
			public HID.HIDReportType reportType;

			// Token: 0x04000A89 RID: 2697
			public int currentBitOffset;
		}

		// Token: 0x020001E7 RID: 487
		private enum HIDItemTypeAndTag
		{
			// Token: 0x04000A8B RID: 2699
			Input = 128,
			// Token: 0x04000A8C RID: 2700
			Output = 144,
			// Token: 0x04000A8D RID: 2701
			Feature = 176,
			// Token: 0x04000A8E RID: 2702
			Collection = 160,
			// Token: 0x04000A8F RID: 2703
			EndCollection = 192,
			// Token: 0x04000A90 RID: 2704
			UsagePage = 4,
			// Token: 0x04000A91 RID: 2705
			LogicalMinimum = 20,
			// Token: 0x04000A92 RID: 2706
			LogicalMaximum = 36,
			// Token: 0x04000A93 RID: 2707
			PhysicalMinimum = 52,
			// Token: 0x04000A94 RID: 2708
			PhysicalMaximum = 68,
			// Token: 0x04000A95 RID: 2709
			UnitExponent = 84,
			// Token: 0x04000A96 RID: 2710
			Unit = 100,
			// Token: 0x04000A97 RID: 2711
			ReportSize = 116,
			// Token: 0x04000A98 RID: 2712
			ReportID = 132,
			// Token: 0x04000A99 RID: 2713
			ReportCount = 148,
			// Token: 0x04000A9A RID: 2714
			Push = 164,
			// Token: 0x04000A9B RID: 2715
			Pop = 180,
			// Token: 0x04000A9C RID: 2716
			Usage = 8,
			// Token: 0x04000A9D RID: 2717
			UsageMinimum = 24,
			// Token: 0x04000A9E RID: 2718
			UsageMaximum = 40,
			// Token: 0x04000A9F RID: 2719
			DesignatorIndex = 56,
			// Token: 0x04000AA0 RID: 2720
			DesignatorMinimum = 72,
			// Token: 0x04000AA1 RID: 2721
			DesignatorMaximum = 88,
			// Token: 0x04000AA2 RID: 2722
			StringIndex = 120,
			// Token: 0x04000AA3 RID: 2723
			StringMinimum = 136,
			// Token: 0x04000AA4 RID: 2724
			StringMaximum = 152,
			// Token: 0x04000AA5 RID: 2725
			Delimiter = 168
		}

		// Token: 0x020001E8 RID: 488
		private struct HIDItemStateLocal
		{
			// Token: 0x0600144B RID: 5195 RVA: 0x0005E710 File Offset: 0x0005C910
			public static void Reset(ref HIDParser.HIDItemStateLocal state)
			{
				List<int> list = state.usageList;
				state = default(HIDParser.HIDItemStateLocal);
				if (list != null)
				{
					list.Clear();
					state.usageList = list;
				}
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x0005E73C File Offset: 0x0005C93C
			public void SetUsage(int value)
			{
				if (this.usage != null)
				{
					if (this.usageList == null)
					{
						this.usageList = new List<int>();
					}
					this.usageList.Add(this.usage.Value);
				}
				this.usage = new int?(value);
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x0005E78C File Offset: 0x0005C98C
			public int GetUsage(int index)
			{
				if (this.usageMinimum != null && this.usageMaximum != null)
				{
					int value = this.usageMinimum.Value;
					int value2 = this.usageMaximum.Value;
					int num = value2 - value;
					if (num < 0)
					{
						return 0;
					}
					if (index >= num)
					{
						return value2;
					}
					return value + index;
				}
				else if (this.usageList != null && this.usageList.Count > 0)
				{
					int count = this.usageList.Count;
					if (index >= count)
					{
						return this.usage.Value;
					}
					return this.usageList[index];
				}
				else
				{
					if (this.usage != null)
					{
						return this.usage.Value;
					}
					return 0;
				}
			}

			// Token: 0x04000AA6 RID: 2726
			public int? usage;

			// Token: 0x04000AA7 RID: 2727
			public int? usageMinimum;

			// Token: 0x04000AA8 RID: 2728
			public int? usageMaximum;

			// Token: 0x04000AA9 RID: 2729
			public int? designatorIndex;

			// Token: 0x04000AAA RID: 2730
			public int? designatorMinimum;

			// Token: 0x04000AAB RID: 2731
			public int? designatorMaximum;

			// Token: 0x04000AAC RID: 2732
			public int? stringIndex;

			// Token: 0x04000AAD RID: 2733
			public int? stringMinimum;

			// Token: 0x04000AAE RID: 2734
			public int? stringMaximum;

			// Token: 0x04000AAF RID: 2735
			public List<int> usageList;
		}

		// Token: 0x020001E9 RID: 489
		private struct HIDItemStateGlobal
		{
			// Token: 0x0600144E RID: 5198 RVA: 0x0005E838 File Offset: 0x0005CA38
			public HID.UsagePage GetUsagePage(int index, ref HIDParser.HIDItemStateLocal localItemState)
			{
				if (this.usagePage == null)
				{
					return (HID.UsagePage)(localItemState.GetUsage(index) >> 16);
				}
				return (HID.UsagePage)this.usagePage.Value;
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x0005E860 File Offset: 0x0005CA60
			public int GetPhysicalMin()
			{
				if (this.physicalMinimum == null || this.physicalMaximum == null || (this.physicalMinimum.Value == 0 && this.physicalMaximum.Value == 0))
				{
					return this.logicalMinimum.GetValueOrDefault(0);
				}
				return this.physicalMinimum.Value;
			}

			// Token: 0x06001450 RID: 5200 RVA: 0x0005E8BC File Offset: 0x0005CABC
			public int GetPhysicalMax()
			{
				if (this.physicalMinimum == null || this.physicalMaximum == null || (this.physicalMinimum.Value == 0 && this.physicalMaximum.Value == 0))
				{
					return this.logicalMaximum.GetValueOrDefault(0);
				}
				return this.physicalMaximum.Value;
			}

			// Token: 0x04000AB0 RID: 2736
			public int? usagePage;

			// Token: 0x04000AB1 RID: 2737
			public int? logicalMinimum;

			// Token: 0x04000AB2 RID: 2738
			public int? logicalMaximum;

			// Token: 0x04000AB3 RID: 2739
			public int? physicalMinimum;

			// Token: 0x04000AB4 RID: 2740
			public int? physicalMaximum;

			// Token: 0x04000AB5 RID: 2741
			public int? unitExponent;

			// Token: 0x04000AB6 RID: 2742
			public int? unit;

			// Token: 0x04000AB7 RID: 2743
			public int? reportSize;

			// Token: 0x04000AB8 RID: 2744
			public int? reportCount;

			// Token: 0x04000AB9 RID: 2745
			public int? reportId;
		}
	}
}
