using System;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Hosting;
using uptime_tracker.models;

namespace uptime_tracker.services
{
    public class TimeCheckService : IHostedService
    {

        private DateTime StartUpTime;
        public TimeCheckService()
        {

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartUpTime = DateTime.UtcNow;

            Task.Run(() => DoWork(cancellationToken));
            
            return Task.CompletedTask;
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (true)
            {


                if (cancellationToken.IsCancellationRequested) 
                {
                    return;
                }  

                try
                {
                    await UpdateTimeEvents();                        
                } catch (Exception e)
                {
                    
                }

                

                await Task.Delay(TimeSpan.FromMinutes(1));
            }

        }

        private async Task UpdateTimeEvents()
        {
            using (var db = new LiteDatabase(@".\storage.db"))
            {
                var timeEntryCollection = db.GetCollection<TimeEntry>("TimeEntry");


                var existingEntry = timeEntryCollection.FindOne(x => x.StartAt == StartUpTime);

                if (existingEntry != null)
                {
                    existingEntry.EndAt = DateTime.UtcNow;  
                    timeEntryCollection.Update(existingEntry.Id, existingEntry); 
                  
                } else
                {
                    var newEntry = new TimeEntry() 
                    {
                        StartAt = StartUpTime,
                        EndAt = DateTime.UtcNow
                    };

                    timeEntryCollection.Insert(newEntry);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}