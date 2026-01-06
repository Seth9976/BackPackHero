using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information about the format of date and time values.</summary>
	// Token: 0x0200095B RID: 2395
	[Serializable]
	public sealed class DateTimeFormatInfo : IFormatProvider, ICloneable
	{
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0011ACF9 File Offset: 0x00118EF9
		private string CultureName
		{
			get
			{
				if (this._name == null)
				{
					this._name = this._cultureData.CultureName;
				}
				return this._name;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x0011AD1A File Offset: 0x00118F1A
		private CultureInfo Culture
		{
			get
			{
				if (this._cultureInfo == null)
				{
					this._cultureInfo = CultureInfo.GetCultureInfo(this.CultureName);
				}
				return this._cultureInfo;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x0011AD3B File Offset: 0x00118F3B
		private string LanguageName
		{
			get
			{
				if (this._langName == null)
				{
					this._langName = this._cultureData.SISO639LANGNAME;
				}
				return this._langName;
			}
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0011AD5C File Offset: 0x00118F5C
		private string[] internalGetAbbreviatedDayOfWeekNames()
		{
			return this.abbreviatedDayNames ?? this.internalGetAbbreviatedDayOfWeekNamesCore();
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0011AD6E File Offset: 0x00118F6E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedDayOfWeekNamesCore()
		{
			this.abbreviatedDayNames = this._cultureData.AbbreviatedDayNames(this.Calendar.ID);
			return this.abbreviatedDayNames;
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0011AD92 File Offset: 0x00118F92
		private string[] internalGetSuperShortDayNames()
		{
			return this.m_superShortDayNames ?? this.internalGetSuperShortDayNamesCore();
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0011ADA4 File Offset: 0x00118FA4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetSuperShortDayNamesCore()
		{
			this.m_superShortDayNames = this._cultureData.SuperShortDayNames(this.Calendar.ID);
			return this.m_superShortDayNames;
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0011ADC8 File Offset: 0x00118FC8
		private string[] internalGetDayOfWeekNames()
		{
			return this.dayNames ?? this.internalGetDayOfWeekNamesCore();
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0011ADDA File Offset: 0x00118FDA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetDayOfWeekNamesCore()
		{
			this.dayNames = this._cultureData.DayNames(this.Calendar.ID);
			return this.dayNames;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0011ADFE File Offset: 0x00118FFE
		private string[] internalGetAbbreviatedMonthNames()
		{
			return this.abbreviatedMonthNames ?? this.internalGetAbbreviatedMonthNamesCore();
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0011AE10 File Offset: 0x00119010
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedMonthNamesCore()
		{
			this.abbreviatedMonthNames = this._cultureData.AbbreviatedMonthNames(this.Calendar.ID);
			return this.abbreviatedMonthNames;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0011AE34 File Offset: 0x00119034
		private string[] internalGetMonthNames()
		{
			return this.monthNames ?? this.internalGetMonthNamesCore();
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0011AE46 File Offset: 0x00119046
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetMonthNamesCore()
		{
			this.monthNames = this._cultureData.MonthNames(this.Calendar.ID);
			return this.monthNames;
		}

		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x060054B5 RID: 21685 RVA: 0x0011AE6C File Offset: 0x0011906C
		public DateTimeFormatInfo()
		{
			this._cultureData = CultureInfo.InvariantCulture._cultureData;
			this.calendar = GregorianCalendar.GetDefaultInstance();
			this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0011AEC6 File Offset: 0x001190C6
		internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
		{
			this._cultureData = cultureData;
			this.Calendar = cal;
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0011AEF4 File Offset: 0x001190F4
		private void InitializeOverridableProperties(CultureData cultureData, int calendarId)
		{
			if (this.firstDayOfWeek == -1)
			{
				this.firstDayOfWeek = cultureData.IFIRSTDAYOFWEEK;
			}
			if (this.calendarWeekRule == -1)
			{
				this.calendarWeekRule = cultureData.IFIRSTWEEKOFYEAR;
			}
			if (this.amDesignator == null)
			{
				this.amDesignator = cultureData.SAM1159;
			}
			if (this.pmDesignator == null)
			{
				this.pmDesignator = cultureData.SPM2359;
			}
			if (this.timeSeparator == null)
			{
				this.timeSeparator = cultureData.TimeSeparator;
			}
			if (this.dateSeparator == null)
			{
				this.dateSeparator = cultureData.DateSeparator(calendarId);
			}
			this.allLongTimePatterns = this._cultureData.LongTimes;
			this.allShortTimePatterns = this._cultureData.ShortTimes;
			this.allLongDatePatterns = cultureData.LongDates(calendarId);
			this.allShortDatePatterns = cultureData.ShortDates(calendarId);
			this.allYearMonthPatterns = cultureData.YearMonths(calendarId);
		}

		/// <summary>Gets the default read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only object that is culture-independent (invariant).</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x0011AFC5 File Offset: 0x001191C5
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.s_invariantInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
					dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
					dateTimeFormatInfo._isReadOnly = true;
					DateTimeFormatInfo.s_invariantInfo = dateTimeFormatInfo;
				}
				return DateTimeFormatInfo.s_invariantInfo;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object based on the <see cref="T:System.Globalization.CultureInfo" /> object for the current thread.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0011AFF8 File Offset: 0x001191F8
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (!currentCulture._isInherited)
				{
					DateTimeFormatInfo dateTimeInfo = currentCulture.dateTimeInfo;
					if (dateTimeInfo != null)
					{
						return dateTimeInfo;
					}
				}
				return (DateTimeFormatInfo)currentCulture.GetFormat(typeof(DateTimeFormatInfo));
			}
		}

		/// <summary>Returns the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> that gets the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.-or- null to get <see cref="P:System.Globalization.DateTimeFormatInfo.CurrentInfo" />. </param>
		// Token: 0x060054BA RID: 21690 RVA: 0x0011B038 File Offset: 0x00119238
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			if (provider == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo._isInherited)
			{
				return cultureInfo.DateTimeFormat;
			}
			DateTimeFormatInfo dateTimeFormatInfo = provider as DateTimeFormatInfo;
			if (dateTimeFormatInfo != null)
			{
				return dateTimeFormatInfo;
			}
			DateTimeFormatInfo dateTimeFormatInfo2 = provider.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo;
			if (dateTimeFormatInfo2 == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			return dateTimeFormatInfo2;
		}

		/// <summary>Returns an object of the specified type that provides a date and time  formatting service.</summary>
		/// <returns>The current  object, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.DateTimeFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The type of the required formatting service. </param>
		// Token: 0x060054BB RID: 21691 RVA: 0x0011B093 File Offset: 0x00119293
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(DateTimeFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.DateTimeFormatInfo" /> object copied from the original <see cref="T:System.Globalization.DateTimeFormatInfo" />.</returns>
		// Token: 0x060054BC RID: 21692 RVA: 0x0011B0AA File Offset: 0x001192AA
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.calendar = (Calendar)this.Calendar.Clone();
			dateTimeFormatInfo._isReadOnly = false;
			return dateTimeFormatInfo;
		}

		/// <summary>Gets or sets the string designator for hours that are "ante meridiem" (before noon).</summary>
		/// <returns>The string designator for hours that are ante meridiem. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "AM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x0011B0D4 File Offset: 0x001192D4
		// (set) Token: 0x060054BE RID: 21694 RVA: 0x0011B0F5 File Offset: 0x001192F5
		public string AMDesignator
		{
			get
			{
				if (this.amDesignator == null)
				{
					this.amDesignator = this._cultureData.SAM1159;
				}
				return this.amDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.amDesignator = value;
			}
		}

		/// <summary>Gets or sets the calendar to use for the current culture.</summary>
		/// <returns>The calendar to use for the current culture. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is a <see cref="T:System.Globalization.GregorianCalendar" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a <see cref="T:System.Globalization.Calendar" /> object that is not valid for the current culture. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x0011B12A File Offset: 0x0011932A
		// (set) Token: 0x060054C0 RID: 21696 RVA: 0x0011B134 File Offset: 0x00119334
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			set
			{
				if (GlobalizationMode.Invariant)
				{
					throw new PlatformNotSupportedException();
				}
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Object cannot be null.");
				}
				if (value == this.calendar)
				{
					return;
				}
				for (int i = 0; i < this.OptionalCalendars.Length; i++)
				{
					if (this.OptionalCalendars[i] == (CalendarId)value.ID)
					{
						if (this.calendar != null)
						{
							this.m_eraNames = null;
							this.m_abbrevEraNames = null;
							this.m_abbrevEnglishEraNames = null;
							this.monthDayPattern = null;
							this.dayNames = null;
							this.abbreviatedDayNames = null;
							this.m_superShortDayNames = null;
							this.monthNames = null;
							this.abbreviatedMonthNames = null;
							this.genitiveMonthNames = null;
							this.m_genitiveAbbreviatedMonthNames = null;
							this.leapYearMonthNames = null;
							this.formatFlags = DateTimeFormatFlags.NotInitialized;
							this.allShortDatePatterns = null;
							this.allLongDatePatterns = null;
							this.allYearMonthPatterns = null;
							this.dateTimeOffsetPattern = null;
							this.longDatePattern = null;
							this.shortDatePattern = null;
							this.yearMonthPattern = null;
							this.fullDateTimePattern = null;
							this.generalShortTimePattern = null;
							this.generalLongTimePattern = null;
							this.dateSeparator = null;
							this.ClearTokenHashTable();
						}
						this.calendar = value;
						this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value", "Not a valid calendar for the given culture.");
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x0011B292 File Offset: 0x00119492
		private CalendarId[] OptionalCalendars
		{
			get
			{
				if (this.optionalCalendars == null)
				{
					this.optionalCalendars = this._cultureData.GetCalendarIds();
				}
				return this.optionalCalendars;
			}
		}

		/// <summary>Returns the integer representing the specified era.</summary>
		/// <returns>The integer representing the era, if <paramref name="eraName" /> is valid; otherwise, -1.</returns>
		/// <param name="eraName">The string containing the name of the era. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eraName" /> is null. </exception>
		// Token: 0x060054C2 RID: 21698 RVA: 0x0011B2B4 File Offset: 0x001194B4
		public int GetEra(string eraName)
		{
			if (eraName == null)
			{
				throw new ArgumentNullException("eraName", "String reference not set to an instance of a String.");
			}
			if (eraName.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.EraNames.Length; i++)
			{
				if (this.m_eraNames[i].Length > 0 && this.Culture.CompareInfo.Compare(eraName, this.m_eraNames[i], CompareOptions.IgnoreCase) == 0)
				{
					return i + 1;
				}
			}
			for (int j = 0; j < this.AbbreviatedEraNames.Length; j++)
			{
				if (this.Culture.CompareInfo.Compare(eraName, this.m_abbrevEraNames[j], CompareOptions.IgnoreCase) == 0)
				{
					return j + 1;
				}
			}
			for (int k = 0; k < this.AbbreviatedEnglishEraNames.Length; k++)
			{
				if (CompareInfo.Invariant.Compare(eraName, this.m_abbrevEnglishEraNames[k], CompareOptions.IgnoreCase) == 0)
				{
					return k + 1;
				}
			}
			return -1;
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x0011B382 File Offset: 0x00119582
		internal string[] EraNames
		{
			get
			{
				if (this.m_eraNames == null)
				{
					this.m_eraNames = this._cultureData.EraNames(this.Calendar.ID);
				}
				return this.m_eraNames;
			}
		}

		/// <summary>Returns the string containing the name of the specified era.</summary>
		/// <returns>A string containing the name of the era.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x060054C4 RID: 21700 RVA: 0x0011B3AE File Offset: 0x001195AE
		public string GetEraName(int era)
		{
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.EraNames.Length && era >= 0)
			{
				return this.m_eraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x0011B3EC File Offset: 0x001195EC
		internal string[] AbbreviatedEraNames
		{
			get
			{
				if (this.m_abbrevEraNames == null)
				{
					this.m_abbrevEraNames = this._cultureData.AbbrevEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEraNames;
			}
		}

		/// <summary>Returns the string containing the abbreviated name of the specified era, if an abbreviation exists.</summary>
		/// <returns>A string containing the abbreviated name of the specified era, if an abbreviation exists.-or- A string containing the full name of the era, if an abbreviation does not exist.</returns>
		/// <param name="era">The integer representing the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> does not represent a valid era in the calendar specified in the <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property. </exception>
		// Token: 0x060054C6 RID: 21702 RVA: 0x0011B418 File Offset: 0x00119618
		public string GetAbbreviatedEraName(int era)
		{
			if (this.AbbreviatedEraNames.Length == 0)
			{
				return this.GetEraName(era);
			}
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.m_abbrevEraNames.Length && era >= 0)
			{
				return this.m_abbrevEraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0011B472 File Offset: 0x00119672
		internal string[] AbbreviatedEnglishEraNames
		{
			get
			{
				if (this.m_abbrevEnglishEraNames == null)
				{
					this.m_abbrevEnglishEraNames = this._cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEnglishEraNames;
			}
		}

		/// <summary>Gets or sets the string that separates the components of a date, that is, the year, month, and day.</summary>
		/// <returns>The string that separates the components of a date, that is, the year, month, and day. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "/".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060054C8 RID: 21704 RVA: 0x0011B49E File Offset: 0x0011969E
		// (set) Token: 0x060054C9 RID: 21705 RVA: 0x0011B4CA File Offset: 0x001196CA
		public string DateSeparator
		{
			get
			{
				if (this.dateSeparator == null)
				{
					this.dateSeparator = this._cultureData.DateSeparator(this.Calendar.ID);
				}
				return this.dateSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.dateSeparator = value;
			}
		}

		/// <summary>Gets or sets the first day of the week.</summary>
		/// <returns>An enumeration value that represents the first day of the week. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.DayOfWeek.Sunday" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060054CA RID: 21706 RVA: 0x0011B4FF File Offset: 0x001196FF
		// (set) Token: 0x060054CB RID: 21707 RVA: 0x0011B524 File Offset: 0x00119724
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				if (this.firstDayOfWeek == -1)
				{
					this.firstDayOfWeek = this._cultureData.IFIRSTDAYOFWEEK;
				}
				return (DayOfWeek)this.firstDayOfWeek;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value >= DayOfWeek.Sunday && value <= DayOfWeek.Saturday)
				{
					this.firstDayOfWeek = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
		}

		/// <summary>Gets or sets a value that specifies which rule is used to determine the first calendar week of the year.</summary>
		/// <returns>A value that determines the first calendar week of the year. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.Globalization.CalendarWeekRule.FirstDay" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x0011B574 File Offset: 0x00119774
		// (set) Token: 0x060054CD RID: 21709 RVA: 0x0011B598 File Offset: 0x00119798
		public CalendarWeekRule CalendarWeekRule
		{
			get
			{
				if (this.calendarWeekRule == -1)
				{
					this.calendarWeekRule = this._cultureData.IFIRSTWEEKOFYEAR;
				}
				return (CalendarWeekRule)this.calendarWeekRule;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value >= CalendarWeekRule.FirstDay && value <= CalendarWeekRule.FirstFourDayWeek)
				{
					this.calendarWeekRule = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.Format("Valid values are between {0} and {1}, inclusive.", CalendarWeekRule.FirstDay, CalendarWeekRule.FirstFourDayWeek));
			}
		}

		/// <summary>Gets or sets the custom format string for a long date and long time value.</summary>
		/// <returns>The custom format string for a long date and long time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060054CE RID: 21710 RVA: 0x0011B5E8 File Offset: 0x001197E8
		// (set) Token: 0x060054CF RID: 21711 RVA: 0x0011B614 File Offset: 0x00119814
		public string FullDateTimePattern
		{
			get
			{
				if (this.fullDateTimePattern == null)
				{
					this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern;
				}
				return this.fullDateTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.fullDateTimePattern = value;
			}
		}

		/// <summary>Gets or sets the custom format string for a long date value.</summary>
		/// <returns>The custom format string for a long date value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x0011B643 File Offset: 0x00119843
		// (set) Token: 0x060054D1 RID: 21713 RVA: 0x0011B661 File Offset: 0x00119861
		public string LongDatePattern
		{
			get
			{
				if (this.longDatePattern == null)
				{
					this.longDatePattern = this.UnclonedLongDatePatterns[0];
				}
				return this.longDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.longDatePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
			}
		}

		/// <summary>Gets or sets the custom format string for a long time value.</summary>
		/// <returns>The format pattern for a long time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x0011B69D File Offset: 0x0011989D
		// (set) Token: 0x060054D3 RID: 21715 RVA: 0x0011B6BC File Offset: 0x001198BC
		public string LongTimePattern
		{
			get
			{
				if (this.longTimePattern == null)
				{
					this.longTimePattern = this.UnclonedLongTimePatterns[0];
				}
				return this.longTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.longTimePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
				this.generalLongTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		/// <summary>Gets or sets the custom format string for a month and day value.</summary>
		/// <returns>The custom format string for a month and day value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x0011B711 File Offset: 0x00119911
		// (set) Token: 0x060054D5 RID: 21717 RVA: 0x0011B73D File Offset: 0x0011993D
		public string MonthDayPattern
		{
			get
			{
				if (this.monthDayPattern == null)
				{
					this.monthDayPattern = this._cultureData.MonthDay(this.Calendar.ID);
				}
				return this.monthDayPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.monthDayPattern = value;
			}
		}

		/// <summary>Gets or sets the string designator for hours that are "post meridiem" (after noon).</summary>
		/// <returns>The string designator for hours that are "post meridiem" (after noon). The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "PM".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x0011B76C File Offset: 0x0011996C
		// (set) Token: 0x060054D7 RID: 21719 RVA: 0x0011B78D File Offset: 0x0011998D
		public string PMDesignator
		{
			get
			{
				if (this.pmDesignator == null)
				{
					this.pmDesignator = this._cultureData.SPM2359;
				}
				return this.pmDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.pmDesignator = value;
			}
		}

		/// <summary>Gets the custom format string for a time value that is based on the Internet Engineering Task Force (IETF) Request for Comments (RFC) 1123 specification.</summary>
		/// <returns>The custom format string for a time value that is based on the IETF RFC 1123 specification.</returns>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0011B7C2 File Offset: 0x001199C2
		public string RFC1123Pattern
		{
			get
			{
				return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			}
		}

		/// <summary>Gets or sets the custom format string for a short date value.</summary>
		/// <returns>The custom format string for a short date value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0011B7C9 File Offset: 0x001199C9
		// (set) Token: 0x060054DA RID: 21722 RVA: 0x0011B7E8 File Offset: 0x001199E8
		public string ShortDatePattern
		{
			get
			{
				if (this.shortDatePattern == null)
				{
					this.shortDatePattern = this.UnclonedShortDatePatterns[0];
				}
				return this.shortDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.shortDatePattern = value;
				this.ClearTokenHashTable();
				this.generalLongTimePattern = null;
				this.generalShortTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		/// <summary>Gets or sets the custom format string for a short time value.</summary>
		/// <returns>The custom format string for a short time value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060054DB RID: 21723 RVA: 0x0011B83D File Offset: 0x00119A3D
		// (set) Token: 0x060054DC RID: 21724 RVA: 0x0011B85B File Offset: 0x00119A5B
		public string ShortTimePattern
		{
			get
			{
				if (this.shortTimePattern == null)
				{
					this.shortTimePattern = this.UnclonedShortTimePatterns[0];
				}
				return this.shortTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.shortTimePattern = value;
				this.ClearTokenHashTable();
				this.generalShortTimePattern = null;
			}
		}

		/// <summary>Gets the custom format string for a sortable date and time value.</summary>
		/// <returns>The custom format string for a sortable date and time value.</returns>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060054DD RID: 21725 RVA: 0x0011B897 File Offset: 0x00119A97
		public string SortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x060054DE RID: 21726 RVA: 0x0011B89E File Offset: 0x00119A9E
		internal string GeneralShortTimePattern
		{
			get
			{
				if (this.generalShortTimePattern == null)
				{
					this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern;
				}
				return this.generalShortTimePattern;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x0011B8CA File Offset: 0x00119ACA
		internal string GeneralLongTimePattern
		{
			get
			{
				if (this.generalLongTimePattern == null)
				{
					this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern;
				}
				return this.generalLongTimePattern;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x0011B8F8 File Offset: 0x00119AF8
		internal string DateTimeOffsetPattern
		{
			get
			{
				if (this.dateTimeOffsetPattern == null)
				{
					string text = this.ShortDatePattern + " " + this.LongTimePattern;
					bool flag = false;
					bool flag2 = false;
					char c = '\'';
					int num = 0;
					while (!flag && num < this.LongTimePattern.Length)
					{
						char c2 = this.LongTimePattern[num];
						if (c2 <= '%')
						{
							if (c2 == '"')
							{
								goto IL_006A;
							}
							if (c2 == '%')
							{
								goto IL_0096;
							}
						}
						else
						{
							if (c2 == '\'')
							{
								goto IL_006A;
							}
							if (c2 == '\\')
							{
								goto IL_0096;
							}
							if (c2 == 'z')
							{
								flag = !flag2;
							}
						}
						IL_009C:
						num++;
						continue;
						IL_006A:
						if (flag2 && c == this.LongTimePattern[num])
						{
							flag2 = false;
							goto IL_009C;
						}
						if (!flag2)
						{
							c = this.LongTimePattern[num];
							flag2 = true;
							goto IL_009C;
						}
						goto IL_009C;
						IL_0096:
						num++;
						goto IL_009C;
					}
					if (!flag)
					{
						text += " zzz";
					}
					this.dateTimeOffsetPattern = text;
				}
				return this.dateTimeOffsetPattern;
			}
		}

		/// <summary>Gets or sets the string that separates the components of time, that is, the hour, minutes, and seconds.</summary>
		/// <returns>The string that separates the components of time. The default for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is ":".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x0011B9D8 File Offset: 0x00119BD8
		// (set) Token: 0x060054E2 RID: 21730 RVA: 0x0011B9F9 File Offset: 0x00119BF9
		public string TimeSeparator
		{
			get
			{
				if (this.timeSeparator == null)
				{
					this.timeSeparator = this._cultureData.TimeSeparator;
				}
				return this.timeSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.timeSeparator = value;
			}
		}

		/// <summary>Gets the custom format string for a universal, sortable date and time string.</summary>
		/// <returns>The custom format string for a universal, sortable date and time string.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x060054E3 RID: 21731 RVA: 0x0011BA2E File Offset: 0x00119C2E
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			}
		}

		/// <summary>Gets or sets the custom format string for a year and month value.</summary>
		/// <returns>The custom format string for a year and month value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0011BA35 File Offset: 0x00119C35
		// (set) Token: 0x060054E5 RID: 21733 RVA: 0x0011BA53 File Offset: 0x00119C53
		public string YearMonthPattern
		{
			get
			{
				if (this.yearMonthPattern == null)
				{
					this.yearMonthPattern = this.UnclonedYearMonthPatterns[0];
				}
				return this.yearMonthPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.yearMonthPattern = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0011BA88 File Offset: 0x00119C88
		private static void CheckNullValue(string[] values, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (values[i] == null)
				{
					throw new ArgumentNullException("value", "Found a null value within an array.");
				}
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific abbreviated names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", and "Sat".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0011BAB6 File Offset: 0x00119CB6
		// (set) Token: 0x060054E8 RID: 21736 RVA: 0x0011BAC8 File Offset: 0x00119CC8
		public string[] AbbreviatedDayNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.abbreviatedDayNames = value;
			}
		}

		/// <summary>Gets or sets a string array of the shortest unique abbreviated day names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of day names.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the array does not have exactly seven elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value array or one of the elements of the value array is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x0011BB32 File Offset: 0x00119D32
		// (set) Token: 0x060054EA RID: 21738 RVA: 0x0011BB44 File Offset: 0x00119D44
		public string[] ShortestDayNames
		{
			get
			{
				return (string[])this.internalGetSuperShortDayNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.m_superShortDayNames = value;
			}
		}

		/// <summary>Gets or sets a one-dimensional string array that contains the culture-specific full names of the days of the week.</summary>
		/// <returns>A one-dimensional string array that contains the culture-specific full names of the days of the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", and "Saturday".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 7. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x0011BBA8 File Offset: 0x00119DA8
		// (set) Token: 0x060054EC RID: 21740 RVA: 0x0011BBBC File Offset: 0x00119DBC
		public string[] DayNames
		{
			get
			{
				return (string[])this.internalGetDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.dayNames = value;
			}
		}

		/// <summary>Gets or sets a one-dimensional string array that contains the culture-specific abbreviated names of the months.</summary>
		/// <returns>A one-dimensional string array with 13 elements that contains the culture-specific abbreviated names of the months. For 12-month calendars, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x0011BC26 File Offset: 0x00119E26
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x0011BC38 File Offset: 0x00119E38
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.abbreviatedMonthNames = value;
			}
		}

		/// <summary>Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months.</summary>
		/// <returns>A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific full names of the months. In a 12-month calendar, the 13th element of the array is an empty string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", and "".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an array that is multidimensional or that has a length that is not exactly 13. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0011BCA6 File Offset: 0x00119EA6
		// (set) Token: 0x060054F0 RID: 21744 RVA: 0x0011BCB8 File Offset: 0x00119EB8
		public string[] MonthNames
		{
			get
			{
				return (string[])this.internalGetMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.monthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x0011BD26 File Offset: 0x00119F26
		internal bool HasSpacesInMonthNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x0011BD33 File Offset: 0x00119F33
		internal bool HasSpacesInDayNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0011BD44 File Offset: 0x00119F44
		internal string internalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
		{
			string[] array;
			if (style != MonthNameStyles.Genitive)
			{
				if (style != MonthNameStyles.LeapYear)
				{
					array = (abbreviated ? this.internalGetAbbreviatedMonthNames() : this.internalGetMonthNames());
				}
				else
				{
					array = this.internalGetLeapYearMonthNames();
				}
			}
			else
			{
				array = this.internalGetGenitiveMonthNames(abbreviated);
			}
			if (month < 1 || month > array.Length)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, array.Length));
			}
			return array[month - 1];
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0011BDB4 File Offset: 0x00119FB4
		private string[] internalGetGenitiveMonthNames(bool abbreviated)
		{
			if (abbreviated)
			{
				if (this.m_genitiveAbbreviatedMonthNames == null)
				{
					this.m_genitiveAbbreviatedMonthNames = this._cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
				}
				return this.m_genitiveAbbreviatedMonthNames;
			}
			if (this.genitiveMonthNames == null)
			{
				this.genitiveMonthNames = this._cultureData.GenitiveMonthNames(this.Calendar.ID);
			}
			return this.genitiveMonthNames;
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0011BE19 File Offset: 0x0011A019
		internal string[] internalGetLeapYearMonthNames()
		{
			if (this.leapYearMonthNames == null)
			{
				this.leapYearMonthNames = this._cultureData.LeapYearMonthNames(this.Calendar.ID);
			}
			return this.leapYearMonthNames;
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x060054F6 RID: 21750 RVA: 0x0011BE45 File Offset: 0x0011A045
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetAbbreviatedDayOfWeekNames()[(int)dayofweek];
		}

		/// <summary>Obtains the shortest abbreviated day name for a specified day of the week associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The abbreviated name of the week that corresponds to the <paramref name="dayOfWeek" /> parameter.</returns>
		/// <param name="dayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayOfWeek" /> is not a value in the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
		// Token: 0x060054F7 RID: 21751 RVA: 0x0011BE78 File Offset: 0x0011A078
		public string GetShortestDayName(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetSuperShortDayNames()[(int)dayOfWeek];
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0011BEAC File Offset: 0x0011A0AC
		private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
		{
			string[] array = new string[patterns1.Length * patterns2.Length];
			int num = 0;
			for (int i = 0; i < patterns1.Length; i++)
			{
				for (int j = 0; j < patterns2.Length; j++)
				{
					array[num++] = patterns1[i] + connectString + patterns2[j];
				}
			}
			return array;
		}

		/// <summary>Returns all the standard patterns in which date and time values can be formatted.</summary>
		/// <returns>An array that contains the standard patterns in which date and time values can be formatted.</returns>
		// Token: 0x060054F9 RID: 21753 RVA: 0x0011BEF8 File Offset: 0x0011A0F8
		public string[] GetAllDateTimePatterns()
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimePatterns = this.GetAllDateTimePatterns(DateTimeFormat.allStandardFormats[i]);
				for (int j = 0; j < allDateTimePatterns.Length; j++)
				{
					list.Add(allDateTimePatterns[j]);
				}
			}
			return list.ToArray();
		}

		/// <summary>Returns all the patterns in which date and time values can be formatted using the specified standard format string.</summary>
		/// <returns>An array containing the standard patterns in which date and time values can be formatted using the specified format string.</returns>
		/// <param name="format">A standard format string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="format" /> is not a valid standard format string. </exception>
		// Token: 0x060054FA RID: 21754 RVA: 0x0011BF50 File Offset: 0x0011A150
		public string[] GetAllDateTimePatterns(char format)
		{
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
					return this.AllLongDatePatterns;
				case 'E':
					goto IL_01AF;
				case 'F':
					break;
				case 'G':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
				default:
					switch (format)
					{
					case 'M':
						goto IL_013D;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_01AF;
					case 'O':
						goto IL_014F;
					case 'R':
						goto IL_0160;
					case 'T':
						return this.AllLongTimePatterns;
					case 'U':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
				return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
			}
			if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
					return this.AllShortDatePatterns;
				case 'e':
					goto IL_01AF;
				case 'f':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
				case 'g':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
				default:
					switch (format)
					{
					case 'm':
						goto IL_013D;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_01AF;
					case 'o':
						goto IL_014F;
					case 'r':
						goto IL_0160;
					case 's':
						return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss" };
					case 't':
						return this.AllShortTimePatterns;
					case 'u':
						return new string[] { this.UniversalSortableDateTimePattern };
					case 'y':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
			}
			return this.AllYearMonthPatterns;
			IL_013D:
			return new string[] { this.MonthDayPattern };
			IL_014F:
			return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK" };
			IL_0160:
			return new string[] { "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'" };
			IL_01AF:
			throw new ArgumentException(SR.Format("Format specifier '{0}' was invalid.", format), "format");
		}

		/// <summary>Returns the culture-specific full name of the specified day of the week based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the day of the week represented by <paramref name="dayofweek" />.</returns>
		/// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value. </exception>
		// Token: 0x060054FB RID: 21755 RVA: 0x0011C128 File Offset: 0x0011A328
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetDayOfWeekNames()[(int)dayofweek];
		}

		/// <summary>Returns the culture-specific abbreviated name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific abbreviated name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x060054FC RID: 21756 RVA: 0x0011C15B File Offset: 0x0011A35B
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetAbbreviatedMonthNames()[month - 1];
		}

		/// <summary>Returns the culture-specific full name of the specified month based on the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The culture-specific full name of the month represented by <paramref name="month" />.</returns>
		/// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 13. </exception>
		// Token: 0x060054FD RID: 21757 RVA: 0x0011C192 File Offset: 0x0011A392
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetMonthNames()[month - 1];
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0011C1CC File Offset: 0x0011A3CC
		private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
		{
			if (defaultPattern == patterns[0])
			{
				return (string[])patterns.Clone();
			}
			int num = 0;
			while (num < patterns.Length && !(defaultPattern == patterns[num]))
			{
				num++;
			}
			string[] array;
			if (num < patterns.Length)
			{
				array = (string[])patterns.Clone();
				array[num] = array[0];
			}
			else
			{
				array = new string[patterns.Length + 1];
				Array.Copy(patterns, 0, array, 1, patterns.Length);
			}
			array[0] = defaultPattern;
			return array;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x0011C23F File Offset: 0x0011A43F
		private string[] AllYearMonthPatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x0011C252 File Offset: 0x0011A452
		private string[] AllShortDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06005501 RID: 21761 RVA: 0x0011C265 File Offset: 0x0011A465
		private string[] AllShortTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x0011C278 File Offset: 0x0011A478
		private string[] AllLongDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x0011C28B File Offset: 0x0011A48B
		private string[] AllLongTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x0011C29E File Offset: 0x0011A49E
		private string[] UnclonedYearMonthPatterns
		{
			get
			{
				if (this.allYearMonthPatterns == null)
				{
					this.allYearMonthPatterns = this._cultureData.YearMonths(this.Calendar.ID);
				}
				return this.allYearMonthPatterns;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005505 RID: 21765 RVA: 0x0011C2CA File Offset: 0x0011A4CA
		private string[] UnclonedShortDatePatterns
		{
			get
			{
				if (this.allShortDatePatterns == null)
				{
					this.allShortDatePatterns = this._cultureData.ShortDates(this.Calendar.ID);
				}
				return this.allShortDatePatterns;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x0011C2F6 File Offset: 0x0011A4F6
		private string[] UnclonedLongDatePatterns
		{
			get
			{
				if (this.allLongDatePatterns == null)
				{
					this.allLongDatePatterns = this._cultureData.LongDates(this.Calendar.ID);
				}
				return this.allLongDatePatterns;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x0011C322 File Offset: 0x0011A522
		private string[] UnclonedShortTimePatterns
		{
			get
			{
				if (this.allShortTimePatterns == null)
				{
					this.allShortTimePatterns = this._cultureData.ShortTimes;
				}
				return this.allShortTimePatterns;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x0011C343 File Offset: 0x0011A543
		private string[] UnclonedLongTimePatterns
		{
			get
			{
				if (this.allLongTimePatterns == null)
				{
					this.allLongTimePatterns = this._cultureData.LongTimes;
				}
				return this.allLongTimePatterns;
			}
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.</returns>
		/// <param name="dtfi">The <see cref="T:System.Globalization.DateTimeFormatInfo" /> object to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dtfi" /> is null. </exception>
		// Token: 0x06005509 RID: 21769 RVA: 0x0011C364 File Offset: 0x0011A564
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			if (dtfi == null)
			{
				throw new ArgumentNullException("dtfi", "Object cannot be null.");
			}
			if (dtfi.IsReadOnly)
			{
				return dtfi;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.MemberwiseClone();
			dateTimeFormatInfo.calendar = Calendar.ReadOnly(dtfi.Calendar);
			dateTimeFormatInfo._isReadOnly = true;
			return dateTimeFormatInfo;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x0011C3B1 File Offset: 0x0011A5B1
		public bool IsReadOnly
		{
			get
			{
				return GlobalizationMode.Invariant || this._isReadOnly;
			}
		}

		/// <summary>Gets the native name of the calendar associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>The native name of the calendar used in the culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object if that name is available, or the empty string ("") if the native calendar name is not available.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x0011C3C2 File Offset: 0x0011A5C2
		public string NativeCalendarName
		{
			get
			{
				return this._cultureData.CalendarName(this.Calendar.ID);
			}
		}

		/// <summary>Sets the custom date and time format strings that correspond to a specified standard format string.</summary>
		/// <param name="patterns">An array of custom format strings.</param>
		/// <param name="format">The standard format string associated with the custom format strings specified in the <paramref name="patterns" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="patterns" /> is null or a zero-length array.-or-<paramref name="format" /> is not a valid standard format string or is a standard format string whose patterns cannot be set.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="patterns" /> has an array element whose value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x0600550C RID: 21772 RVA: 0x0011C3DC File Offset: 0x0011A5DC
		public void SetAllDateTimePatterns(string[] patterns, char format)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException("Instance is read-only.");
			}
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns", "Array cannot be null.");
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException("Array must not be of length zero.", "patterns");
			}
			for (int i = 0; i < patterns.Length; i++)
			{
				if (patterns[i] == null)
				{
					throw new ArgumentNullException("patterns[" + i.ToString() + "]", "Found a null value within an array.");
				}
			}
			if (format <= 'Y')
			{
				if (format == 'D')
				{
					this.allLongDatePatterns = patterns;
					this.longDatePattern = this.allLongDatePatterns[0];
					goto IL_0126;
				}
				if (format == 'T')
				{
					this.allLongTimePatterns = patterns;
					this.longTimePattern = this.allLongTimePatterns[0];
					goto IL_0126;
				}
				if (format != 'Y')
				{
					goto IL_010B;
				}
			}
			else
			{
				if (format == 'd')
				{
					this.allShortDatePatterns = patterns;
					this.shortDatePattern = this.allShortDatePatterns[0];
					goto IL_0126;
				}
				if (format == 't')
				{
					this.allShortTimePatterns = patterns;
					this.shortTimePattern = this.allShortTimePatterns[0];
					goto IL_0126;
				}
				if (format != 'y')
				{
					goto IL_010B;
				}
			}
			this.allYearMonthPatterns = patterns;
			this.yearMonthPattern = this.allYearMonthPatterns[0];
			goto IL_0126;
			IL_010B:
			throw new ArgumentException(SR.Format("Format specifier '{0}' was invalid.", format), "format");
			IL_0126:
			this.ClearTokenHashTable();
		}

		/// <summary>Gets or sets a string array of abbreviated month names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>An array of abbreviated month names.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the array is multidimensional or has a length that is not exactly 13.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the array or one of the elements of the array is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x0011C515 File Offset: 0x0011A715
		// (set) Token: 0x0600550E RID: 21774 RVA: 0x0011C528 File Offset: 0x0011A728
		public string[] AbbreviatedMonthGenitiveNames
		{
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(true).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.m_genitiveAbbreviatedMonthNames = value;
			}
		}

		/// <summary>Gets or sets a string array of month names associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.</summary>
		/// <returns>A string array of month names.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the array is multidimensional or has a length that is not exactly 13.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the array or one of its elements is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x0011C596 File Offset: 0x0011A796
		// (set) Token: 0x06005510 RID: 21776 RVA: 0x0011C5AC File Offset: 0x0011A7AC
		public string[] MonthGenitiveNames
		{
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(false).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.genitiveMonthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x0011C61C File Offset: 0x0011A81C
		internal string FullTimeSpanPositivePattern
		{
			get
			{
				if (this._fullTimeSpanPositivePattern == null)
				{
					CultureData cultureData;
					if (this._cultureData.UseUserOverride)
					{
						cultureData = CultureData.GetCultureData(this._cultureData.CultureName, false);
					}
					else
					{
						cultureData = this._cultureData;
					}
					string numberDecimalSeparator = new NumberFormatInfo(cultureData).NumberDecimalSeparator;
					this._fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + numberDecimalSeparator + "'FFFFFFF";
				}
				return this._fullTimeSpanPositivePattern;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x0011C681 File Offset: 0x0011A881
		internal string FullTimeSpanNegativePattern
		{
			get
			{
				if (this._fullTimeSpanNegativePattern == null)
				{
					this._fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern;
				}
				return this._fullTimeSpanNegativePattern;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x0011C6A7 File Offset: 0x0011A8A7
		internal CompareInfo CompareInfo
		{
			get
			{
				if (this._compareInfo == null)
				{
					this._compareInfo = CompareInfo.GetCompareInfo(this._cultureData.SCOMPAREINFO);
				}
				return this._compareInfo;
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
		internal static void ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException("An undefined DateTimeStyles value is being used.", parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.", parameterName);
			}
			if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles value RoundtripKind cannot be used with the values AssumeLocal, AssumeUniversal or AdjustToUniversal.", parameterName);
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x0011C725 File Offset: 0x0011A925
		internal DateTimeFormatFlags FormatFlags
		{
			get
			{
				if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
				{
					return this.InitializeFormatFlags();
				}
				return this.formatFlags;
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0011C740 File Offset: 0x0011A940
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DateTimeFormatFlags InitializeFormatFlags()
		{
			this.formatFlags = (DateTimeFormatFlags)(DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames) | DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar(this.Calendar.ID));
			return this.formatFlags;
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x0011C7BC File Offset: 0x0011A9BC
		internal bool HasForceTwoDigitYears
		{
			get
			{
				CalendarId calendarId = (CalendarId)this.calendar.ID;
				return calendarId - CalendarId.JAPAN <= 1;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x0011C7DF File Offset: 0x0011A9DF
		internal bool HasYearMonthAdjustment
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0011C7EC File Offset: 0x0011A9EC
		internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
		{
			if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
			{
				if (year < 1000)
				{
					year += 5000;
				}
				if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
				{
					return false;
				}
				if (parsedMonthName && !this.Calendar.IsLeapYear(year))
				{
					if (month >= 8)
					{
						month--;
					}
					else if (month == 7)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0011C874 File Offset: 0x0011AA74
		internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_jajpDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
				dateTimeFormat.Calendar = JapaneseCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_jajpDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0011C8B8 File Offset: 0x0011AAB8
		internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_zhtwDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("zh-TW", false).DateTimeFormat;
				dateTimeFormat.Calendar = TaiwanCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0011C8FC File Offset: 0x0011AAFC
		private void ClearTokenHashTable()
		{
			this._dtfiTokenHash = null;
			this.formatFlags = DateTimeFormatFlags.NotInitialized;
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0011C90C File Offset: 0x0011AB0C
		internal DateTimeFormatInfo.TokenHashValue[] CreateTokenHashTable()
		{
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = new DateTimeFormatInfo.TokenHashValue[199];
				if (!GlobalizationMode.Invariant)
				{
					this.LanguageName.Equals("ko");
				}
				string text = this.TimeSeparator.Trim();
				if ("," != text)
				{
					this.InsertHash(array, ",", TokenType.IgnorableSymbol, 0);
				}
				if ("." != text)
				{
					this.InsertHash(array, ".", TokenType.IgnorableSymbol, 0);
				}
				if (!GlobalizationMode.Invariant && "시" != text && "時" != text && "时" != text)
				{
					this.InsertHash(array, this.TimeSeparator, TokenType.SEP_Time, 0);
				}
				this.InsertHash(array, this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, this.PMDesignator, (TokenType)1284, 1);
				bool flag = false;
				if (!GlobalizationMode.Invariant)
				{
					this.PopulateSpecialTokenHashTable(array, ref flag);
				}
				if (!GlobalizationMode.Invariant && this.LanguageName.Equals("ky"))
				{
					this.InsertHash(array, "-", TokenType.IgnorableSymbol, 0);
				}
				else
				{
					this.InsertHash(array, "-", TokenType.SEP_DateOrOffset, 0);
				}
				if (!flag)
				{
					this.InsertHash(array, this.DateSeparator, TokenType.SEP_Date, 0);
				}
				this.AddMonthNames(array, null);
				for (int i = 1; i <= 13; i++)
				{
					this.InsertHash(array, this.GetAbbreviatedMonthName(i), TokenType.MonthToken, i);
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					for (int j = 1; j <= 13; j++)
					{
						string text2 = this.internalGetMonthName(j, MonthNameStyles.Genitive, false);
						this.InsertHash(array, text2, TokenType.MonthToken, j);
					}
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					for (int k = 1; k <= 13; k++)
					{
						string text3 = this.internalGetMonthName(k, MonthNameStyles.LeapYear, false);
						this.InsertHash(array, text3, TokenType.MonthToken, k);
					}
				}
				for (int l = 0; l < 7; l++)
				{
					string text4 = this.GetDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
					text4 = this.GetAbbreviatedDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
				}
				int[] eras = this.calendar.Eras;
				for (int m = 1; m <= eras.Length; m++)
				{
					this.InsertHash(array, this.GetEraName(m), TokenType.EraToken, m);
					this.InsertHash(array, this.GetAbbreviatedEraName(m), TokenType.EraToken, m);
				}
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.PMDesignator, (TokenType)1284, 1);
				for (int n = 1; n <= 12; n++)
				{
					string text5 = DateTimeFormatInfo.InvariantInfo.GetMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
					text5 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
				}
				for (int num = 0; num < 7; num++)
				{
					string text6 = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
					text6 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
				}
				for (int num2 = 0; num2 < this.AbbreviatedEnglishEraNames.Length; num2++)
				{
					this.InsertHash(array, this.AbbreviatedEnglishEraNames[num2], TokenType.EraToken, num2 + 1);
				}
				this.InsertHash(array, "T", TokenType.SEP_LocalTimeMark, 0);
				this.InsertHash(array, "GMT", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "Z", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "/", TokenType.SEP_Date, 0);
				this.InsertHash(array, ":", TokenType.SEP_Time, 0);
				this._dtfiTokenHash = array;
			}
			return array;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0011CCAC File Offset: 0x0011AEAC
		private void PopulateSpecialTokenHashTable(DateTimeFormatInfo.TokenHashValue[] temp, ref bool useDateSepAsIgnorableSymbol)
		{
			if (this.LanguageName.Equals("sq"))
			{
				this.InsertHash(temp, "." + this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(temp, "." + this.PMDesignator, (TokenType)1284, 1);
			}
			this.InsertHash(temp, "年", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "년", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "月", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "월", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "日", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "일", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "時", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "时", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "分", TokenType.SEP_MinuteSuff, 0);
			this.InsertHash(temp, "秒", TokenType.SEP_SecondSuff, 0);
			if (!AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3)
			{
				this.InsertHash(temp, "元", TokenType.YearNumberToken, 1);
				this.InsertHash(temp, "(", TokenType.IgnorableSymbol, 0);
				this.InsertHash(temp, ")", TokenType.IgnorableSymbol, 0);
			}
			if (this.LanguageName.Equals("ko"))
			{
				this.InsertHash(temp, "시", TokenType.SEP_HourSuff, 0);
				this.InsertHash(temp, "분", TokenType.SEP_MinuteSuff, 0);
				this.InsertHash(temp, "초", TokenType.SEP_SecondSuff, 0);
			}
			string[] dateWordsOfDTFI = new DateTimeFormatInfoScanner().GetDateWordsOfDTFI(this);
			DateTimeFormatFlags dateTimeFormatFlags = this.FormatFlags;
			if (dateWordsOfDTFI != null)
			{
				for (int i = 0; i < dateWordsOfDTFI.Length; i++)
				{
					char c = dateWordsOfDTFI[i][0];
					if (c != '\ue000')
					{
						if (c != '\ue001')
						{
							this.InsertHash(temp, dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							if (this.LanguageName.Equals("eu"))
							{
								this.InsertHash(temp, "." + dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							}
						}
						else
						{
							string text = dateWordsOfDTFI[i].Substring(1);
							this.InsertHash(temp, text, TokenType.IgnorableSymbol, 0);
							if (this.DateSeparator.Trim(null).Equals(text))
							{
								useDateSepAsIgnorableSymbol = true;
							}
						}
					}
					else
					{
						string text2 = dateWordsOfDTFI[i].Substring(1);
						this.AddMonthNames(temp, text2);
					}
				}
			}
			if (this.LanguageName.Equals("ja"))
			{
				for (int j = 0; j < 7; j++)
				{
					string text3 = "(" + this.GetAbbreviatedDayName((DayOfWeek)j) + ")";
					this.InsertHash(temp, text3, TokenType.DayOfWeekToken, j);
				}
				if (!DateTimeFormatInfo.IsJapaneseCalendar(this.Calendar))
				{
					DateTimeFormatInfo japaneseCalendarDTFI = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
					for (int k = 1; k <= japaneseCalendarDTFI.Calendar.Eras.Length; k++)
					{
						this.InsertHash(temp, japaneseCalendarDTFI.GetEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.GetAbbreviatedEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.AbbreviatedEnglishEraNames[k - 1], TokenType.JapaneseEraToken, k);
					}
					return;
				}
			}
			else if (this.CultureName.Equals("zh-TW"))
			{
				DateTimeFormatInfo taiwanCalendarDTFI = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
				for (int l = 1; l <= taiwanCalendarDTFI.Calendar.Eras.Length; l++)
				{
					if (taiwanCalendarDTFI.GetEraName(l).Length > 0)
					{
						this.InsertHash(temp, taiwanCalendarDTFI.GetEraName(l), TokenType.TEraToken, l);
					}
				}
			}
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0011D021 File Offset: 0x0011B221
		private static bool IsJapaneseCalendar(Calendar calendar)
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return calendar.GetType() == typeof(JapaneseCalendar);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0011D048 File Offset: 0x0011B248
		private void AddMonthNames(DateTimeFormatInfo.TokenHashValue[] temp, string monthPostfix)
		{
			for (int i = 1; i <= 13; i++)
			{
				string text = this.GetMonthName(i);
				if (text.Length > 0)
				{
					if (monthPostfix != null)
					{
						this.InsertHash(temp, text + monthPostfix, TokenType.MonthToken, i);
					}
					else
					{
						this.InsertHash(temp, text, TokenType.MonthToken, i);
					}
				}
				text = this.GetAbbreviatedMonthName(i);
				this.InsertHash(temp, text, TokenType.MonthToken, i);
			}
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0011D0A4 File Offset: 0x0011B2A4
		private unsafe static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
		{
			number = -1;
			badFormat = false;
			int index = str.Index;
			if (!HebrewNumber.IsDigit((char)(*str.Value[index])))
			{
				return false;
			}
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState;
			for (;;)
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar((char)(*str.Value[index++]), ref hebrewNumberParsingContext);
				if (hebrewNumberParsingState <= HebrewNumberParsingState.NotHebrewDigit)
				{
					break;
				}
				if (index >= str.Value.Length || hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
				{
					goto IL_005C;
				}
			}
			return false;
			IL_005C:
			if (hebrewNumberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				return false;
			}
			str.Advance(index - str.Index);
			number = hebrewNumberParsingContext.result;
			return true;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0011D12B File Offset: 0x0011B32B
		private static bool IsHebrewChar(char ch)
		{
			return ch >= '\u0590' && ch <= '\u05ff';
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0011D144 File Offset: 0x0011B344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsAllowedJapaneseTokenFollowedByNonSpaceLetter(string tokenString, char nextCh)
		{
			return !AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3 && (nextCh == "元"[0] || (tokenString == "元" && nextCh == "年"[0]));
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0011D194 File Offset: 0x0011B394
		internal unsafe bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
		{
			tokenType = TokenType.UnknownToken;
			tokenValue = 0;
			char c = str.m_current;
			bool flag = char.IsLetter(c);
			if (flag)
			{
				c = this.Culture.TextInfo.ToLower(c);
				bool flag2;
				if (!GlobalizationMode.Invariant && DateTimeFormatInfo.IsHebrewChar(c) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out flag2, out tokenValue))
				{
					if (flag2)
					{
						tokenType = TokenType.UnknownToken;
						return false;
					}
					tokenType = TokenType.HebrewNumber;
					return true;
				}
			}
			int num = (int)(c % 'Ç');
			int num2 = (int)('\u0001' + c % 'Å');
			int num3 = str.Length - str.Index;
			int num4 = 0;
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = this.CreateTokenHashTable();
			}
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			int num6;
			for (;;)
			{
				tokenHashValue = array[num];
				if (tokenHashValue == null)
				{
					return false;
				}
				if ((tokenHashValue.tokenType & TokenMask) > (TokenType)0 && tokenHashValue.tokenString.Length <= num3)
				{
					bool flag3 = true;
					if (flag)
					{
						int num5 = str.Index + tokenHashValue.tokenString.Length;
						if (num5 > str.Length)
						{
							flag3 = false;
						}
						else if (num5 < str.Length)
						{
							char c2 = (char)(*str.Value[num5]);
							flag3 = !char.IsLetter(c2) || this.IsAllowedJapaneseTokenFollowedByNonSpaceLetter(tokenHashValue.tokenString, c2);
						}
					}
					if (flag3 && ((tokenHashValue.tokenString.Length == 1 && *str.Value[str.Index] == (ushort)tokenHashValue.tokenString[0]) || this.Culture.CompareInfo.Compare(str.Value.Slice(str.Index, tokenHashValue.tokenString.Length), tokenHashValue.tokenString, CompareOptions.IgnoreCase) == 0))
					{
						break;
					}
					if ((tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames) || (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames))
					{
						num6 = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref num6))
						{
							goto Block_19;
						}
					}
				}
				num4++;
				num += num2;
				if (num >= 199)
				{
					num -= 199;
				}
				if (num4 >= 199)
				{
					return false;
				}
			}
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(tokenHashValue.tokenString.Length);
			return true;
			Block_19:
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(num6);
			return true;
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0011D3DC File Offset: 0x0011B5DC
		private void InsertAtCurrentHashNode(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
		{
			DateTimeFormatInfo.TokenHashValue tokenHashValue = hashTable[hashcode];
			hashTable[hashcode] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			while (++pos < 199)
			{
				hashcode += hashProbe;
				if (hashcode >= 199)
				{
					hashcode -= 199;
				}
				DateTimeFormatInfo.TokenHashValue tokenHashValue2 = hashTable[hashcode];
				if (tokenHashValue2 == null || this.Culture.TextInfo.ToLower(tokenHashValue2.tokenString[0]) == ch)
				{
					hashTable[hashcode] = tokenHashValue;
					if (tokenHashValue2 == null)
					{
						return;
					}
					tokenHashValue = tokenHashValue2;
				}
			}
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0011D458 File Offset: 0x0011B658
		private void InsertHash(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
		{
			if (str == null || str.Length == 0)
			{
				return;
			}
			int num = 0;
			if (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[str.Length - 1]))
			{
				str = str.Trim(null);
				if (str.Length == 0)
				{
					return;
				}
			}
			char c = this.Culture.TextInfo.ToLower(str[0]);
			int num2 = (int)(c % 'Ç');
			int num3 = (int)('\u0001' + c % 'Å');
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			for (;;)
			{
				tokenHashValue = hashTable[num2];
				if (tokenHashValue == null)
				{
					break;
				}
				if (str.Length >= tokenHashValue.tokenString.Length && this.CompareStringIgnoreCaseOptimized(str, 0, tokenHashValue.tokenString.Length, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length))
				{
					goto Block_6;
				}
				num++;
				num2 += num3;
				if (num2 >= 199)
				{
					num2 -= 199;
				}
				if (num >= 199)
				{
					return;
				}
			}
			hashTable[num2] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			return;
			Block_6:
			if (str.Length > tokenHashValue.tokenString.Length)
			{
				this.InsertAtCurrentHashNode(hashTable, str, c, tokenType, tokenValue, num, num2, num3);
				return;
			}
			int tokenType2 = (int)tokenHashValue.tokenType;
			if (((tokenType2 & 255) == 0 && (tokenType & TokenType.RegularTokenMask) != (TokenType)0) || ((tokenType2 & 65280) == 0 && (tokenType & TokenType.SeparatorTokenMask) != (TokenType)0))
			{
				tokenHashValue.tokenType |= tokenType;
				if (tokenValue != 0)
				{
					tokenHashValue.tokenValue = tokenValue;
				}
			}
			return;
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0011D5BA File Offset: 0x0011B7BA
		private bool CompareStringIgnoreCaseOptimized(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return (length1 == 1 && length2 == 1 && string1[offset1] == string2[offset2]) || this.Culture.CompareInfo.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x040033EA RID: 13290
		private static volatile DateTimeFormatInfo s_invariantInfo;

		// Token: 0x040033EB RID: 13291
		[NonSerialized]
		private CultureData _cultureData;

		// Token: 0x040033EC RID: 13292
		private string _name;

		// Token: 0x040033ED RID: 13293
		[NonSerialized]
		private string _langName;

		// Token: 0x040033EE RID: 13294
		[NonSerialized]
		private CompareInfo _compareInfo;

		// Token: 0x040033EF RID: 13295
		[NonSerialized]
		private CultureInfo _cultureInfo;

		// Token: 0x040033F0 RID: 13296
		private string amDesignator;

		// Token: 0x040033F1 RID: 13297
		private string pmDesignator;

		// Token: 0x040033F2 RID: 13298
		private string dateSeparator;

		// Token: 0x040033F3 RID: 13299
		private string generalShortTimePattern;

		// Token: 0x040033F4 RID: 13300
		private string generalLongTimePattern;

		// Token: 0x040033F5 RID: 13301
		private string timeSeparator;

		// Token: 0x040033F6 RID: 13302
		private string monthDayPattern;

		// Token: 0x040033F7 RID: 13303
		private string dateTimeOffsetPattern;

		// Token: 0x040033F8 RID: 13304
		private const string rfc1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";

		// Token: 0x040033F9 RID: 13305
		private const string sortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		// Token: 0x040033FA RID: 13306
		private const string universalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";

		// Token: 0x040033FB RID: 13307
		private Calendar calendar;

		// Token: 0x040033FC RID: 13308
		private int firstDayOfWeek = -1;

		// Token: 0x040033FD RID: 13309
		private int calendarWeekRule = -1;

		// Token: 0x040033FE RID: 13310
		private string fullDateTimePattern;

		// Token: 0x040033FF RID: 13311
		private string[] abbreviatedDayNames;

		// Token: 0x04003400 RID: 13312
		private string[] m_superShortDayNames;

		// Token: 0x04003401 RID: 13313
		private string[] dayNames;

		// Token: 0x04003402 RID: 13314
		private string[] abbreviatedMonthNames;

		// Token: 0x04003403 RID: 13315
		private string[] monthNames;

		// Token: 0x04003404 RID: 13316
		private string[] genitiveMonthNames;

		// Token: 0x04003405 RID: 13317
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x04003406 RID: 13318
		private string[] leapYearMonthNames;

		// Token: 0x04003407 RID: 13319
		private string longDatePattern;

		// Token: 0x04003408 RID: 13320
		private string shortDatePattern;

		// Token: 0x04003409 RID: 13321
		private string yearMonthPattern;

		// Token: 0x0400340A RID: 13322
		private string longTimePattern;

		// Token: 0x0400340B RID: 13323
		private string shortTimePattern;

		// Token: 0x0400340C RID: 13324
		private string[] allYearMonthPatterns;

		// Token: 0x0400340D RID: 13325
		private string[] allShortDatePatterns;

		// Token: 0x0400340E RID: 13326
		private string[] allLongDatePatterns;

		// Token: 0x0400340F RID: 13327
		private string[] allShortTimePatterns;

		// Token: 0x04003410 RID: 13328
		private string[] allLongTimePatterns;

		// Token: 0x04003411 RID: 13329
		private string[] m_eraNames;

		// Token: 0x04003412 RID: 13330
		private string[] m_abbrevEraNames;

		// Token: 0x04003413 RID: 13331
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x04003414 RID: 13332
		private CalendarId[] optionalCalendars;

		// Token: 0x04003415 RID: 13333
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x04003416 RID: 13334
		internal bool _isReadOnly;

		// Token: 0x04003417 RID: 13335
		private DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;

		// Token: 0x04003418 RID: 13336
		private static readonly char[] s_monthSpaces = new char[] { ' ', '\u00a0' };

		// Token: 0x04003419 RID: 13337
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x0400341A RID: 13338
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x0400341B RID: 13339
		private string _fullTimeSpanPositivePattern;

		// Token: 0x0400341C RID: 13340
		private string _fullTimeSpanNegativePattern;

		// Token: 0x0400341D RID: 13341
		internal const DateTimeStyles InvalidDateTimeStyles = ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind);

		// Token: 0x0400341E RID: 13342
		[NonSerialized]
		private DateTimeFormatInfo.TokenHashValue[] _dtfiTokenHash;

		// Token: 0x0400341F RID: 13343
		private const int TOKEN_HASH_SIZE = 199;

		// Token: 0x04003420 RID: 13344
		private const int SECOND_PRIME = 197;

		// Token: 0x04003421 RID: 13345
		private const string dateSeparatorOrTimeZoneOffset = "-";

		// Token: 0x04003422 RID: 13346
		private const string invariantDateSeparator = "/";

		// Token: 0x04003423 RID: 13347
		private const string invariantTimeSeparator = ":";

		// Token: 0x04003424 RID: 13348
		internal const string IgnorablePeriod = ".";

		// Token: 0x04003425 RID: 13349
		internal const string IgnorableComma = ",";

		// Token: 0x04003426 RID: 13350
		internal const string CJKYearSuff = "年";

		// Token: 0x04003427 RID: 13351
		internal const string CJKMonthSuff = "月";

		// Token: 0x04003428 RID: 13352
		internal const string CJKDaySuff = "日";

		// Token: 0x04003429 RID: 13353
		internal const string KoreanYearSuff = "년";

		// Token: 0x0400342A RID: 13354
		internal const string KoreanMonthSuff = "월";

		// Token: 0x0400342B RID: 13355
		internal const string KoreanDaySuff = "일";

		// Token: 0x0400342C RID: 13356
		internal const string KoreanHourSuff = "시";

		// Token: 0x0400342D RID: 13357
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x0400342E RID: 13358
		internal const string KoreanSecondSuff = "초";

		// Token: 0x0400342F RID: 13359
		internal const string CJKHourSuff = "時";

		// Token: 0x04003430 RID: 13360
		internal const string ChineseHourSuff = "时";

		// Token: 0x04003431 RID: 13361
		internal const string CJKMinuteSuff = "分";

		// Token: 0x04003432 RID: 13362
		internal const string CJKSecondSuff = "秒";

		// Token: 0x04003433 RID: 13363
		internal const string JapaneseEraStart = "元";

		// Token: 0x04003434 RID: 13364
		internal const string LocalTimeMark = "T";

		// Token: 0x04003435 RID: 13365
		internal const string GMTName = "GMT";

		// Token: 0x04003436 RID: 13366
		internal const string ZuluName = "Z";

		// Token: 0x04003437 RID: 13367
		internal const string KoreanLangName = "ko";

		// Token: 0x04003438 RID: 13368
		internal const string JapaneseLangName = "ja";

		// Token: 0x04003439 RID: 13369
		internal const string EnglishLangName = "en";

		// Token: 0x0400343A RID: 13370
		private static volatile DateTimeFormatInfo s_jajpDTFI;

		// Token: 0x0400343B RID: 13371
		private static volatile DateTimeFormatInfo s_zhtwDTFI;

		// Token: 0x0200095C RID: 2396
		internal class TokenHashValue
		{
			// Token: 0x06005529 RID: 21801 RVA: 0x0011D610 File Offset: 0x0011B810
			internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
			{
				this.tokenString = tokenString;
				this.tokenType = tokenType;
				this.tokenValue = tokenValue;
			}

			// Token: 0x0400343C RID: 13372
			internal string tokenString;

			// Token: 0x0400343D RID: 13373
			internal TokenType tokenType;

			// Token: 0x0400343E RID: 13374
			internal int tokenValue;
		}
	}
}
