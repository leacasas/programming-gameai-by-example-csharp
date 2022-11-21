using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.MinersWifeStates;

internal sealed class CookStewState : State<MinersWife>
{
    private static readonly Lazy<CookStewState> _instance = new(() => new CookStewState());

    private CookStewState() { }

    internal static CookStewState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(MinersWife wife)
    {
        if (!wife.IsCooking)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{wife.Name}: Puttin' the stew in the oven!");
            Console.BackgroundColor = ConsoleColor.Black;

            MessageDispatcher.Instance.DispatchMessage(WestWorldEntity.Elsa, WestWorldEntity.Elsa, MessageType.StewReady, 1.5, null);

            wife.IsCooking = true;
        }
    }

    internal override void Execute(MinersWife wife)
    {
        if (wife.IsCooking)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{wife.Name}: Fussin' over food");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    internal override void Exit(MinersWife wife)
    {
        if (wife.IsCooking)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{wife.Name}: Puttin' the stew on the table");
            Console.BackgroundColor = ConsoleColor.Black;

            wife.IsCooking = false;
        }
    }

    internal override bool OnMessage(MinersWife wife, Telegram message)
    {
        switch (message.MessageType)
        {
            case MessageType.StewReady:
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"___ Message received by {wife.Name} at {DateTime.Now.Ticks}");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{wife.Name}: Stew's ready! Let's eat");
                    Console.BackgroundColor = ConsoleColor.Black;

                    MessageDispatcher.Instance.DispatchMessage(WestWorldEntity.Elsa, WestWorldEntity.MinerBob, MessageType.StewReady, 0, null);

                    wife.ChangeState(DoHouseWorkState.Instance);

                    return true;
                }
        }

        return false;
    }
}
