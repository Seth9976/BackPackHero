using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Mime
{
	// Token: 0x02000620 RID: 1568
	internal class SmtpDateTime
	{
		// Token: 0x0600324A RID: 12874 RVA: 0x000B48B0 File Offset: 0x000B2AB0
		internal static Dictionary<string, TimeSpan> InitializeShortHandLookups()
		{
			return new Dictionary<string, TimeSpan>
			{
				{
					"UT",
					TimeSpan.Zero
				},
				{
					"GMT",
					TimeSpan.Zero
				},
				{
					"EDT",
					new TimeSpan(-4, 0, 0)
				},
				{
					"EST",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CDT",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CST",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MDT",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MST",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PDT",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PST",
					new TimeSpan(-8, 0, 0)
				}
			};
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000B4984 File Offset: 0x000B2B84
		internal SmtpDateTime(DateTime value)
		{
			this._date = value;
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				this._unknownTimeZone = true;
				return;
			case DateTimeKind.Utc:
				this._timeZone = TimeSpan.Zero;
				return;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				this._timeZone = this.ValidateAndGetSanitizedTimeSpan(utcOffset);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000B49E8 File Offset: 0x000B2BE8
		internal SmtpDateTime(string value)
		{
			string text;
			this._date = this.ParseValue(value, out text);
			if (!this.TryParseTimeZoneString(text, out this._timeZone))
			{
				this._unknownTimeZone = true;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000B4A20 File Offset: 0x000B2C20
		internal DateTime Date
		{
			get
			{
				if (this._unknownTimeZone)
				{
					return DateTime.SpecifyKind(this._date, DateTimeKind.Unspecified);
				}
				DateTimeOffset dateTimeOffset = new DateTimeOffset(this._date, this._timeZone);
				return dateTimeOffset.LocalDateTime;
			}
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000B4A5C File Offset: 0x000B2C5C
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FormatDate(this._date), this._unknownTimeZone ? "-0000" : this.TimeSpanToOffset(this._timeZone));
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000B4A90 File Offset: 0x000B2C90
		internal void ValidateAndGetTimeZoneOffsetValues(string offset, out bool positive, out int hours, out int minutes)
		{
			if (offset.Length != 5)
			{
				throw new FormatException("The date is in an invalid format.");
			}
			positive = offset.StartsWith("+", StringComparison.Ordinal);
			if (!int.TryParse(offset.Substring(1, 2), NumberStyles.None, CultureInfo.InvariantCulture, out hours))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			if (!int.TryParse(offset.Substring(3, 2), NumberStyles.None, CultureInfo.InvariantCulture, out minutes))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			if (minutes > 59)
			{
				throw new FormatException("The date is in an invalid format.");
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000B4B14 File Offset: 0x000B2D14
		internal void ValidateTimeZoneShortHandValue(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsLetter(value, i))
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", value));
				}
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000B4B4C File Offset: 0x000B2D4C
		internal string FormatDate(DateTime value)
		{
			return value.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000B4B60 File Offset: 0x000B2D60
		internal DateTime ParseValue(string data, out string timeZone)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			int num = data.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data));
			}
			int num2 = data.IndexOfAny(SmtpDateTime.s_allowedWhiteSpaceChars, num);
			if (num2 == -1)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data));
			}
			DateTime dateTime;
			if (!DateTime.TryParseExact(data.Substring(0, num2).Trim(), SmtpDateTime.s_validDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			string text = data.Substring(num2).Trim();
			int num3 = text.IndexOfAny(SmtpDateTime.s_allowedWhiteSpaceChars);
			if (num3 != -1)
			{
				text = text.Substring(0, num3);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			timeZone = text;
			return dateTime;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000B4C2C File Offset: 0x000B2E2C
		internal bool TryParseTimeZoneString(string timeZoneString, out TimeSpan timeZone)
		{
			if (timeZoneString == "-0000")
			{
				timeZone = TimeSpan.Zero;
				return false;
			}
			if (timeZoneString[0] == '+' || timeZoneString[0] == '-')
			{
				bool flag;
				int num;
				int num2;
				this.ValidateAndGetTimeZoneOffsetValues(timeZoneString, out flag, out num, out num2);
				if (!flag)
				{
					if (num != 0)
					{
						num *= -1;
					}
					else if (num2 != 0)
					{
						num2 *= -1;
					}
				}
				timeZone = new TimeSpan(num, num2, 0);
				return true;
			}
			this.ValidateTimeZoneShortHandValue(timeZoneString);
			return SmtpDateTime.s_timeZoneOffsetLookup.TryGetValue(timeZoneString, out timeZone);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000B4CAC File Offset: 0x000B2EAC
		internal TimeSpan ValidateAndGetSanitizedTimeSpan(TimeSpan span)
		{
			TimeSpan timeSpan = new TimeSpan(span.Days, span.Hours, span.Minutes, 0, 0);
			if (Math.Abs(timeSpan.Ticks) > 3599400000000L)
			{
				throw new FormatException("The date is in an invalid format.");
			}
			return timeSpan;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000B4CFC File Offset: 0x000B2EFC
		internal string TimeSpanToOffset(TimeSpan span)
		{
			if (span.Ticks == 0L)
			{
				return "+0000";
			}
			uint num = (uint)Math.Abs(Math.Floor(span.TotalHours));
			uint num2 = (uint)Math.Abs(span.Minutes);
			string text = ((span.Ticks > 0L) ? "+" : "-");
			if (num < 10U)
			{
				text += "0";
			}
			text += num.ToString();
			if (num2 < 10U)
			{
				text += "0";
			}
			return text + num2.ToString();
		}

		// Token: 0x04001EB4 RID: 7860
		internal const string UnknownTimeZoneDefaultOffset = "-0000";

		// Token: 0x04001EB5 RID: 7861
		internal const string UtcDefaultTimeZoneOffset = "+0000";

		// Token: 0x04001EB6 RID: 7862
		internal const int OffsetLength = 5;

		// Token: 0x04001EB7 RID: 7863
		internal const int MaxMinuteValue = 59;

		// Token: 0x04001EB8 RID: 7864
		internal const string DateFormatWithDayOfWeek = "ddd, dd MMM yyyy HH:mm:ss";

		// Token: 0x04001EB9 RID: 7865
		internal const string DateFormatWithoutDayOfWeek = "dd MMM yyyy HH:mm:ss";

		// Token: 0x04001EBA RID: 7866
		internal const string DateFormatWithDayOfWeekAndNoSeconds = "ddd, dd MMM yyyy HH:mm";

		// Token: 0x04001EBB RID: 7867
		internal const string DateFormatWithoutDayOfWeekAndNoSeconds = "dd MMM yyyy HH:mm";

		// Token: 0x04001EBC RID: 7868
		internal static readonly string[] s_validDateTimeFormats = new string[] { "ddd, dd MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss", "ddd, dd MMM yyyy HH:mm", "dd MMM yyyy HH:mm" };

		// Token: 0x04001EBD RID: 7869
		internal static readonly char[] s_allowedWhiteSpaceChars = new char[] { ' ', '\t' };

		// Token: 0x04001EBE RID: 7870
		internal static readonly Dictionary<string, TimeSpan> s_timeZoneOffsetLookup = SmtpDateTime.InitializeShortHandLookups();

		// Token: 0x04001EBF RID: 7871
		internal const long TimeSpanMaxTicks = 3599400000000L;

		// Token: 0x04001EC0 RID: 7872
		internal const int OffsetMaxValue = 9959;

		// Token: 0x04001EC1 RID: 7873
		private readonly DateTime _date;

		// Token: 0x04001EC2 RID: 7874
		private readonly TimeSpan _timeZone;

		// Token: 0x04001EC3 RID: 7875
		private readonly bool _unknownTimeZone;
	}
}
