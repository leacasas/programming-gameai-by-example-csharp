namespace WestWorld.Messaging;

internal struct Telegram
{
    readonly WestWorldEntity _sender;
    internal readonly WestWorldEntity Receiver;
    internal readonly MessageType MessageType;
    internal double DispatchTime;
    readonly object _data;

    internal Telegram(WestWorldEntity sender, WestWorldEntity receiver, MessageType messageType, double dispatchTime, object? data = null)
    {
        _sender = sender;
        Receiver = receiver;
        MessageType = messageType;
        DispatchTime = dispatchTime;
        _data = data;
    }
}