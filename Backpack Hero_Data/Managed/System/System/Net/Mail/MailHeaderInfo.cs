using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x0200062D RID: 1581
	internal static class MailHeaderInfo
	{
		// Token: 0x060032A0 RID: 12960 RVA: 0x000B5EB4 File Offset: 0x000B40B4
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.s_headerInfo.Length; i++)
			{
				MailHeaderInfo.s_headerDictionary.Add(MailHeaderInfo.s_headerInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000B61F0 File Offset: 0x000B43F0
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.s_headerInfo[(int)id].NormalizedName;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000B6210 File Offset: 0x000B4410
		internal static MailHeaderID GetID(string name)
		{
			int num;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num))
			{
				return MailHeaderID.Unknown;
			}
			return (MailHeaderID)num;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000B6230 File Offset: 0x000B4430
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].IsUserSettable;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000B6260 File Offset: 0x000B4460
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) && MailHeaderInfo.s_headerInfo[num].IsSingleton;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000B6290 File Offset: 0x000B4490
		internal static string NormalizeCase(string name)
		{
			int num;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num))
			{
				return name;
			}
			return MailHeaderInfo.s_headerInfo[num].NormalizedName;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000B62C0 File Offset: 0x000B44C0
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].AllowsUnicode;
		}

		// Token: 0x04001EFC RID: 7932
		private static readonly MailHeaderInfo.HeaderInfo[] s_headerInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x04001EFD RID: 7933
		private static readonly Dictionary<string, int> s_headerDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200062E RID: 1582
		private readonly struct HeaderInfo
		{
			// Token: 0x060032A7 RID: 12967 RVA: 0x000B62EE File Offset: 0x000B44EE
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x04001EFE RID: 7934
			public readonly string NormalizedName;

			// Token: 0x04001EFF RID: 7935
			public readonly bool IsSingleton;

			// Token: 0x04001F00 RID: 7936
			public readonly MailHeaderID ID;

			// Token: 0x04001F01 RID: 7937
			public readonly bool IsUserSettable;

			// Token: 0x04001F02 RID: 7938
			public readonly bool AllowsUnicode;
		}
	}
}
