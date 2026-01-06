using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the W3C XML Schema Definition Language (XSD) schema types.</summary>
	// Token: 0x020005EE RID: 1518
	public enum XmlTypeCode
	{
		/// <summary>No type information.</summary>
		// Token: 0x04002C36 RID: 11318
		None,
		/// <summary>An item such as a node or atomic value.</summary>
		// Token: 0x04002C37 RID: 11319
		Item,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C38 RID: 11320
		Node,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C39 RID: 11321
		Document,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3A RID: 11322
		Element,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3B RID: 11323
		Attribute,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3C RID: 11324
		Namespace,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3D RID: 11325
		ProcessingInstruction,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3E RID: 11326
		Comment,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C3F RID: 11327
		Text,
		/// <summary>Any atomic value of a union.</summary>
		// Token: 0x04002C40 RID: 11328
		AnyAtomicType,
		/// <summary>An untyped atomic value.</summary>
		// Token: 0x04002C41 RID: 11329
		UntypedAtomic,
		/// <summary>A W3C XML Schema xs:string type.</summary>
		// Token: 0x04002C42 RID: 11330
		String,
		/// <summary>A W3C XML Schema xs:boolean type.</summary>
		// Token: 0x04002C43 RID: 11331
		Boolean,
		/// <summary>A W3C XML Schema xs:decimal type.</summary>
		// Token: 0x04002C44 RID: 11332
		Decimal,
		/// <summary>A W3C XML Schema xs:float type.</summary>
		// Token: 0x04002C45 RID: 11333
		Float,
		/// <summary>A W3C XML Schema xs:double type.</summary>
		// Token: 0x04002C46 RID: 11334
		Double,
		/// <summary>A W3C XML Schema xs:Duration type.</summary>
		// Token: 0x04002C47 RID: 11335
		Duration,
		/// <summary>A W3C XML Schema xs:dateTime type.</summary>
		// Token: 0x04002C48 RID: 11336
		DateTime,
		/// <summary>A W3C XML Schema xs:time type.</summary>
		// Token: 0x04002C49 RID: 11337
		Time,
		/// <summary>A W3C XML Schema xs:date type.</summary>
		// Token: 0x04002C4A RID: 11338
		Date,
		/// <summary>A W3C XML Schema xs:gYearMonth type.</summary>
		// Token: 0x04002C4B RID: 11339
		GYearMonth,
		/// <summary>A W3C XML Schema xs:gYear type.</summary>
		// Token: 0x04002C4C RID: 11340
		GYear,
		/// <summary>A W3C XML Schema xs:gMonthDay type.</summary>
		// Token: 0x04002C4D RID: 11341
		GMonthDay,
		/// <summary>A W3C XML Schema xs:gDay type.</summary>
		// Token: 0x04002C4E RID: 11342
		GDay,
		/// <summary>A W3C XML Schema xs:gMonth type.</summary>
		// Token: 0x04002C4F RID: 11343
		GMonth,
		/// <summary>A W3C XML Schema xs:hexBinary type.</summary>
		// Token: 0x04002C50 RID: 11344
		HexBinary,
		/// <summary>A W3C XML Schema xs:base64Binary type.</summary>
		// Token: 0x04002C51 RID: 11345
		Base64Binary,
		/// <summary>A W3C XML Schema xs:anyURI type.</summary>
		// Token: 0x04002C52 RID: 11346
		AnyUri,
		/// <summary>A W3C XML Schema xs:QName type.</summary>
		// Token: 0x04002C53 RID: 11347
		QName,
		/// <summary>A W3C XML Schema xs:NOTATION type.</summary>
		// Token: 0x04002C54 RID: 11348
		Notation,
		/// <summary>A W3C XML Schema xs:normalizedString type.</summary>
		// Token: 0x04002C55 RID: 11349
		NormalizedString,
		/// <summary>A W3C XML Schema xs:token type.</summary>
		// Token: 0x04002C56 RID: 11350
		Token,
		/// <summary>A W3C XML Schema xs:language type.</summary>
		// Token: 0x04002C57 RID: 11351
		Language,
		/// <summary>A W3C XML Schema xs:NMTOKEN type.</summary>
		// Token: 0x04002C58 RID: 11352
		NmToken,
		/// <summary>A W3C XML Schema xs:Name type.</summary>
		// Token: 0x04002C59 RID: 11353
		Name,
		/// <summary>A W3C XML Schema xs:NCName type.</summary>
		// Token: 0x04002C5A RID: 11354
		NCName,
		/// <summary>A W3C XML Schema xs:ID type.</summary>
		// Token: 0x04002C5B RID: 11355
		Id,
		/// <summary>A W3C XML Schema xs:IDREF type.</summary>
		// Token: 0x04002C5C RID: 11356
		Idref,
		/// <summary>A W3C XML Schema xs:ENTITY type.</summary>
		// Token: 0x04002C5D RID: 11357
		Entity,
		/// <summary>A W3C XML Schema xs:integer type.</summary>
		// Token: 0x04002C5E RID: 11358
		Integer,
		/// <summary>A W3C XML Schema xs:nonPositiveInteger type.</summary>
		// Token: 0x04002C5F RID: 11359
		NonPositiveInteger,
		/// <summary>A W3C XML Schema xs:negativeInteger type.</summary>
		// Token: 0x04002C60 RID: 11360
		NegativeInteger,
		/// <summary>A W3C XML Schema xs:long type.</summary>
		// Token: 0x04002C61 RID: 11361
		Long,
		/// <summary>A W3C XML Schema xs:int type.</summary>
		// Token: 0x04002C62 RID: 11362
		Int,
		/// <summary>A W3C XML Schema xs:short type.</summary>
		// Token: 0x04002C63 RID: 11363
		Short,
		/// <summary>A W3C XML Schema xs:byte type.</summary>
		// Token: 0x04002C64 RID: 11364
		Byte,
		/// <summary>A W3C XML Schema xs:nonNegativeInteger type.</summary>
		// Token: 0x04002C65 RID: 11365
		NonNegativeInteger,
		/// <summary>A W3C XML Schema xs:unsignedLong type.</summary>
		// Token: 0x04002C66 RID: 11366
		UnsignedLong,
		/// <summary>A W3C XML Schema xs:unsignedInt type.</summary>
		// Token: 0x04002C67 RID: 11367
		UnsignedInt,
		/// <summary>A W3C XML Schema xs:unsignedShort type.</summary>
		// Token: 0x04002C68 RID: 11368
		UnsignedShort,
		/// <summary>A W3C XML Schema xs:unsignedByte type.</summary>
		// Token: 0x04002C69 RID: 11369
		UnsignedByte,
		/// <summary>A W3C XML Schema xs:positiveInteger type.</summary>
		// Token: 0x04002C6A RID: 11370
		PositiveInteger,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C6B RID: 11371
		YearMonthDuration,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04002C6C RID: 11372
		DayTimeDuration
	}
}
