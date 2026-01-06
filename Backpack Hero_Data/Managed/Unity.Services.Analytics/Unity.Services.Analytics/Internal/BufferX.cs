using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000029 RID: 41
	internal class BufferX : IBuffer
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000394B File Offset: 0x00001B4B
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003953 File Offset: 0x00001B53
		public string UserID { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000395C File Offset: 0x00001B5C
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003964 File Offset: 0x00001B64
		public string InstallID { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000396D File Offset: 0x00001B6D
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003975 File Offset: 0x00001B75
		public string PlayerID { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000397E File Offset: 0x00001B7E
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003986 File Offset: 0x00001B86
		public string SessionID { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000398F File Offset: 0x00001B8F
		public int Length
		{
			get
			{
				return (int)this.m_Buffer.Length;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000399D File Offset: 0x00001B9D
		internal int EventsRecorded
		{
			get
			{
				return this.m_EventEnds.Count;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000039AA File Offset: 0x00001BAA
		internal IReadOnlyList<int> EventEndIndices
		{
			get
			{
				return this.m_EventEnds;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000039B2 File Offset: 0x00001BB2
		internal byte[] RawContents
		{
			get
			{
				return this.m_Buffer.ToArray();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000039BF File Offset: 0x00001BBF
		public BufferX(IBufferSystemCalls eventIdGenerator, IDiskCache diskCache)
		{
			this.m_Buffer = new MemoryStream();
			this.m_SpareBuffer = new MemoryStream();
			this.m_EventEnds = new List<int>();
			this.m_SystemCalls = eventIdGenerator;
			this.m_DiskCache = diskCache;
			this.ClearBuffer();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000039FC File Offset: 0x00001BFC
		private void WriteString(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			for (int i = 0; i < bytes.Length; i++)
			{
				this.m_Buffer.WriteByte(bytes[i]);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A34 File Offset: 0x00001C34
		public void PushStartEvent(string name, DateTime datetime, long? eventVersion, bool addPlayerIdsToEventBody)
		{
			this.WriteString("{");
			this.WriteString("\"eventName\":\"");
			this.WriteString(name);
			this.WriteString("\",");
			this.WriteString("\"userID\":\"");
			this.WriteString(this.UserID);
			this.WriteString("\",");
			this.WriteString("\"sessionID\":\"");
			this.WriteString(this.SessionID);
			this.WriteString("\",");
			this.WriteString("\"eventUUID\":\"");
			this.WriteString(this.m_SystemCalls.GenerateGuid());
			this.WriteString("\",");
			this.WriteString("\"eventTimestamp\":\"");
			this.WriteString(BufferX.SerializeDateTime(datetime));
			this.WriteString("\",");
			if (eventVersion != null)
			{
				this.WriteString("\"eventVersion\":");
				this.WriteString(eventVersion.ToString());
				this.WriteString(",");
			}
			if (addPlayerIdsToEventBody)
			{
				this.WriteString("\"unityInstallationID\":\"");
				this.WriteString(this.InstallID);
				this.WriteString("\",");
				if (!string.IsNullOrEmpty(this.PlayerID))
				{
					this.WriteString("\"unityPlayerID\":\"");
					this.WriteString(this.PlayerID);
					this.WriteString("\",");
				}
			}
			this.WriteString("\"eventParams\":{");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B88 File Offset: 0x00001D88
		private void StripTrailingCommaIfNecessary()
		{
			this.m_Buffer.Seek(-1L, SeekOrigin.End);
			if ((ushort)this.m_Buffer.ReadByte() == 44)
			{
				this.m_Buffer.Seek(-1L, SeekOrigin.Current);
				this.m_Buffer.SetLength(this.m_Buffer.Length - 1L);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BDC File Offset: 0x00001DDC
		public void PushEndEvent()
		{
			this.StripTrailingCommaIfNecessary();
			this.WriteString("}},");
			int num = (int)this.m_Buffer.Length;
			if ((long)((this.m_EventEnds.Count > 0) ? (num - this.m_EventEnds[this.m_EventEnds.Count - 1]) : num) > 4194304L)
			{
				Debug.LogWarning(string.Format("Detected event that would be too big to upload (greater than {0}KB in size), discarding it to prevent blockage.", 4096L));
				int num2 = ((this.m_EventEnds.Count > 0) ? this.m_EventEnds[this.m_EventEnds.Count - 1] : "{\"eventList\":[".Length);
				this.m_Buffer.SetLength((long)num2);
				this.m_Buffer.Position = (long)num2;
				return;
			}
			this.m_EventEnds.Add(num);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003CAF File Offset: 0x00001EAF
		public void PushObjectStart(string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			this.WriteString("{");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003CDC File Offset: 0x00001EDC
		public void PushObjectEnd()
		{
			this.StripTrailingCommaIfNecessary();
			this.WriteString("},");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003CEF File Offset: 0x00001EEF
		public void PushArrayStart(string name)
		{
			this.WriteString("\"");
			this.WriteString(name);
			this.WriteString("\":");
			this.WriteString("[");
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003D19 File Offset: 0x00001F19
		public void PushArrayEnd()
		{
			this.StripTrailingCommaIfNecessary();
			this.WriteString("],");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003D2C File Offset: 0x00001F2C
		public void PushDouble(double val, string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			string text = val.ToString(CultureInfo.InvariantCulture);
			this.WriteString(text);
			this.WriteString(",");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D78 File Offset: 0x00001F78
		public void PushFloat(float val, string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			string text = val.ToString(CultureInfo.InvariantCulture);
			this.WriteString(text);
			this.WriteString(",");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public void PushString(string val, string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			this.WriteString(JsonConvert.ToString(val));
			this.WriteString(",");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003DFD File Offset: 0x00001FFD
		public void PushInt64(long val, string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			this.WriteString(val.ToString());
			this.WriteString(",");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003E37 File Offset: 0x00002037
		public void PushInt(int val, string name = null)
		{
			this.PushInt64((long)val, name);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003E44 File Offset: 0x00002044
		public void PushBool(bool val, string name = null)
		{
			if (name != null)
			{
				this.WriteString("\"");
				this.WriteString(name);
				this.WriteString("\":");
			}
			this.WriteString(val ? "true" : "false");
			this.WriteString(",");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003E91 File Offset: 0x00002091
		public void PushTimestamp(DateTime val, string name)
		{
			this.WriteString("\"");
			this.WriteString(name);
			this.WriteString("\":\"");
			this.WriteString(BufferX.SerializeDateTime(val));
			this.WriteString("\",");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003EC8 File Offset: 0x000020C8
		[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
		public void PushEvent(Event evt)
		{
			DateTime dateTime = this.m_SystemCalls.Now();
			string name = evt.Name;
			DateTime dateTime2 = dateTime;
			int? version = evt.Version;
			this.PushStartEvent(name, dateTime2, (version != null) ? new long?((long)version.GetValueOrDefault()) : null, false);
			foreach (KeyValuePair<string, object> keyValuePair in evt.Parameters.Data)
			{
				object obj = keyValuePair.Value;
				if (obj is float)
				{
					float num = (float)obj;
					this.PushFloat(num, keyValuePair.Key);
				}
				else
				{
					obj = keyValuePair.Value;
					if (obj is double)
					{
						double num2 = (double)obj;
						this.PushDouble(num2, keyValuePair.Key);
					}
					else
					{
						string text = keyValuePair.Value as string;
						if (text != null)
						{
							this.PushString(text, keyValuePair.Key);
						}
						else
						{
							obj = keyValuePair.Value;
							if (obj is int)
							{
								int num3 = (int)obj;
								this.PushInt(num3, keyValuePair.Key);
							}
							else
							{
								obj = keyValuePair.Value;
								if (obj is long)
								{
									long num4 = (long)obj;
									this.PushInt64(num4, keyValuePair.Key);
								}
								else
								{
									obj = keyValuePair.Value;
									if (obj is bool)
									{
										bool flag = (bool)obj;
										this.PushBool(flag, keyValuePair.Key);
									}
								}
							}
						}
					}
				}
			}
			this.PushEndEvent();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000407C File Offset: 0x0000227C
		public byte[] Serialize()
		{
			if (this.m_EventEnds.Count > 0)
			{
				long position = this.m_Buffer.Position;
				int num = this.m_EventEnds[0];
				int num2 = 0;
				while (num2 < this.m_EventEnds.Count && (long)this.m_EventEnds[num2] < 4194304L)
				{
					num = this.m_EventEnds[num2];
					num2++;
				}
				byte[] array = new byte[num + 1];
				this.m_Buffer.Position = 0L;
				this.m_Buffer.Read(array, 0, num);
				byte[] bytes = Encoding.UTF8.GetBytes("]}");
				array[num - 1] = bytes[0];
				array[num] = bytes[1];
				this.m_Buffer.Position = position;
				return array;
			}
			return null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004140 File Offset: 0x00002340
		public void ClearBuffer()
		{
			this.m_Buffer.SetLength(0L);
			this.m_Buffer.Position = 0L;
			this.WriteString("{\"eventList\":[");
			this.m_EventEnds.Clear();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004174 File Offset: 0x00002374
		public void ClearBuffer(long upTo)
		{
			MemoryStream buffer = this.m_Buffer;
			this.m_Buffer = this.m_SpareBuffer;
			this.m_SpareBuffer = buffer;
			int num = 0;
			for (int i = 0; i < this.m_EventEnds.Count; i++)
			{
				this.m_EventEnds[i] = this.m_EventEnds[i] - (int)upTo + "{\"eventList\":[".Length;
				if (this.m_EventEnds[i] <= "{\"eventList\":[".Length)
				{
					num = i;
				}
			}
			this.m_EventEnds.RemoveRange(0, num + 1);
			this.m_Buffer.SetLength(0L);
			this.m_Buffer.Position = 0L;
			this.WriteString("{\"eventList\":[");
			this.m_SpareBuffer.Position = upTo;
			for (long num2 = upTo; num2 < this.m_SpareBuffer.Length; num2 += 1L)
			{
				byte b = (byte)this.m_SpareBuffer.ReadByte();
				this.m_Buffer.WriteByte(b);
			}
			this.m_SpareBuffer.SetLength(0L);
			this.m_SpareBuffer.Position = 0L;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000427F File Offset: 0x0000247F
		public void FlushToDisk()
		{
			this.m_DiskCache.Write(this.m_EventEnds, this.m_Buffer);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004298 File Offset: 0x00002498
		public void ClearDiskCache()
		{
			this.m_DiskCache.Clear();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000042A5 File Offset: 0x000024A5
		public void LoadFromDisk()
		{
			if (!this.m_DiskCache.Read(this.m_EventEnds, this.m_Buffer))
			{
				this.ClearBuffer();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000042C6 File Offset: 0x000024C6
		internal static string SerializeDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x040000AA RID: 170
		private const long k_UploadBatchMaximumSizeInBytes = 4194304L;

		// Token: 0x040000AB RID: 171
		private const string k_BufferHeader = "{\"eventList\":[";

		// Token: 0x040000AC RID: 172
		private const string k_SecondDateFormat = "yyyy-MM-dd HH:mm:ss zzz";

		// Token: 0x040000AD RID: 173
		private const string k_MillisecondDateFormat = "yyyy-MM-dd HH:mm:ss.fff zzz";

		// Token: 0x040000AE RID: 174
		private readonly IBufferSystemCalls m_SystemCalls;

		// Token: 0x040000AF RID: 175
		private readonly IDiskCache m_DiskCache;

		// Token: 0x040000B0 RID: 176
		private readonly List<int> m_EventEnds;

		// Token: 0x040000B1 RID: 177
		private MemoryStream m_SpareBuffer;

		// Token: 0x040000B2 RID: 178
		private MemoryStream m_Buffer;
	}
}
