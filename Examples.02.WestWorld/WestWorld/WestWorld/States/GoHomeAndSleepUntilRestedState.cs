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
            Console.WriteLine($"{miner.ID}: Walkin' home...");

            miner.ChangeLocation(LocationType.Home);
        }
    }

    internal override void Execute(Miner miner)
    {
        //if miner is not fatigued start diggin'
        if (!miner.IsFatigued())
        {
            Console.WriteLine($"{miner.ID} : What a god darn fantastic nap! Time to find more gold eh!");

            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
        else // sleep
        {
            miner.DecreaseFatigue();

            Console.WriteLine($"{miner.ID}: zzz... zzz... zzz...");
        }
    }

    internal override void Exit(Miner miner)
    {
        Console.WriteLine($"{miner.ID}: Leaving the house...");
    }
}