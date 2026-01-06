using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents authentication information in Authorization, ProxyAuthorization, WWW-Authenticate, and Proxy-Authenticate header values.</summary>
	// Token: 0x02000033 RID: 51
	public class AuthenticationHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x000069A2 File Offset: 0x00004BA2
		public AuthenticationHeaderValue(string scheme)
			: this(scheme, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
		// Token: 0x06000191 RID: 401 RVA: 0x000069AC File Offset: 0x00004BAC
		public AuthenticationHeaderValue(string scheme, string parameter)
		{
			Parser.Token.Check(scheme);
			this.Scheme = scheme;
			this.Parameter = parameter;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00002300 File Offset: 0x00000500
		private AuthenticationHeaderValue()
		{
		}

		/// <summary>Gets the credentials containing the authentication information of the user agent for the resource being requested.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The credentials containing the authentication information.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000069C8 File Offset: 0x00004BC8
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000069D0 File Offset: 0x00004BD0
		public string Parameter { get; private set; }

		/// <summary>Gets the scheme to use for authorization.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The scheme to use for authorization.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000069D9 File Offset: 0x00004BD9
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000069E1 File Offset: 0x00004BE1
		public string Scheme { get; private set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000197 RID: 407 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object. </param>
		// Token: 0x06000198 RID: 408 RVA: 0x000069F4 File Offset: 0x00004BF4
		public override bool Equals(object obj)
		{
			AuthenticationHeaderValue authenticationHeaderValue = obj as AuthenticationHeaderValue;
			return authenticationHeaderValue != null && string.Equals(authenticationHeaderValue.Scheme, this.Scheme, StringComparison.OrdinalIgnoreCase) && authenticationHeaderValue.Parameter == this.Parameter;
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000199 RID: 409 RVA: 0x00006A34 File Offset: 0x00004C34
		public override int GetHashCode()
		{
			int num = this.Scheme.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.Parameter))
			{
				num ^= this.Parameter.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" />.An <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		// Token: 0x0600019A RID: 410 RVA: 0x00006A74 File Offset: 0x00004C74
		public static AuthenticationHeaderValue Parse(string input)
		{
			AuthenticationHeaderValue authenticationHeaderValue;
			if (AuthenticationHeaderValue.TryParse(input, out authenticationHeaderValue))
			{
				return authenticationHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> version of the string.</param>
		// Token: 0x0600019B RID: 411 RVA: 0x00006A94 File Offset: 0x00004C94
		public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
		{
			Token token;
			if (AuthenticationHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006AC0 File Offset: 0x00004CC0
		internal static bool TryParse(string input, int minimalCount, out List<AuthenticationHeaderValue> result)
		{
			return CollectionParser.TryParse<AuthenticationHeaderValue>(input, minimalCount, new ElementTryParser<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParseElement), out result);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006AD8 File Offset: 0x00004CD8
		private static bool TryParseElement(Lexer lexer, out AuthenticationHeaderValue parsedValue, out Token t)
		{
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = new AuthenticationHeaderValue();
			parsedValue.Scheme = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.Token)
			{
				parsedValue.Parameter = lexer.GetRemainingStringValue(t.StartPosition);
				t = new Token(Token.Type.End, 0, 0);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x0600019E RID: 414 RVA: 0x00006B5C File Offset: 0x00004D5C
		public override string ToString()
		{
			if (this.Parameter == null)
			{
				return this.Scheme;
			}
			return this.Scheme + " " + this.Parameter;
		}
	}
}
