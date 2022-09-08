namespace KgNet88.Woden.Account.Application.Common.Interfaces;

/// <summary>
/// Service to abstract time from a specific implementation. Let you choose your provider and is ideal for
/// testing recurrent or timebound events. Current and local is free to choose.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// System Clock in locals default time zone.
    /// </summary>
    public static ZonedClock? ZonedClock { get; }

    /// <summary>
    /// Timestamp on a global independent timeline sinc a specific epoch.
    /// </summary>
    public Instant Timestamp { get; }

    /// <summary>
    /// Current date and time in UTC.
    /// </summary>
    public ZonedDateTime UtcNow { get; }

    /// <summary>
    /// Current date and time in the local time zone.
    /// </summary>
    public ZonedDateTime Now { get; }

    /// <summary>
    /// Current date in local time zone.
    /// </summary>
    public LocalDate Today { get; }

    /// <summary>
    /// Current time in local time zone.
    /// </summary>
    public LocalTime TimeOfDay { get; }

    /// <summary>
    /// Current time zone.
    /// </summary>
    public DateTimeZone TimeZone { get; }

    /// <summary>
    /// Current used calendar system.
    /// </summary>
    public CalendarSystem Calendar { get; }
}
