using CommandLine;

namespace MongoConnect
{
    public class CmdOptions 
    {
        [Option('g', "get", HelpText = "Get Records per second.")]
        public int Seconds { get; set; }

        [Option('i', "insert", HelpText = "Insert Records per second.")]
        public int InsSeconds { get; set; }

        [Option('u', "update", HelpText = "Update Records per batch.")]
        public bool IsUpdate { get; set; }        

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
}