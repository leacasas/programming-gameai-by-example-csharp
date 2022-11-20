namespace WestWorld.States;

internal class State
{
    internal virtual void Enter(Miner miner) { }
    internal virtual void Execute(Miner miner) { }
    internal virtual void Exit(Miner miner) { }
}
