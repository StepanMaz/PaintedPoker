using MongoDB.Bson.Serialization;
using PaintedPokerLib.Game;
using Results = PaintedPokerLib.Game.Results;

namespace PaintedPoker.Configuration;

public class MongoDBSettings
{
   public string ConnectionString { get; set; } = "";
   public string DatabaseName { get; set; } = "";
}

public static class MongoDBConfiguration
{
   public static void ConfigurePolymorphism()
   {
      BsonClassMap.RegisterClassMap<Results.RoundResult>(cm =>
      {
         cm.AutoMap();
         cm.SetIsRootClass(true);
         cm.AddKnownType(typeof(Results.DefaultRoundResult));
         cm.AddKnownType(typeof(Results.NoStakesRoundResult));
         cm.AddKnownType(typeof(Results.NoTrumpCardRoundResult));
         cm.AddKnownType(typeof(Results.BlindStakesRoundResult));
         cm.AddKnownType(typeof(Results.WinLosesRoundResult));
      });

      BsonClassMap.RegisterClassMap<Results.DefaultRoundResult>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Results.NoStakesRoundResult>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Results.NoTrumpCardRoundResult>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Results.BlindStakesRoundResult>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Results.WinLosesRoundResult>(cm => cm.AutoMap());

      BsonClassMap.RegisterClassMap<Round.RoundBase>(cm =>
      {
         cm.AutoMap();
         cm.SetIsRootClass(true);
         cm.AddKnownType(typeof(Round.DefaultRound));
         cm.AddKnownType(typeof(Round.NoStakesRound));
         cm.AddKnownType(typeof(Round.NoTrumpCardsRound));
         cm.AddKnownType(typeof(Round.BlindStakesRound));
         cm.AddKnownType(typeof(Round.WinLosesRound));
      });

      BsonClassMap.RegisterClassMap<Round.DefaultRound>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Round.NoStakesRound>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Round.NoTrumpCardsRound>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Round.BlindStakesRound>(cm => cm.AutoMap());
      BsonClassMap.RegisterClassMap<Round.WinLosesRound>(cm => cm.AutoMap());
   }
}