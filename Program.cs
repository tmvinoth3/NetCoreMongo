using System;
using CommandLine;
using DL;
using MongoConnect;

namespace MongoConnect
{
    class Program
    {            
        static void Main(string[] args)
        {
            MongoRepo mongoRepo = new MongoRepo();
            mongoRepo.Connect();
            
            Parser.Default.ParseArguments<CmdOptions>(args)
                   .WithParsed<CmdOptions>(o =>
                   {
                       Console.WriteLine(o.Seconds);
                       if(o.Seconds > 0){
                           mongoRepo.Get(o.Seconds);
                       }
                       else if(o.InsSeconds > 0)
                       {
                           mongoRepo.Create(o.InsSeconds);
                       }
                       else if(o.IsUpdate)
                       {                             
                           Console.WriteLine("Enter the number of batches: ");
                           int NoOfBatches = Convert.ToInt32(Console.ReadLine());
                           Console.WriteLine("Enter the number of records per batch: ");
                           int NoOfRecordsPerBatch = Convert.ToInt32(Console.ReadLine());   
                           for (int i = 0; i < NoOfBatches; i++)
                           {
                               bool updateRes = mongoRepo.Update(NoOfRecordsPerBatch);
                               if(updateRes)
                               {
                                   Console.WriteLine("Batch "+ (i+1) + " updated successfully");
                               }
                           }                       
                       }
                       else if (o.Verbose)
                       {
                           Console.WriteLine("--h Help");
                           Console.WriteLine("--g Get Records per second");
                           Console.WriteLine("--i Insert Records per second");
                       }
                       else
                       {
                           Console.WriteLine("--h Help");
                       }
                   });
        }
    }
}
