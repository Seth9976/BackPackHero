using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Unity.VisualScripting
{
	// Token: 0x0200004A RID: 74
	public class EnsureThat
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00004F9C File Offset: 0x0000319C
		public void IsTrue(bool value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!value)
			{
				throw new ArgumentException(ExceptionMessages.Booleans_IsTrueFailed, this.paramName);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004FBA File Offset: 0x000031BA
		public void IsFalse(bool value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value)
			{
				throw new ArgumentException(ExceptionMessages.Booleans_IsFalseFailed, this.paramName);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004FD8 File Offset: 0x000031D8
		public void HasItems<T>(T value) where T : class, ICollection
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull<T>(value);
			if (value.Count < 1)
			{
				throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, this.paramName);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005008 File Offset: 0x00003208
		public void HasItems<T>(ICollection<T> value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull<ICollection<T>>(value);
			if (value.Count < 1)
			{
				throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, this.paramName);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005033 File Offset: 0x00003233
		public void HasItems<T>(T[] value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull<T[]>(value);
			if (value.Length < 1)
			{
				throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, this.paramName);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000505C File Offset: 0x0000325C
		public void HasNoNullItem<T>(T value) where T : class, IEnumerable
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull<T>(value);
			using (IEnumerator enumerator = value.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw new ArgumentException(ExceptionMessages.Collections_HasNoNullItemFailed, this.paramName);
					}
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000050CC File Offset: 0x000032CC
		public void HasItems<T>(IList<T> value)
		{
			this.HasItems<T>(value);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000050D5 File Offset: 0x000032D5
		public void HasItems<TKey, TValue>(IDictionary<TKey, TValue> value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull<IDictionary<TKey, TValue>>(value);
			if (value.Count < 1)
			{
				throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, this.paramName);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005100 File Offset: 0x00003300
		public void SizeIs<T>(T[] value, int expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value.Length != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Length }), this.paramName);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005140 File Offset: 0x00003340
		public void SizeIs<T>(T[] value, long expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if ((long)value.Length != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Length }), this.paramName);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000518C File Offset: 0x0000338C
		public void SizeIs<T>(T value, int expected) where T : ICollection
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000051EC File Offset: 0x000033EC
		public void SizeIs<T>(T value, long expected) where T : ICollection
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if ((long)value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000524C File Offset: 0x0000344C
		public void SizeIs<T>(ICollection<T> value, int expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000052A0 File Offset: 0x000034A0
		public void SizeIs<T>(ICollection<T> value, long expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if ((long)value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000052F2 File Offset: 0x000034F2
		public void SizeIs<T>(IList<T> value, int expected)
		{
			this.SizeIs<T>(value, expected);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000052FC File Offset: 0x000034FC
		public void SizeIs<T>(IList<T> value, long expected)
		{
			this.SizeIs<T>(value, expected);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005308 File Offset: 0x00003508
		public void SizeIs<TKey, TValue>(IDictionary<TKey, TValue> value, int expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000535C File Offset: 0x0000355C
		public void SizeIs<TKey, TValue>(IDictionary<TKey, TValue> value, long expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if ((long)value.Count != expected)
			{
				throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(new object[] { expected, value.Count }), this.paramName);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000053B0 File Offset: 0x000035B0
		public void IsKeyOf<TKey, TValue>(IDictionary<TKey, TValue> value, TKey expectedKey, string keyLabel = null)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!value.ContainsKey(expectedKey))
			{
				throw new ArgumentException(ExceptionMessages.Collections_ContainsKey_Failed.Inject(new object[]
				{
					expectedKey,
					keyLabel ?? this.paramName.Prettify()
				}), this.paramName);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005406 File Offset: 0x00003606
		public void Any<T>(IList<T> value, Func<T, bool> predicate)
		{
			this.Any<T>(value, predicate);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005410 File Offset: 0x00003610
		public void Any<T>(ICollection<T> value, Func<T, bool> predicate)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!value.Any(predicate))
			{
				throw new ArgumentException(ExceptionMessages.Collections_Any_Failed, this.paramName);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005434 File Offset: 0x00003634
		public void Any<T>(T[] value, Func<T, bool> predicate)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!value.Any(predicate))
			{
				throw new ArgumentException(ExceptionMessages.Collections_Any_Failed, this.paramName);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005458 File Offset: 0x00003658
		public void Is<T>(T param, T expected) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!param.IsEq(expected))
			{
				throw new ArgumentException(ExceptionMessages.Comp_Is_Failed.Inject(new object[] { param, expected }), this.paramName);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000054AC File Offset: 0x000036AC
		public void IsNot<T>(T param, T expected) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (param.IsEq(expected))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNot_Failed.Inject(new object[] { param, expected }), this.paramName);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005500 File Offset: 0x00003700
		public void IsLt<T>(T param, T limit) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!param.IsLt(limit))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotLt.Inject(new object[] { param, limit }), this.paramName);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00005554 File Offset: 0x00003754
		public void IsLte<T>(T param, T limit) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (param.IsGt(limit))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotLte.Inject(new object[] { param, limit }), this.paramName);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000055A8 File Offset: 0x000037A8
		public void IsGt<T>(T param, T limit) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!param.IsGt(limit))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotGt.Inject(new object[] { param, limit }), this.paramName);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000055FC File Offset: 0x000037FC
		public void IsGte<T>(T param, T limit) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (param.IsLt(limit))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotGte.Inject(new object[] { param, limit }), this.paramName);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005650 File Offset: 0x00003850
		public void IsInRange<T>(T param, T min, T max) where T : struct, IComparable<T>
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (param.IsLt(min))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotInRange_ToLow.Inject(new object[] { param, min }), this.paramName);
			}
			if (param.IsGt(max))
			{
				throw new ArgumentException(ExceptionMessages.Comp_IsNotInRange_ToHigh.Inject(new object[] { param, max }), this.paramName);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000056DD File Offset: 0x000038DD
		public void IsNotEmpty(Guid value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value.Equals(Guid.Empty))
			{
				throw new ArgumentException(ExceptionMessages.Guids_IsNotEmpty_Failed, this.paramName);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005706 File Offset: 0x00003906
		public void IsNotNull<T>(T? value) where T : struct
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException(this.paramName, ExceptionMessages.Common_IsNotNull_Failed);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000572A File Offset: 0x0000392A
		public void IsNull<T>([NoEnumeration] T value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value != null)
			{
				throw new ArgumentNullException(this.paramName, ExceptionMessages.Common_IsNull_Failed);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000574D File Offset: 0x0000394D
		public void IsNotNull<T>([NoEnumeration] T value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException(this.paramName, ExceptionMessages.Common_IsNotNull_Failed);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005770 File Offset: 0x00003970
		public void HasAttribute(Type param, Type attributeType)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!param.HasAttribute(attributeType, true))
			{
				throw new ArgumentException(ExceptionMessages.Reflection_HasAttribute_Failed.Inject(new string[]
				{
					param.ToString(),
					attributeType.ToString()
				}), this.paramName);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000057BD File Offset: 0x000039BD
		public void HasAttribute<TAttribute>(Type param) where TAttribute : Attribute
		{
			this.HasAttribute(param, typeof(TAttribute));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000057D0 File Offset: 0x000039D0
		private void HasConstructorAccepting(Type param, Type[] parameterTypes, bool nonPublic)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (param.GetConstructorAccepting(parameterTypes, nonPublic) == null)
			{
				throw new ArgumentException((nonPublic ? ExceptionMessages.Reflection_HasConstructor_Failed : ExceptionMessages.Reflection_HasPublicConstructor_Failed).Inject(new string[]
				{
					param.ToString(),
					parameterTypes.ToCommaSeparatedString()
				}), this.paramName);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000582D File Offset: 0x00003A2D
		public void HasConstructorAccepting(Type param, params Type[] parameterTypes)
		{
			this.HasConstructorAccepting(param, parameterTypes, true);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005838 File Offset: 0x00003A38
		public void HasPublicConstructorAccepting(Type param, params Type[] parameterTypes)
		{
			this.HasConstructorAccepting(param, parameterTypes, false);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005843 File Offset: 0x00003A43
		public void IsNotNullOrWhiteSpace(string value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull(value);
			if (StringUtility.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsNotNullOrWhiteSpace_Failed, this.paramName);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000586D File Offset: 0x00003A6D
		public void IsNotNullOrEmpty(string value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull(value);
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsNotNullOrEmpty_Failed, this.paramName);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005897 File Offset: 0x00003A97
		public void IsNotNull(string value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException(this.paramName, ExceptionMessages.Common_IsNotNull_Failed);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000058B5 File Offset: 0x00003AB5
		public void IsNotEmpty(string value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (string.Empty.Equals(value))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsNotEmpty_Failed, this.paramName);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000058E0 File Offset: 0x00003AE0
		public void HasLengthBetween(string value, int minLength, int maxLength)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull(value);
			int length = value.Length;
			if (length < minLength)
			{
				throw new ArgumentException(ExceptionMessages.Strings_HasLengthBetween_Failed_ToShort.Inject(new object[] { minLength, maxLength, length }), this.paramName);
			}
			if (length > maxLength)
			{
				throw new ArgumentException(ExceptionMessages.Strings_HasLengthBetween_Failed_ToLong.Inject(new object[] { minLength, maxLength, length }), this.paramName);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00005979 File Offset: 0x00003B79
		public void Matches(string value, string match)
		{
			this.Matches(value, new Regex(match));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005988 File Offset: 0x00003B88
		public void Matches(string value, Regex match)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!match.IsMatch(value))
			{
				throw new ArgumentException(ExceptionMessages.Strings_Matches_Failed.Inject(new object[] { value, match }), this.paramName);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000059C0 File Offset: 0x00003BC0
		public void SizeIs(string value, int expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			this.IsNotNull(value);
			if (value.Length != expected)
			{
				throw new ArgumentException(ExceptionMessages.Strings_SizeIs_Failed.Inject(new object[] { expected, value.Length }), this.paramName);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00005A18 File Offset: 0x00003C18
		public void IsEqualTo(string value, string expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!this.StringEquals(value, expected, null))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsEqualTo_Failed.Inject(new string[] { value, expected }), this.paramName);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00005A64 File Offset: 0x00003C64
		public void IsEqualTo(string value, string expected, StringComparison comparison)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!this.StringEquals(value, expected, new StringComparison?(comparison)))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsEqualTo_Failed.Inject(new string[] { value, expected }), this.paramName);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public void IsNotEqualTo(string value, string expected)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (this.StringEquals(value, expected, null))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(new string[] { value, expected }), this.paramName);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public void IsNotEqualTo(string value, string expected, StringComparison comparison)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (this.StringEquals(value, expected, new StringComparison?(comparison)))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(new string[] { value, expected }), this.paramName);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005B2E File Offset: 0x00003D2E
		public void IsGuid(string value)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!StringUtility.IsGuid(value))
			{
				throw new ArgumentException(ExceptionMessages.Strings_IsGuid_Failed.Inject(new string[] { value }), this.paramName);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005B60 File Offset: 0x00003D60
		private bool StringEquals(string x, string y, StringComparison? comparison = null)
		{
			if (comparison == null)
			{
				return string.Equals(x, y);
			}
			return string.Equals(x, y, comparison.Value);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00005B84 File Offset: 0x00003D84
		public void IsOfType<T>(T param, Type expectedType)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!expectedType.IsAssignableFrom(param))
			{
				throw new ArgumentException(ExceptionMessages.Types_IsOfType_Failed.Inject(new string[]
				{
					expectedType.ToString(),
					((param != null) ? param.GetType().ToString() : null) ?? "null"
				}), this.paramName);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public void IsOfType(Type param, Type expectedType)
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			if (!expectedType.IsAssignableFrom(param))
			{
				throw new ArgumentException(ExceptionMessages.Types_IsOfType_Failed.Inject(new string[]
				{
					expectedType.ToString(),
					param.ToString()
				}), this.paramName);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005C44 File Offset: 0x00003E44
		public void IsOfType<T>(object param)
		{
			this.IsOfType<object>(param, typeof(T));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00005C57 File Offset: 0x00003E57
		public void IsOfType<T>(Type param)
		{
			this.IsOfType(param, typeof(T));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005C6C File Offset: 0x00003E6C
		public void IsNotDefault<T>(T param) where T : struct
		{
			if (!Ensure.IsActive)
			{
				return;
			}
			T t = default(T);
			if (t.Equals(param))
			{
				throw new ArgumentException(ExceptionMessages.ValueTypes_IsNotDefault_Failed, this.paramName);
			}
		}

		// Token: 0x0400004C RID: 76
		internal string paramName;
	}
}
