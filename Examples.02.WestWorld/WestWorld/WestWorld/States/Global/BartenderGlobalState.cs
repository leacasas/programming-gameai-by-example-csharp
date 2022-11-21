using WestWorld.Entities;
using WestWorld.Messaging;
using WestWorld.States.BartenderStates;

namespace WestWorld.States.Global;

internal sealed class BartenderGlobalState : State<Bartender>
{
    private static readonly Lazy<BartenderGlobalState> _instance = new(() => new BartenderGlobalState());

    private BartenderGlobalState() { }

    internal static BartenderGlobalState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Bartender bartender)
    {

    }

    internal override void Execute(Bartender bartender)
    {
        if (Random.Shared.Next(0, 5) <= 1)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{bartender.Name}: Today seems to be a slooow day...");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    internal override void Exit(Bartender bartender)
    {

    }

    internal override bool OnMessage(Bartender bartender, Telegram message)
    {
        switch (message.MessageType)
        {
            case MessageType.PourMeAWhiskey:
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"___ Message handled by {bartender.Name} at {DateTime.Now.Ticks}");
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{bartender.Name}: Hi there sir! Whiskey comin' right up");
                    Console.BackgroundColor = ConsoleColor.Black;

                    bartender.NuggetsEarned += 2; //payment from Miner
                    bartender.ChangeState(ServingWhiskeyState.Instance);
                    return true;
                }
        }

        return false;
    }
}