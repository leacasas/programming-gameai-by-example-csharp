namespace WestWorld.States;

internal sealed class QuenchThirstState : State
{
    private static readonly Lazy<QuenchThirstState> _instance = new(() => new QuenchThirstState());

    private QuenchThirstState() { }

    internal static QuenchThirstState Instance
    {
        get { return _instance.Value; }
    }

    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Saloon)
        {
            miner.ChangeLocation(LocationType.Saloon);

            Console.WriteLine($"{miner.ID} : Boy! ah sure am thirsty! Goin' to that Saloon...");
        }
    }

    internal override void Execute(Miner miner)
    {
        if (miner.IsThirsty())
        {
            miner.BuyAndDrinkWhiskey();

            Console.WriteLine($"{miner.ID} : Tis' a mighty fine sippin' liquer");

            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
        else
        {
            // no idea
        }
    }

    internal override void Exit(Miner miner)
    {
        Console.WriteLine($"{miner.ID}: Leaving the Saloon now... feelin' recovered already!");
    }
}
