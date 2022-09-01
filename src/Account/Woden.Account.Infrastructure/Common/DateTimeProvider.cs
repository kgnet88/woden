namespace KgNet88.Woden.Account.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public static ZonedClock ZonedClock => SystemClock.Instance.InTzdbSystemDefaultZone();
    public Instant Timestamp => SystemClock.Instance.GetCurrentInstant();
    public ZonedDateTime UtcNow => SystemClock.Instance.GetCurrentInstant().InUtc();
    public ZonedDateTime Now => ZonedClock.GetCurrentZonedDateTime();
    public LocalDate Today => ZonedClock.GetCurrentDate();
    public LocalTime TimeOfDay => ZonedClock.GetCurrentTimeOfDay();
    public DateTimeZone TimeZone => ZonedClock.Zone;
    public CalendarSystem Calendar => ZonedClock.Calendar;
}
