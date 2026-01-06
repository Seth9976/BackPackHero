using System;
using System.Globalization;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000177 RID: 375
	public class fsDateConverter : fsConverter
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00029AE1 File Offset: 0x00027CE1
		private string DateTimeFormatString
		{
			get
			{
				return this.Serializer.Config.CustomDateTimeFormatString ?? "o";
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00029AFC File Offset: 0x00027CFC
		public override bool CanProcess(Type type)
		{
			return type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00029B34 File Offset: 0x00027D34
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			if (instance is DateTime)
			{
				serialized = new fsData(((DateTime)instance).ToString(this.DateTimeFormatString));
				return fsResult.Success;
			}
			if (instance is DateTimeOffset)
			{
				serialized = new fsData(((DateTimeOffset)instance).ToString("o"));
				return fsResult.Success;
			}
			if (instance is TimeSpan)
			{
				serialized = new fsData(((TimeSpan)instance).ToString());
				return fsResult.Success;
			}
			throw new InvalidOperationException("FullSerializer Internal Error -- Unexpected serialization type");
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00029BC8 File Offset: 0x00027DC8
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Date deserialization requires a string, not " + data.Type.ToString());
			}
			if (storageType == typeof(DateTime))
			{
				DateTime dateTime;
				if (DateTime.TryParse(data.AsString, null, DateTimeStyles.RoundtripKind, out dateTime))
				{
					instance = dateTime;
					return fsResult.Success;
				}
				if (fsGlobalConfig.AllowInternalExceptions)
				{
					try
					{
						instance = Convert.ToDateTime(data.AsString);
						return fsResult.Success;
					}
					catch (Exception ex)
					{
						string text = "Unable to parse ";
						string asString = data.AsString;
						string text2 = " into a DateTime; got exception ";
						Exception ex2 = ex;
						return fsResult.Fail(text + asString + text2 + ((ex2 != null) ? ex2.ToString() : null));
					}
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a DateTime");
			}
			else if (storageType == typeof(DateTimeOffset))
			{
				DateTimeOffset dateTimeOffset;
				if (DateTimeOffset.TryParse(data.AsString, null, DateTimeStyles.RoundtripKind, out dateTimeOffset))
				{
					instance = dateTimeOffset;
					return fsResult.Success;
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a DateTimeOffset");
			}
			else
			{
				if (!(storageType == typeof(TimeSpan)))
				{
					throw new InvalidOperationException("FullSerializer Internal Error -- Unexpected deserialization type");
				}
				TimeSpan timeSpan;
				if (TimeSpan.TryParse(data.AsString, out timeSpan))
				{
					instance = timeSpan;
					return fsResult.Success;
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a TimeSpan");
			}
		}

		// Token: 0x0400025A RID: 602
		private const string DefaultDateTimeFormatString = "o";

		// Token: 0x0400025B RID: 603
		private const string DateTimeOffsetFormatString = "o";
	}
}
