using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	/// <summary>A container for name/value tuples encoded using application/x-www-form-urlencoded MIME type.</summary>
	// Token: 0x02000014 RID: 20
	public class FormUrlEncodedContent : ByteArrayContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.FormUrlEncodedContent" /> class with a specific collection of name/value pairs.</summary>
		/// <param name="nameValueCollection">A collection of name/value pairs.</param>
		// Token: 0x060000C2 RID: 194 RVA: 0x00003B54 File Offset: 0x00001D54
		public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
			: base(FormUrlEncodedContent.EncodeContent(nameValueCollection))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003B78 File Offset: 0x00001D78
		private static byte[] EncodeContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw new ArgumentNullException("nameValueCollection");
			}
			List<byte> list = new List<byte>();
			foreach (KeyValuePair<string, string> keyValuePair in nameValueCollection)
			{
				if (list.Count != 0)
				{
					list.Add(38);
				}
				byte[] array = FormUrlEncodedContent.SerializeValue(keyValuePair.Key);
				if (array != null)
				{
					list.AddRange(array);
				}
				list.Add(61);
				array = FormUrlEncodedContent.SerializeValue(keyValuePair.Value);
				if (array != null)
				{
					list.AddRange(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003C1C File Offset: 0x00001E1C
		private static byte[] SerializeValue(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = Uri.EscapeDataString(value).Replace("%20", "+");
			return Encoding.ASCII.GetBytes(value);
		}
	}
}
