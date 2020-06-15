using System;
using System.Collections.Generic;
using System.Diagnostics;
using models;
using MongoDB.Driver;

namespace DL
{
    public class MongoRepo : IMongoRepo
    {
        private IMongoCollection<LatLong> _coords;

        public void Connect()
        {
            string conStr = "mongodb://localhost:27017/";
            var client = new MongoClient(conStr);
            var database = client.GetDatabase("gps");
            _coords = database.GetCollection<LatLong>("latlong");
        }

        public void Get(int seconds)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int count = 1;
            double PrevSeconds = 0.0;
            Console.WriteLine((int)timer.Elapsed.TotalSeconds);
            
            while(timer.Elapsed.TotalSeconds < seconds)
            {
                List<LatLong> LatLongList = _coords.Find<LatLong>(lat => true).Limit(1).ToList();
                //Console.WriteLine(LatLongList[0].Latitude + "," + LatLongList[0].Longitude);
                if( (int)PrevSeconds != (int)timer.Elapsed.TotalSeconds ) 
                {
                    Console.WriteLine((int)PrevSeconds);
                    Console.WriteLine(count);
                    count = 0;
                }
                count++;
                PrevSeconds = timer.Elapsed.TotalSeconds;
            }
            timer.Stop();          
        }

        public void Create(int seconds)
        {  
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int count = 0;
            double PrevSeconds = 0.0;
            Console.WriteLine((int)timer.Elapsed.TotalSeconds);
            while(timer.Elapsed.TotalSeconds < seconds)
            {
                LatLong latLong = new LatLong();
                //Random random = new Random();
                // latLong.Temperature = Convert.ToString(random.Next(80,100));
                // latLong.Latitude = Convert.ToString(random.Next(10,30)) + "." + Convert.ToString(random.Next(516400146, 630304598)); //18.51640014679267 - 18.630304598192915
                // latLong.Longitude = Convert.ToString(random.Next(10,30)) + "." + Convert.ToString(random.Next(224464416, 341194152)); //-72.34119415283203 - -72.2244644165039
                latLong.Temperature = "100F";//Convert.ToString(count);
                latLong.Latitude = "18.51640014679267";
                latLong.Longitude = "72.2244644165039";
                latLong.CustomString1 = "test1";// + count.ToString();
                latLong.CustomString2 = "test2";// + count.ToString();
                latLong.CustomString3 = "test3";// + count.ToString();
                _coords.InsertOne(latLong);
                
                if( (int)PrevSeconds != (int)timer.Elapsed.TotalSeconds ) 
                {
                    Console.WriteLine((int)timer.Elapsed.TotalSeconds);
                    Console.WriteLine(count);
                    count = 0;
                }
                count++;
                PrevSeconds = timer.Elapsed.TotalSeconds;
            }
            timer.Stop();          
        }

        public bool Update(int NoOfRecordsPerBatch)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            bool Updated = false;
            try
            {
                var builder = Builders<LatLong>.Filter;
                var filterDef = builder.And(builder.Ne(l => l.Latitude, "0"), builder.Ne(l => l.Longitude, "0"));
                //filterDef.Find
                var res = _coords.Find(filterDef).Limit(NoOfRecordsPerBatch);
                var updateDef = Builders<LatLong>.Update.Set(l => l.Latitude, "0").Set(l => l.Longitude , "0");
                for (int i = 0; i < NoOfRecordsPerBatch; i++)
                {
                UpdateResult updateRes =_coords.UpdateOne(filterDef, updateDef);
                }
                Console.WriteLine("Time taken: " + timer.Elapsed.TotalSeconds + " seconds");
                Updated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
            timer.Stop();
            return Updated;
        }
    }
}