using WestWorld.Messaging;

namespace WestWorld.States;

internal sealed class VisitBathroomState : State<MinersWife>
{
    private static readonly Lazy<VisitBathroomState> _instance = new(() => new VisitBathroomState());

    private VisitBathroomState() { }

    internal static VisitBathroomState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(MinersWife wife)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{wife.Name}: Walkin' to the can. Need to powda mah pretty li'lle nose...");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Execute(MinersWife wife)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{wife.Name}: Ahhh sweet relief!");
        Console.BackgroundColor = ConsoleColor.Black;

        wife.RevertToPreviousState();
    }

    internal override void Exit(MinersWife wife)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{wife.Name}: Leavin' the Jon...");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override bool OnMessage(MinersWife wife, Telegram message)
    {
        return false;
    }
}