namespace WestWorld.States;

internal sealed class QuenchThirstState : State<Miner>
{
    private static readonly Lazy<QuenchThirstState> _instance = new(() => new QuenchThirstState());

    private QuenchThirstState() { }

    internal static QuenchThirstState Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// Miner enters the saloon to quench his thirst.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Saloon)
        {
            miner.ChangeLocation(LocationType.Saloon);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{miner.Name}: Boy! ah sure am thirsty! Goin' to that Saloon...");
        }
    }

    /// <summary>
    /// If the miner is thirsty, buy a drink and then go back to dig.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Execute(Miner miner)
    {
        if (miner.IsThirsty())
        {
            miner.BuyAndDrinkWhiskey();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{miner.Name}: Tis' a mighty fine sippin' liquer");

            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
        else
        {
            // no idea
        }
    }

    /// <summary>
    /// Leave the saloon
    /// </summary>
    /// <param name="miner"></param>
    internal override void Exit(Miner miner)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{miner.Name}: Leaving the Saloon now... feelin' recovered already!");
    }
}
