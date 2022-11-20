namespace WestWorld.States;

internal sealed class VisitBankAndDepositGoldState : State<Miner>
{
    private static readonly Lazy<VisitBankAndDepositGoldState> _instance = new(() => new VisitBankAndDepositGoldState());

    private VisitBankAndDepositGoldState() { }

    internal static VisitBankAndDepositGoldState Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// Miner enters the bank
    /// </summary>
    /// <param name="miner">The miner</param>
    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Bank)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{miner.Name}: Goin' to the bank. Yes siree!");

            miner.ChangeLocation(LocationType.Bank);
        }
    }

    /// <summary>
    /// Deposit gold in bank. If wealthy enough, miner goes home to rest. If not, 
    /// goes to the mine to keep digging.
    /// </summary>
    /// <param name="miner">the miner</param>
    internal override void Execute(Miner miner)
    {
        //deposit gold
        miner.AddToWealth(miner.GoldCarried());

        miner.SetGoldCarried(0);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{miner.Name}: Depositing gold. My total savings are {miner.Wealth()} gold nuggets.");

        //wealthy enough to rest?
        if (miner.IsComfy())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{miner.Name}: Woohoo! Rich enough for now. Back home to mah lil' lady!");

            miner.ChangeState(GoHomeAndSleepUntilRestedState.Instance);
        }
        else //otherwise, go get more gold
        {
            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
    }

    internal override void Exit(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{miner.Name}: Leavin' the bank now");
    }
}