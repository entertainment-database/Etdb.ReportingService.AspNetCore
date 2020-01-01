using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Etdb.ReportingService.Repositories.Conventions
{
    public class GuidIdConvention : ConventionBase, IPostProcessingConvention {
        public void PostProcess(BsonClassMap classMap) {
            var idMap = classMap.IdMemberMap;
            if (idMap == null || idMap.MemberName != "Id" || idMap.MemberType != typeof(Guid)) return;
            
            idMap.SetIdGenerator(new GuidGenerator());
        }
    }
}