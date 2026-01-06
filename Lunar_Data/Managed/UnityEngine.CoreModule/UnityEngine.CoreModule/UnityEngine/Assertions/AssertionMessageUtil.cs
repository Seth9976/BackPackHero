using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000485 RID: 1157
	internal class AssertionMessageUtil
	{
		// Token: 0x06002900 RID: 10496 RVA: 0x00043B18 File Offset: 0x00041D18
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[] { "Assertion failure.", failureMessage });
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00043B48 File Offset: 0x00041D48
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00043B88 File Offset: 0x00041D88
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[] { expectEqual ? "not " : "" }), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				expectEqual ? "==" : "!="
			}));
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00043BEC File Offset: 0x00041DEC
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[] { expectNull ? "not " : "" }), UnityString.Format("Value was {0}Null", new object[] { expectNull ? "" : "not " }));
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00043C48 File Offset: 0x00041E48
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + (!expected).ToString(), expected.ToString());
		}

		// Token: 0x04000F95 RID: 3989
		private const string k_Expected = "Expected:";

		// Token: 0x04000F96 RID: 3990
		private const string k_AssertionFailed = "Assertion failure.";
	}
}
