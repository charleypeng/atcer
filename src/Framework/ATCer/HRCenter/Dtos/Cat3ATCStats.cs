using System;
using System.Text.Json.Serialization;

namespace ATCer.HRCenter.Dtos;

public class Cat3ATCStats
{
    public DateTime? Date { get; set; }

    public string? Sector { get; set; }

    public IEnumerable<TimeItemDto>? Violator { get; set; }

    public TimeItemDto? Comparer { get; set; }

    [JsonIgnore]
    public Guid UID { get; set; }
}

