using MongoDB.Bson;

namespace models
{
    public class LatLong
    {
        public ObjectId Id { get; set; }
        public string Temperature { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CustomString1 { get; set; }
        public string CustomString2 { get; set; }
        public string CustomString3 { get; set; }
    }
}