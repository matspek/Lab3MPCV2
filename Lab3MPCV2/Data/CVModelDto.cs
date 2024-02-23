using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lab3MPCV2.Data
{
    public class CVModelDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public PersonalInformation[] personal_infomation { get; set; }
        public List<WorkExperience> Erfarenhet { get; set; } = new List<WorkExperience>();
        public List<Education> Utbildning { get; set; } = new List<Education>();
        public List<Kompetenser> Kompetenser { get; set; } = new List<Kompetenser>();

    }
}
