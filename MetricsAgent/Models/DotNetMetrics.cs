﻿namespace MetricsAgent.Models;

public class DotNetMetrics
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public double Time { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Value} - {Time}";
    }
}
