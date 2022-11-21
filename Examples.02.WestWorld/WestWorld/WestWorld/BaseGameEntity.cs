using WestWorld.Messaging;
using WestWorld.States;

namespace WestWorld;

internal class BaseGameEntity<T> : AbstractGameEntity where T : class
{
    protected StateMachine<T> _stateMachine;

    internal LocationType Location { get; set; }

    internal BaseGameEntity(int id)
    {
        ID = id;
    }

    internal override void Update()
    {
        _stateMachine.Update();
    }

    internal override void ChangeLocation(LocationType location)
    {
        Location = location;
    }

    internal void ChangeState(State<T> newState)
    {
        _stateMachine.ChangeState(newState);
    }

    internal override void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    internal bool IsInState(State<T> state)
    {
        return _stateMachine.IsInState(state);
    }

    internal override bool HandleMessage(Telegram message)
    {
        return _stateMachine.HandleMessage(message);
    }
}