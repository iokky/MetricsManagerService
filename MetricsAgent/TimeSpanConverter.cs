﻿using System.Text.Json;
using System.Text.Json.Serialization;


namespace MetricsAgent;

public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        TimeSpan timeSpan = TimeSpan.Parse(value);
        return timeSpan;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
