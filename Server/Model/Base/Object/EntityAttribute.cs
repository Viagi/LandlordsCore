using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
	[BsonKnownTypes(typeof(Location))]
	[BsonKnownTypes(typeof(Recharge))]
	[BsonKnownTypes(typeof(RechargeRecord))]
    [BsonKnownTypes(typeof(AccountInfo))]
    [BsonKnownTypes(typeof(UserInfo))]
    public partial class Entity
	{
	}
}
