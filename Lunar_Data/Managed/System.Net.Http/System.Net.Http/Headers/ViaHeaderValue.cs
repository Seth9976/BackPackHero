using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of a Via header.</summary>
	// Token: 0x0200006A RID: 106
	public class ViaHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000D053 File Offset: 0x0000B253
		public ViaHeaderValue(string protocolVersion, string receivedBy)
		{
			Parser.Token.Check(protocolVersion);
			Parser.Uri.Check(receivedBy);
			this.ProtocolVersion = protocolVersion;
			this.ReceivedBy = receivedBy;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000D075 File Offset: 0x0000B275
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName)
			: this(protocolVersion, receivedBy)
		{
			if (!string.IsNullOrEmpty(protocolName))
			{
				Parser.Token.Check(protocolName);
				this.ProtocolName = protocolName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		/// <param name="comment">The comment field used to identify the software of the recipient proxy or gateway.</param>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000D094 File Offset: 0x0000B294
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName, string comment)
			: this(protocolVersion, receivedBy, protocolName)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				Parser.Token.CheckComment(comment);
				this.Comment = comment;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00002300 File Offset: 0x00000500
		private ViaHeaderValue()
		{
		}

		/// <summary>Gets the comment field used to identify the software of the recipient proxy or gateway.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The comment field used to identify the software of the recipient proxy or gateway.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000D0BF File Offset: 0x0000B2BF
		public string Comment { get; private set; }

		/// <summary>Gets the protocol name of the received protocol.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The protocol name.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public string ProtocolName { get; private set; }

		/// <summary>Gets the protocol version of the received protocol.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The protocol version.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000D0E1 File Offset: 0x0000B2E1
		public string ProtocolVersion { get; private set; }

		/// <summary>Gets the host and port that the request or response was received by.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The host and port that the request or response was received by.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000D0EA File Offset: 0x0000B2EA
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public string ReceivedBy { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x060003D1 RID: 977 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" />object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060003D2 RID: 978 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public override bool Equals(object obj)
		{
			ViaHeaderValue viaHeaderValue = obj as ViaHeaderValue;
			return viaHeaderValue != null && (string.Equals(viaHeaderValue.Comment, this.Comment, StringComparison.Ordinal) && string.Equals(viaHeaderValue.ProtocolName, this.ProtocolName, StringComparison.OrdinalIgnoreCase) && string.Equals(viaHeaderValue.ProtocolVersion, this.ProtocolVersion, StringComparison.OrdinalIgnoreCase)) && string.Equals(viaHeaderValue.ReceivedBy, this.ReceivedBy, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.Returns a hash code for the current object.</returns>
		// Token: 0x060003D3 RID: 979 RVA: 0x0000D168 File Offset: 0x0000B368
		public override int GetHashCode()
		{
			int num = this.ProtocolVersion.ToLowerInvariant().GetHashCode();
			num ^= this.ReceivedBy.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.ProtocolName))
			{
				num ^= this.ProtocolName.ToLowerInvariant().GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.Comment))
			{
				num ^= this.Comment.GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.ViaHeaderValue" />.An <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents via header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid via header value information.</exception>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		public static ViaHeaderValue Parse(string input)
		{
			ViaHeaderValue viaHeaderValue;
			if (ViaHeaderValue.TryParse(input, out viaHeaderValue))
			{
				return viaHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> version of the string.</param>
		// Token: 0x060003D5 RID: 981 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public static bool TryParse(string input, out ViaHeaderValue parsedValue)
		{
			Token token;
			if (ViaHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D224 File Offset: 0x0000B424
		internal static bool TryParse(string input, int minimalCount, out List<ViaHeaderValue> result)
		{
			return CollectionParser.TryParse<ViaHeaderValue>(input, minimalCount, new ElementTryParser<ViaHeaderValue>(ViaHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D23C File Offset: 0x0000B43C
		private static bool TryParseElement(Lexer lexer, out ViaHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			Token token = lexer.Scan(false);
			ViaHeaderValue viaHeaderValue = new ViaHeaderValue();
			if (token == Token.Type.SeparatorSlash)
			{
				token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					return false;
				}
				viaHeaderValue.ProtocolName = lexer.GetStringValue(t);
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(token);
				token = lexer.Scan(false);
			}
			else
			{
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(t);
			}
			if (token != Token.Type.Token)
			{
				return false;
			}
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				t = lexer.Scan(false);
				if (t != Token.Type.Token)
				{
					return false;
				}
			}
			else
			{
				t = token;
			}
			viaHeaderValue.ReceivedBy = lexer.GetStringValue(token, t);
			string text;
			if (lexer.ScanCommentOptional(out text, out t))
			{
				t = lexer.Scan(false);
			}
			viaHeaderValue.Comment = text;
			parsedValue = viaHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000D344 File Offset: 0x0000B544
		public override string ToString()
		{
			string text = ((this.ProtocolName != null) ? string.Concat(new string[] { this.ProtocolName, "/", this.ProtocolVersion, " ", this.ReceivedBy }) : (this.ProtocolVersion + " " + this.ReceivedBy));
			if (this.Comment == null)
			{
				return text;
			}
			return text + " " + this.Comment;
		}
	}
}
