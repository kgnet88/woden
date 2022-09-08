namespace KgNet88.Woden.Account.Infrastructure.Common.Implementations;

/// <summary>
/// Realtime implementation of the date and time provider for production use.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// System Clock in locals default time zone.
    /// </summary>
    public static ZonedClock ZonedClock => SystemClock.Instance.InTzdbSystemDefaultZone();

    /// <summary>
    /// Timestamp on a global independent timeline sinc a specific epoch.
    /// </summary>
    public Instant Timestamp => SystemClock.Instance.GetCurrentInstant();

    /// <summary>
    /// Current date and time in UTC.
    /// </summary>
    public ZonedDateTime UtcNow => SystemClock.Instance.GetCurrentInstant().InUtc();

    /// <summary>
    /// Current date and time in the local time zone.
    /// </summary>
    public ZonedDateTime Now => ZonedClock.GetCurrentZonedDateTime();

    /// <summary>
    /// Current date in local time zone.
    /// </summary>
    public LocalDate Today => ZonedClock.GetCurrentDate();

    /// <summary>
    /// Current time in local time zone.
    /// </summary>
    public LocalTime TimeOfDay => ZonedClock.GetCurrentTimeOfDay();

    /// <summary>
    /// Current time zone.
    /// </summary>
    public DateTimeZone TimeZone => ZonedClock.Zone;

    /// <summary>
    /// Current used calendar system.
    /// </summary>
    public CalendarSystem Calendar => ZonedClock.Calendar;
}
