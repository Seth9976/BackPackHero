using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C7 RID: 199
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0002CC0D File Offset: 0x0002AE0D
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002CC31 File Offset: 0x0002AE31
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002CC39 File Offset: 0x0002AE39
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002CC50 File Offset: 0x0002AE50
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002CC71 File Offset: 0x0002AE71
		public JTokenWriter()
		{
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002CC79 File Offset: 0x0002AE79
		public override void Flush()
		{
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002CC7B File Offset: 0x0002AE7B
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CC83 File Offset: 0x0002AE83
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CC96 File Offset: 0x0002AE96
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002CD15 File Offset: 0x0002AF15
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CD28 File Offset: 0x0002AF28
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CD3D File Offset: 0x0002AF3D
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CD45 File Offset: 0x0002AF45
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002CD72 File Offset: 0x0002AF72
		private void AddRawValue(object value, JTokenType type, JsonToken token)
		{
			this.AddJValue(new JValue(value, type), token);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002CD84 File Offset: 0x0002AF84
		internal void AddJValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = value ?? JValue.CreateNull();
				this._current = this._value;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CE16 File Offset: 0x0002B016
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddJValue(JValue.CreateNull(), JsonToken.Null);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002CE2B File Offset: 0x0002B02B
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddJValue(JValue.CreateUndefined(), JsonToken.Undefined);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002CE40 File Offset: 0x0002B040
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddJValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CE56 File Offset: 0x0002B056
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddJValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CE6C File Offset: 0x0002B06C
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002CE83 File Offset: 0x0002B083
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002CE9A File Offset: 0x0002B09A
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002CEB1 File Offset: 0x0002B0B1
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002CEC7 File Offset: 0x0002B0C7
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002CEDD File Offset: 0x0002B0DD
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002CEF3 File Offset: 0x0002B0F3
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002CF09 File Offset: 0x0002B109
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Boolean);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002CF20 File Offset: 0x0002B120
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002CF37 File Offset: 0x0002B137
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002CF50 File Offset: 0x0002B150
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddJValue(new JValue(text), JsonToken.String);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002CF7F File Offset: 0x0002B17F
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002CF96 File Offset: 0x0002B196
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002CFAD File Offset: 0x0002B1AD
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002CFC3 File Offset: 0x0002B1C3
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002CFFF File Offset: 0x0002B1FF
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value, JTokenType.Bytes), JsonToken.Bytes);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002D018 File Offset: 0x0002B218
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002D02F File Offset: 0x0002B22F
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002D046 File Offset: 0x0002B246
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D060 File Offset: 0x0002B260
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken(null);
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = jtoken as JContainer;
					this._value = jtoken as JValue;
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x040003B2 RID: 946
		private JContainer _token;

		// Token: 0x040003B3 RID: 947
		private JContainer _parent;

		// Token: 0x040003B4 RID: 948
		private JValue _value;

		// Token: 0x040003B5 RID: 949
		private JToken _current;
	}
}
