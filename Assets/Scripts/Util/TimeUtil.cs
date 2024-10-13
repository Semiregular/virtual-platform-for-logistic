using System;

namespace Util
{
    public static class TimeUtil
    {
        public static long GetTolSeconds()
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var totalMilliseconds= (long)(DateTimeOffset.UtcNow.ToUniversalTime() - startTime).TotalMilliseconds;
            return totalMilliseconds;
        }
    }
}
