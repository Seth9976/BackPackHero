using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000FD RID: 253
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x00049263 File Offset: 0x00047463
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0004926B File Offset: 0x0004746B
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0004927C File Offset: 0x0004747C
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000492A0 File Offset: 0x000474A0
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000492AD File Offset: 0x000474AD
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000492BB File Offset: 0x000474BB
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000492C8 File Offset: 0x000474C8
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000492DA File Offset: 0x000474DA
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000492EA File Offset: 0x000474EA
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000492F7 File Offset: 0x000474F7
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00049304 File Offset: 0x00047504
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00049314 File Offset: 0x00047514
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00049321 File Offset: 0x00047521
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0004932F File Offset: 0x0004752F
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0004933D File Offset: 0x0004753D
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0004934C File Offset: 0x0004754C
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0004935A File Offset: 0x0004755A
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00049368 File Offset: 0x00047568
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00049376 File Offset: 0x00047576
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00049384 File Offset: 0x00047584
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00049393 File Offset: 0x00047593
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000493A3 File Offset: 0x000475A3
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000493B3 File Offset: 0x000475B3
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000493C1 File Offset: 0x000475C1
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000493D1 File Offset: 0x000475D1
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x000493E1 File Offset: 0x000475E1
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000493EE File Offset: 0x000475EE
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000493FB File Offset: 0x000475FB
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00049408 File Offset: 0x00047608
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00049416 File Offset: 0x00047616
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00049425 File Offset: 0x00047625
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00049433 File Offset: 0x00047633
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00049440 File Offset: 0x00047640
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0004944D File Offset: 0x0004764D
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x04000980 RID: 2432
		private XmlWriter _xmltextWriter;
	}
}
