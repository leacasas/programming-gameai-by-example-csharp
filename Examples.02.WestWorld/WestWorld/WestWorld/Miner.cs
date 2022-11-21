using WestWorld.States;

namespace WestWorld;

internal class Miner : BaseGameEntity<Miner>
{
    internal bool IsWifeCooking { get; set; }

    internal int GoldCarried { get; set; }

    internal int Fatigue { get; set; }

    int _moneyInBank;
    int _thirst;

    internal Miner(int id) : base(id)
    {
        // Init
        Location = LocationType.Home;
        IsWifeCooking = false;
        GoldCarried = 0;
        _moneyInBank = 0;
        _thirst = 0;
        Fatigue = 0;

        // Setting FSM
        _stateMachine = new StateMachine<Miner>(this)
        {
            CurrentState = GoHomeAndSleepUntilRestedState.Instance,
            GlobalState = MinerGlobalState.Instance
        };
    }

    internal override void Update()
    {
        ++_thirst;

        base.Update();
    }

    internal void AddToGoldCarried(int goldToAdd)
    {
        GoldCarried += goldToAdd;

        if (GoldCarried < 0)
            GoldCarried = 0;
    }

    internal bool HasPocketsFull()
    {
        return GoldCarried >= Constants.MAX_NUGGETS;
    }

    internal void AddToWealth(int goldToAdd)
    {
        _moneyInBank += goldToAdd;

        if (_moneyInBank < 0)
            _moneyInBank = 0;
    }

    internal int Wealth()
    {
        return _moneyInBank;
    }

    internal bool IsComfy()
    {
        return _moneyInBank >= Constants.COMFORT_LEVEL;
    }

    internal bool IsFatigued()
    {
        return Fatigue > Constants.TIREDNESS_LIMIT;
    }

    internal bool IsThirsty()
    {
        return _thirst >= Constants.THIRST_LEVEL;
    }

    internal void BuyAndDrinkWhiskey()
    {
        _thirst = 0;
        _moneyInBank -= 2;
    }
}