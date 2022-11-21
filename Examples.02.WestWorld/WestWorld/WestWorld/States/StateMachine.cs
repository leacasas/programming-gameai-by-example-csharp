using WestWorld.Messaging;

namespace WestWorld.States;

internal class StateMachine<T> where T : class
{
    private readonly T _owner;
    internal State<T> CurrentState { get; set; }
    internal State<T> PreviousState { get; set; }
    internal State<T> GlobalState { get; set; }

    internal StateMachine(T owner)
    {
        _owner = owner;
    }

    internal void Update()
    {
        GlobalState?.Execute(_owner);
        CurrentState?.Execute(_owner);
    }

    internal void ChangeState(State<T> state)
    {
        // Record previous state
        PreviousState = CurrentState;
        // Exiting current state
        CurrentState.Exit(_owner);
        // Changing state
        CurrentState = state;
        // Entering new state
        CurrentState.Enter(_owner);
    }

    internal void RevertToPreviousState()
    {
        ChangeState(PreviousState);
    }

    internal bool IsInState(State<T> state)
    {
        return CurrentState == state;
    }

    internal bool HandleMessage(Telegram message)
    {
        // First, see if current state is valid and can handle the message
        if (CurrentState != null && CurrentState.OnMessage(_owner, message))
        {
            return true;
        }

        // If not, and if global state is available, send msg to global state
        if (GlobalState != null && GlobalState.OnMessage(_owner, message))
        {
            return true;
        }

        return false;
    }
}
