namespace WestWorld.States;

internal sealed class GoHomeAndSleepUntilRestedState : State
{
    private static readonly Lazy<GoHomeAndSleepUntilRestedState> _instance = new(() => new GoHomeAndSleepUntilRestedState());

    private GoHomeAndSleepUntilRestedState() { }

    internal static GoHomeAndSleepUntilRestedState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Home)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{miner.Name}: Walkin' home...");

            miner.ChangeLocation(LocationType.Home);
        }
    }

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

    internal override void Exit(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{miner.Name}: Leaving the house...");
    }
}