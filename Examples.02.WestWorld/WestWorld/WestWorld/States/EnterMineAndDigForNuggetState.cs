﻿namespace WestWorld.States;

internal sealed class EnterMineAndDigForNuggetState : State
{
    private static readonly Lazy<EnterMineAndDigForNuggetState> _instance = new(() => new EnterMineAndDigForNuggetState());

    private EnterMineAndDigForNuggetState() { }

    internal static EnterMineAndDigForNuggetState Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// If the miner is not already located at the gold mine,
    /// change location to the gold mine. 
    /// This method should not change the state.
    /// </summary>
    /// <param name="miner">The miner</param>
    internal override void Enter(Miner miner)
    {
        if (miner.Location != LocationType.Goldmine)
        {
            Console.WriteLine($"{miner.ID}: Walkin' to the gold mine!");

            miner.ChangeLocation(LocationType.Goldmine);
        }
    }

    /// <summary>
    /// Change miner state, depending on his pockets and 
    /// his thirst levels
    /// </summary>
    /// <param name="miner">the miner</param>
    internal override void Execute(Miner miner)
    {
        // miner digs out gold nuggets.
        miner.AddToGoldCarried(1);

        // diggin' is hard work yo
        miner.IncreaseFatigue();

        Console.WriteLine($"{miner.ID}: Pickin' up a nugget...");

        // If enough gold in pockets, go to the bank and deposit them.
        if (miner.HasPocketsFull())
        {
            miner.ChangeState(VisitBankAndDepositGoldState.Instance);
        }

        // If thirsty go and get a drink
        if (miner.IsThirsty())
        {
            miner.ChangeState(QuenchThirstState.Instance);
        }
    }

    /// <summary>
    /// Miner is leaving the mine. 
    /// This method should not change the state.
    /// </summary>
    /// <param name="miner"></param>
    internal override void Exit(Miner miner)
    {
        Console.WriteLine($"{miner.ID}: I'm leaving this mine with pockets full of gold nuggets!");
    }
}
