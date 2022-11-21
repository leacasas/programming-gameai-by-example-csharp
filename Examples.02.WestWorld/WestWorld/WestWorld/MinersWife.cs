using WestWorld.Messaging;
using WestWorld.States;

namespace WestWorld;

internal class MinersWife : BaseGameEntity
{
    private readonly StateMachine<MinersWife> _stateMachine;

    internal LocationType Location { get; set; }
    internal bool IsCooking { get; set; }

    internal MinersWife(int id) : base(id)
    {
        // Init
        Location = LocationType.Home;
        IsCooking = false;

        // Setting FSM
        _stateMachine = new StateMachine<MinersWife>(this)
        {
            CurrentState = DoHouseWorkState.Instance,
            GlobalState = MinersWifeGlobalState.Instance
        };
    }

    internal override void Update()
    {
        _stateMachine.Update();
    }

    internal void ChangeState(State<MinersWife> newState)
    {
        _stateMachine.ChangeState(newState);
    }

    internal void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    internal bool IsInState(State<MinersWife> state)
    {
        return _stateMachine.IsInState(state);
    }

    internal void ChangeLocation(LocationType location)
    {
        Location = location;
    }

    internal override bool HandleMessage(Telegram message)
    {
        return _stateMachine.HandleMessage(message);
    }
}