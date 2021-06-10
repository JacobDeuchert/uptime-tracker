using System;
using LiteDB;

namespace uptime_tracker.models
{
    public class TimeEntry
    {
        public ObjectId Id { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
        
    }
}