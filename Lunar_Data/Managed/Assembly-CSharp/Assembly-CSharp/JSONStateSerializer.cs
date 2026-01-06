using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SaveSystem.States;

namespace SaveSystem
{
	// Token: 0x020000B0 RID: 176
	public class JSONStateSerializer
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00016B84 File Offset: 0x00014D84
		public static T Deserialize<T>(string jsonString) where T : State
		{
			State state = JsonConvert.DeserializeObject<State>(jsonString);
			if (State.stateClasses[state.type] != typeof(T))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Serialized State cannot map to ",
					typeof(T).ToString(),
					" (",
					state.type.ToString(),
					")."
				}));
			}
			Type type = ((Dictionary<int, Type>)State.stateClasses[state.type].GetField("versionClasses").GetValue(null))[state.version];
			object obj = JsonConvert.DeserializeObject(jsonString, type);
			if (obj.GetType() != typeof(T))
			{
				for (;;)
				{
					object obj2 = type.GetMethod("Migrate").Invoke(obj, null);
					if (obj2 == null)
					{
						break;
					}
					obj = obj2;
					type = obj.GetType();
				}
			}
			return (T)((object)obj);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016C7F File Offset: 0x00014E7F
		public static string Serialize<T>(T state) where T : State
		{
			return JsonConvert.SerializeObject(state);
		}

		// Token: 0x02000127 RID: 295
		public class DefaultUnknownEnumConverter : StringEnumConverter
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0001A501 File Offset: 0x00018701
			public DefaultUnknownEnumConverter()
			{
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0001A509 File Offset: 0x00018709
			public DefaultUnknownEnumConverter(int defaultValue)
			{
				this.defaultValue = defaultValue;
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001A518 File Offset: 0x00018718
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				object obj;
				try
				{
					obj = base.ReadJson(reader, objectType, existingValue, serializer);
				}
				catch
				{
					obj = Enum.Parse(objectType, string.Format("{0}", this.defaultValue));
				}
				return obj;
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0001A564 File Offset: 0x00018764
			public override bool CanConvert(Type objectType)
			{
				return base.CanConvert(objectType) && objectType.GetTypeInfo().IsEnum && Enum.IsDefined(objectType, this.defaultValue);
			}

			// Token: 0x04000530 RID: 1328
			private readonly int defaultValue;
		}
	}
}
