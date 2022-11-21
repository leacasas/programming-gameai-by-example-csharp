using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld.States.MinerStates;

internal sealed class GoHomeAndSleepUntilRestedState : State<Miner>
{
    private static readonly Lazy<GoHomeAndSleepUntilRestedState> _instance = new(() => new GoHomeAndSleepUntilRestedState());

    private GoHomeAndSleepUntilRestedState() { }

    internal static GoHomeAndSleepUntilRestedState Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// Enter the home.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Home)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{miner.Name}: Walkin' home to take a lil' nap...");

            miner.ChangeLocation(LocationType.Home);

            // Dispatch message to Elsa
            MessageDispatcher.Instance.DispatchMessage((WestWorldEntity)miner.ID, WestWorldEntity.Elsa, MessageType.HiHoneyImHome, 0, null);
        }
    }

    /// <summary>
    /// If the miner is not fatigued (maybe just rested), go back to the gold mine.
    /// If he is, rest until fatigue dissappears.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Execute(Miner miner)
    {
        //if miner is not fatigued start diggin'
        if (!miner.IsFatigued())
        {
            if (miner.IsWifeCooking)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{miner.Name}: What a god darn fantastic nap! I'm hungry for that stew hun'");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{miner.Name}: What a god darn fantastic nap! Time to find more gold eh!");

                miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
            }
        }
        else // sleep
        {
            miner.Fatigue--;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{miner.Name}: zzz... zzz... zzz...");
        }
    }

    /// <summary>
    /// Leave the house.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Exit(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{miner.Name}: Leaving the house...");
    }

    internal override bool OnMessage(Miner miner, Telegram message)
    {
        switch (message.MessageType)
        {
            case MessageType.StewReady:
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"___ Message received by {miner.Name} at {DateTime.Now.Ticks}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{miner.Name}: Okay hun, ahm a-comin'!");

                    miner.IsWifeCooking = true;
                    miner.ChangeState(EatStewState.Instance);

                    return true;
                }
        }

        return false;
    }
}