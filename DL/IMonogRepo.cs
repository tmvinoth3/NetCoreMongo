namespace DL
{
    interface IMongoRepo
    {
        void Connect();
        void Get(int Seconds);
        void Create(int Seconds);
        bool Update(int NoOfRecordsPerBatch);
    }
}