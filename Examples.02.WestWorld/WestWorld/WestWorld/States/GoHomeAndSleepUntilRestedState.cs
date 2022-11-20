namespace WestWorld.States;

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
            Console.WriteLine($"{miner.Name}: Walkin' home...");

            miner.ChangeLocation(LocationType.Home);
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{miner.Name}: What a god darn fantastic nap! Time to find more gold eh!");

            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
        else // sleep
        {
            miner.DecreaseFatigue();

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
}