using LiteDB;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using uptime_tracker.models;

namespace uptime_tracker.Pages
{
    public class IndexModel : PageModel 
    {

        public List<TimeEntry> TimeEntries { get; set; }

        public string EntryJson { get; set; }
        public string test => "test";
        public IndexModel()
        {

        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            using (var db = new LiteDatabase(@".\storage.db"))
            {
                var collection = db.GetCollection<TimeEntry>("TimeEntry");

                TimeEntries = collection.FindAll().ToList();            
                EntryJson = System.Text.Json.JsonSerializer.Serialize(TimeEntries);
            }
        }

    }
}