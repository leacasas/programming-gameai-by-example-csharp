using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.MinersWifeStates;

internal sealed class DoHouseWorkState : State<MinersWife>
{
    private static readonly Lazy<DoHouseWorkState> _instance = new(() => new DoHouseWorkState());

    private DoHouseWorkState() { }

    internal static DoHouseWorkState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(MinersWife wife)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{wife.Name}: Time to do some more housework!");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Execute(MinersWife wife)
    {
        var rnd = Random.Shared.Next(0, 3);

        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;

        switch (rnd)
        {
            case 0:
                Console.WriteLine($"{wife.Name}: Moppin' the floor!");
                break;
            case 1:
                Console.WriteLine($"{wife.Name}: Washin' the dishes!");
                break;
            case 2:
                Console.WriteLine($"{wife.Name}: Makin' the bed!");
                break;
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Exit(MinersWife wife)
    {

    }

    internal override bool OnMessage(MinersWife wife, Telegram message)
    {
        return false;
    }
}
