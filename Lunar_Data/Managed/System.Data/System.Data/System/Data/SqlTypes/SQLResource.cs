using System;

namespace System.Data.SqlTypes
{
	// Token: 0x020002B2 RID: 690
	internal static class SQLResource
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00093183 File Offset: 0x00091383
		internal static string NullString
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0009318A File Offset: 0x0009138A
		internal static string MessageString
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x00093191 File Offset: 0x00091391
		internal static string ArithOverflowMessage
		{
			get
			{
				return "Arithmetic Overflow.";
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00093198 File Offset: 0x00091398
		internal static string DivideByZeroMessage
		{
			get
			{
				return "Divide by zero error encountered.";
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0009319F File Offset: 0x0009139F
		internal static string NullValueMessage
		{
			get
			{
				return "Data is Null. This method or property cannot be called on Null values.";
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000931A6 File Offset: 0x000913A6
		internal static string TruncationMessage
		{
			get
			{
				return "Numeric arithmetic causes truncation.";
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000931AD File Offset: 0x000913AD
		internal static string DateTimeOverflowMessage
		{
			get
			{
				return "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000931B4 File Offset: 0x000913B4
		internal static string ConcatDiffCollationMessage
		{
			get
			{
				return "Two strings to be concatenated have different collation.";
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000931BB File Offset: 0x000913BB
		internal static string CompareDiffCollationMessage
		{
			get
			{
				return "Two strings to be compared have different collation.";
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000931C2 File Offset: 0x000913C2
		internal static string InvalidFlagMessage
		{
			get
			{
				return "Invalid flag value.";
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000931C9 File Offset: 0x000913C9
		internal static string NumeToDecOverflowMessage
		{
			get
			{
				return "Conversion from SqlDecimal to Decimal overflows.";
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000931D0 File Offset: 0x000913D0
		internal static string ConversionOverflowMessage
		{
			get
			{
				return "Conversion overflows.";
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000931D7 File Offset: 0x000913D7
		internal static string InvalidDateTimeMessage
		{
			get
			{
				return "Invalid SqlDateTime.";
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000931DE File Offset: 0x000913DE
		internal static string TimeZoneSpecifiedMessage
		{
			get
			{
				return "A time zone was specified. SqlDateTime does not support time zones.";
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000931E5 File Offset: 0x000913E5
		internal static string InvalidArraySizeMessage
		{
			get
			{
				return "Invalid array size.";
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000931EC File Offset: 0x000913EC
		internal static string InvalidPrecScaleMessage
		{
			get
			{
				return "Invalid numeric precision/scale.";
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000931F3 File Offset: 0x000913F3
		internal static string FormatMessage
		{
			get
			{
				return "The input wasn't in a correct format.";
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x000931FA File Offset: 0x000913FA
		internal static string NotFilledMessage
		{
			get
			{
				return "SQL Type has not been loaded with data.";
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00093201 File Offset: 0x00091401
		internal static string AlreadyFilledMessage
		{
			get
			{
				return "SQL Type has already been loaded with data.";
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00093208 File Offset: 0x00091408
		internal static string ClosedXmlReaderMessage
		{
			get
			{
				return "Invalid attempt to access a closed XmlReader.";
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0009320F File Offset: 0x0009140F
		internal static string InvalidOpStreamClosed(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is closed.", method);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0009321C File Offset: 0x0009141C
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-writable.", method);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00093229 File Offset: 0x00091429
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-readable.", method);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00093236 File Offset: 0x00091436
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is non-seekable.", method);
		}
	}
}
