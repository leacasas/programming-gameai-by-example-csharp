using WestWorld.Messaging;

namespace WestWorld;

internal class BaseGameEntity
{
    internal int ID { get; set; }

    internal string Name
    {
        get
        {
            switch (ID)
            {
                case (int)WestWorldEntity.MinerBob:
                    return "Miner Bob";
                case (int)WestWorldEntity.Elsa:
                    return "Wife Elsa";
                default:
                    return "UNKNOWN";
            }
        }
    }

    internal BaseGameEntity(int id)
    {
        ID = id;
    }

    internal virtual void Update() { }

    internal virtual bool HandleMessage(Telegram message) { return false; }
}