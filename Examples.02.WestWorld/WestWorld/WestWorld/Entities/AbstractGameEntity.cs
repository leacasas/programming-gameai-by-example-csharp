using WestWorld.Messaging;

namespace WestWorld.Entities;

internal abstract class AbstractGameEntity
{
    internal int ID { get; set; }

    internal string Name
    {
        get
        {
            return ID switch
            {
                (int)WestWorldEntity.MinerBob => "Miner Bob",
                (int)WestWorldEntity.Elsa => "Wife Elsa",
                (int)WestWorldEntity.BartenderJoe => "Bartender Joe",
                _ => "UNKNOWN",
            };
        }
    }

    internal abstract void Update();

    internal abstract void ChangeLocation(LocationType location);

    internal abstract void RevertToPreviousState();

    internal abstract bool HandleMessage(Telegram message);
}