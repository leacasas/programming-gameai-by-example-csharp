using WestWorld.States;

namespace WestWorld;

internal class Miner : BaseGameEntity
{
    const int COMFORT_LEVEL = 5;
    const int MAX_NUGGETS = 3;
    const int THIRST_LEVEL = 5;
    const int TIREDNESS_LIMIT = 5;

    State _currentState;
    internal LocationType Location { get; set; }

    int _goldCarried;
    int _moneyInBank;
    int _thirst;
    int _fatigue;

    internal Miner(int id) : base(id)
    {
        Location = LocationType.Home;
        _goldCarried = 0;
        _moneyInBank = 0;
        _thirst = 0;
        _fatigue = 0;
        _currentState = GoHomeAndSleepUntilRestedState.Instance;
    }

    internal override void Update()
    {
        _thirst++;

        _currentState.Execute(this);
    }

    internal void ChangeState(State newState)
    {
        // State machine logic

        // 1) Exit state
        _currentState.Exit(this);
        // 2) Reassign new state
        _currentState = newState;
        // 3) Enter new state
        _currentState.Enter(this);
    }

    internal void ChangeLocation(LocationType location)
    {
        Location = location;
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
        return _goldCarried >= MAX_NUGGETS;
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
        return _moneyInBank >= COMFORT_LEVEL;
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
        return _fatigue > TIREDNESS_LIMIT;
    }

    internal bool IsThirsty()
    {
        return _thirst >= THIRST_LEVEL;
    }

    internal void BuyAndDrinkWhiskey()
    {
        _thirst = 0;
        _moneyInBank -= 2;
    }
}