using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.BartenderStates;

internal sealed class ServingWhiskeyState : State<Bartender>
{
    private static readonly Lazy<ServingWhiskeyState> _instance = new(() => new ServingWhiskeyState());

    private ServingWhiskeyState() { }

    internal static ServingWhiskeyState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Bartender bartender)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{bartender.Name}: I gotta a special one today...");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override void Execute(Bartender bartender)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{bartender.Name}: Here! Enjoy sir!");
        Console.BackgroundColor = ConsoleColor.Black;

        bartender.RevertToPreviousState();
    }

    internal override void Exit(Bartender bartender)
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{bartender.Name}: Come back any time mister");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    internal override bool OnMessage(Bartender bartender, Telegram message)
    {
        return false;
    }
}