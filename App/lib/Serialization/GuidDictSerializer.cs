using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PaintedPoker.Utils;

public class PlayerCollectionSerializer<TValue> : SerializerBase<PlayerCollection<TValue>>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PlayerCollection<TValue> value)
    {
        context.Writer.WriteStartDocument();
        foreach (var kvp in value)
        {
            context.Writer.WriteName(kvp.Key.ToString());
            BsonSerializer.Serialize(context.Writer, typeof(TValue), kvp.Value);
        }
        context.Writer.WriteEndDocument();
    }

    public override PlayerCollection<TValue> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var dictionary = new PlayerCollection<TValue>();
        context.Reader.ReadStartDocument();
        while (context.Reader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var key = Guid.Parse(context.Reader.ReadName(Utf8NameDecoder.Instance));
            var value = BsonSerializer.Deserialize<TValue>(context.Reader);
            dictionary[key] = value;
        }
        context.Reader.ReadEndDocument();
        return dictionary;
    }
}
