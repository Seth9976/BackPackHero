using System;

namespace System
{
	/// <summary>A customizable parser for a hierarchical URI.</summary>
	// Token: 0x0200014C RID: 332
	public class GenericUriParser : UriParser
	{
		/// <summary>Create a customizable parser for a hierarchical URI.</summary>
		/// <param name="options">Specify the options for this <see cref="T:System.GenericUriParser" />.</param>
		// Token: 0x060008DB RID: 2267 RVA: 0x00020A83 File Offset: 0x0001EC83
		public GenericUriParser(GenericUriParserOptions options)
			: base(GenericUriParser.MapGenericParserOptions(options))
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00020A94 File Offset: 0x0001EC94
		private static UriSyntaxFlags MapGenericParserOptions(GenericUriParserOptions options)
		{
			UriSyntaxFlags uriSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes;
			if ((options & GenericUriParserOptions.GenericAuthority) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~(UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host);
				uriSyntaxFlags |= UriSyntaxFlags.AllowAnyOtherHost;
			}
			if ((options & GenericUriParserOptions.AllowEmptyAuthority) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags |= UriSyntaxFlags.AllowEmptyHost;
			}
			if ((options & GenericUriParserOptions.NoUserInfo) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.MayHaveUserInfo;
			}
			if ((options & GenericUriParserOptions.NoPort) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.MayHavePort;
			}
			if ((options & GenericUriParserOptions.NoQuery) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.MayHaveQuery;
			}
			if ((options & GenericUriParserOptions.NoFragment) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.MayHaveFragment;
			}
			if ((options & GenericUriParserOptions.DontConvertPathBackslashes) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.ConvertPathSlashes;
			}
			if ((options & GenericUriParserOptions.DontCompressPath) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~(UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath);
			}
			if ((options & GenericUriParserOptions.DontUnescapePathDotsAndSlashes) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags &= ~UriSyntaxFlags.UnEscapeDotsAndSlashes;
			}
			if ((options & GenericUriParserOptions.Idn) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags |= UriSyntaxFlags.AllowIdn;
			}
			if ((options & GenericUriParserOptions.IriParsing) != GenericUriParserOptions.Default)
			{
				uriSyntaxFlags |= UriSyntaxFlags.AllowIriParsing;
			}
			return uriSyntaxFlags;
		}

		// Token: 0x0400058C RID: 1420
		private const UriSyntaxFlags DefaultGenericUriParserFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes;
	}
}
