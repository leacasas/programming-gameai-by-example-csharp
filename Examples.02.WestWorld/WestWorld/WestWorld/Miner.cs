using WestWorld.Messaging;
using WestWorld.States;

namespace WestWorld;

internal class Miner : BaseGameEntity
{
    const int COMFORT_LEVEL = 5;
    const int MAX_NUGGETS = 3;
    const int THIRST_LEVEL = 5;
    const int TIREDNESS_LIMIT = 5;

    private readonly StateMachine<Miner> _stateMachine;

    internal LocationType Location { get; set; }

    int _goldCarried;
    int _moneyInBank;
    int _thirst;
    int _fatigue;

    internal Miner(int id) : base(id)
    {
        // Init
        Location = LocationType.Home;
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

        _stateMachine.Update();
    }

    internal void ChangeState(State<Miner> newState)
    {
        _stateMachine.ChangeState(newState);
    }

    internal void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    internal void ChangeLocation(LocationType location)
    {
        Location = location;
    }

    internal override bool HandleMessage(Telegram message)
    {
        return _stateMachine.HandleMessage(message);
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