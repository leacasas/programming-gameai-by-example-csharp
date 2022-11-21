using WestWorld.States;

namespace WestWorld;

internal class Miner : BaseGameEntity<Miner>
{
    internal bool IsWifeCooking { get; set; }

    int _goldCarried;
    int _moneyInBank;
    int _thirst;
    int _fatigue;

    internal Miner(int id) : base(id)
    {
        // Init
        Location = LocationType.Home;
        IsWifeCooking = false;
        _goldCarried = 0;
        _moneyInBank = 0;
        _thirst = 0;
        _fatigue = 0;

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
        _goldCarried += goldToAdd;

        if (_goldCarried < 0)
            _goldCarried = 0;
    }

    internal int GoldCarried()
    {
        return _goldCarried;
    }

    internal bool HasPocketsFull()
    {
        return _goldCarried >= Constants.MAX_NUGGETS;
    }

    internal void AddToWealth(int goldToAdd)
    {
        _moneyInBank += goldToAdd;

        if (_moneyInBank < 0)
            _moneyInBank = 0;
    }

    internal void SetGoldCarried(int goldToSet)
    {
        _goldCarried = goldToSet;
    }

    internal int Wealth()
    {
        return _moneyInBank;
    }

    internal bool IsComfy()
    {
        return _moneyInBank >= Constants.COMFORT_LEVEL;
    }

    internal void IncreaseFatigue()
    {
        _fatigue++;
    }
    internal void DecreaseFatigue()
    {
        _fatigue--;
    }
    internal bool IsFatigued()
    {
        return _fatigue > Constants.TIREDNESS_LIMIT;
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