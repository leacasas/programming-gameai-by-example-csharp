using WestWorld.Messaging;

namespace WestWorld.States;

internal class State<T> where T : class
{
    internal virtual void Enter(T entity) { }
    internal virtual void Execute(T entity) { }
    internal virtual void Exit(T entity) { }

    internal virtual bool OnMessage(T entity, Telegram message) { return false; }
}
