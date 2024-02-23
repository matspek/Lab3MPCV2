using Lab3MPCV2.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;

namespace Lab3MPCV.Data
{
    public class MongoCRUD
    {
        public IMongoDatabase db;

        public MongoCRUD(string Database)
        {
            var client = new MongoClient("");
            db = client.GetDatabase(Database);
        }
        public async Task AddWorkExperience(string table, string name, WorkExperience workexperience)
        {
            var collection = db.GetCollection<CVModelDto>(table);
            var filter = Builders<CVModelDto>.Filter.Eq(x => x.personal_infomation[0].name, name);
            // Document with the specified _id was not found
            var update = Builders<CVModelDto>.Update.Push(x=>x.Erfarenhet, workexperience); // Update the value of field1
            await collection.UpdateOneAsync(filter, update);
        }   
        public async Task AddEducation(string table, string name, Education education)
        {
            var collection = db.GetCollection<CVModelDto>(table);
            var filter = Builders<CVModelDto>.Filter.Eq(x => x.personal_infomation[0].name, name);
            // Document with the specified _id was not found
            var update = Builders<CVModelDto>.Update.Push(x=>x.Utbildning, education); // Update the value of field1
            await collection.UpdateOneAsync(filter, update);
        }

        public async Task AddSkill(string table, string name, Kompetenser kompetenser)
        {
            var collection = db.GetCollection<CVModelDto>(table);
            var filter = Builders<CVModelDto>.Filter.Eq(x => x.personal_infomation[0].name, name);
            // Document with the specified _id was not found
            var update = Builders<CVModelDto>.Update.Push(x=>x.Kompetenser,kompetenser); // Update the value of field1
            var result= await collection.FindOneAndUpdateAsync(filter, update);
        }
        public async Task<CVModelDto> GetPerson(string table, string name)
        {
            var collection = db.GetCollection<CVModelDto>(table);
            var test1 = await collection.AsQueryable().ToListAsync();

            var test = test1[0];
            CVModelDto dto = new CVModelDto
            {
                Erfarenhet = test.Erfarenhet,
                personal_infomation = test.personal_infomation,
                Utbildning = test.Utbildning,
                Kompetenser = test.Kompetenser

            };
            return dto;


        }

        public  async Task UpdateJobTitle(string table, string name, string role)
        {
            var collection = db.GetCollection<CVModelDto>(table);
            var filter = Builders<CVModelDto>.Filter.Eq(x => x.personal_infomation[0].name, name);
            var update = Builders<CVModelDto>.Update.Set(x=>x.personal_infomation[0].role, role); // Update the value of field1
            await collection.UpdateOneAsync(filter, update);


        }
        public async Task DeteteWork(string table, string name, string position)
        {
            var collection = db.GetCollection<CVModel>(table);
            var filter = Builders<CVModel>.Filter.And(
          //  Builders<CVModel>.Filter.Eq(x=>", position),
            Builders<CVModel>.Filter.Eq(x=>x.personal_infomation[0].name, name));
            var result = await collection.DeleteOneAsync(filter);

            // Output result
            if (result.DeletedCount > 0)
            {
                Console.WriteLine($"Deleted {result.DeletedCount} document(s) with position: {position}");
            }
            else
            {
                Console.WriteLine($"No document found with position: {position}");
            }

        }

    }
}
