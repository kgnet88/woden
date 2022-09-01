namespace KgNet88.Woden.Account.Application.Interfaces.Common;

public interface IDateTimeProvider
{
    Instant Timestamp { get; }
    ZonedDateTime UtcNow { get; }
    ZonedDateTime Now { get; }
    LocalDate Today { get; }
    LocalTime TimeOfDay { get; }
    DateTimeZone TimeZone { get; }
    CalendarSystem Calendar { get; }
}
