using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    [BsonKnownTypes(typeof(AccountInfo))]
    [BsonKnownTypes(typeof(UserInfo))]
    public partial class Entity
	{
	}
}
