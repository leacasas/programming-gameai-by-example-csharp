using WestWorld.Messaging;

namespace WestWorld.States;

internal sealed class MinersWifeGlobalState : State<MinersWife>
{
    private static readonly Lazy<MinersWifeGlobalState> _instance = new(() => new MinersWifeGlobalState());

    private MinersWifeGlobalState() { }

    internal static MinersWifeGlobalState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(MinersWife wife)
    {

    }

    internal override void Execute(MinersWife wife)
    {
        if (Random.Shared.Next(0, 10) <= 1 && !wife.IsInState(VisitBathroomState.Instance))
            wife.ChangeState(VisitBathroomState.Instance);
    }

    internal override void Exit(MinersWife wife)
    {

    }

    internal override bool OnMessage(MinersWife wife, Telegram message)
    {
        switch (message.MessageType)
        {
            case MessageType.HiHoneyImHome:
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"___ Message handled by {wife.Name} at {DateTime.Now.Ticks}");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{wife.Name}: Hi honey, let me cook you some of mah fine country stew");
                    Console.BackgroundColor = ConsoleColor.Black;

                    wife.ChangeState(CookStewState.Instance);
                    return true;
                }
        }

        return false;
    }
}
