using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.BartenderStates;

internal sealed class CountingMoneyState : State<Bartender>
{
    private static readonly Lazy<CountingMoneyState> _instance = new(() => new CountingMoneyState());

    private CountingMoneyState() { }

    internal static CountingMoneyState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Bartender bartender)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{bartender.Name}: I gotta see how much dough we have...");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Execute(Bartender bartender)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{bartender.Name}: uhm, I have {bartender.NuggetsEarned} nuggets");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Exit(Bartender bartender)
    {
    }

    internal override bool OnMessage(Bartender bartender, Telegram message)
    {
        return false;
    }
}