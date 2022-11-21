using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.MinerStates;

internal class EatStewState : State<Miner>
{
    private static readonly Lazy<EatStewState> _instance = new(() => new EatStewState());

    private EatStewState() { }

    internal static EatStewState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{miner.Name} : Smells reeeal good Elsa!");
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal override void Execute(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{miner.Name} : It is delicious!");
        Console.ForegroundColor = ConsoleColor.White;

        miner.RevertToPreviousState();
    }

    internal override void Exit(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{miner.Name} : \"Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
        Console.ForegroundColor = ConsoleColor.White;

        miner.IsWifeCooking = false;
    }

    internal override bool OnMessage(Miner miner, Telegram message)
    {
        return false;
    }
}
