using System.Collections.Generic;

public class TimeRange {
    private KeyValuePair<float, float> timeRangeValues;

    public static TimeRange FalseyTimeRange() {
        return new TimeRange(-1, -1);
    }

    public TimeRange(float minTime, float maxTime) {
        timeRangeValues = new KeyValuePair<float, float>(minTime, maxTime);
    }

    public float MinTime { get { return timeRangeValues.Key; } }

    public float MaxTime { get { return timeRangeValues.Value; } }

    public bool valueFitsInRange(float time) {
        return time >= MinTime && time <= MaxTime;
    }

    public bool isFalsey() {
        return MinTime == -1 && MaxTime == -1;
    }

    public override string ToString() {
        return timeRangeValues.ToString();
    }
}
