using System.Reflection;

namespace WestWorld.Messaging;

internal class MessageDispatcher
{
    private static readonly Lazy<MessageDispatcher> _instance = new(() => new MessageDispatcher());
    private readonly PriorityQueue<Telegram, double> _messageQueue; // messages sorted by their dispatch time

    private MessageDispatcher()
    {
        _messageQueue = new PriorityQueue<Telegram, double>();
    }

    internal static MessageDispatcher Instance
    {
        get { return _instance.Value; }
    }

    /// <summary>
    /// Sends a message to another agent
    /// </summary>
    internal void DispatchMessage(WestWorldEntity sender, WestWorldEntity receiver, MessageType messageType, double delay, object? extraInfo = null)
    {
        // Get received from the EntityManager (the database of entities).
        BaseGameEntity receiverEntity = EntityManager.Instance.GetEntityFromID((int)receiver);

        // Create the message
        Telegram newMessage = new Telegram(sender, receiver, messageType, delay, extraInfo);

        // If there's no delay, route the message immediately
        if (delay >= 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"___ Message dispatched at time: {DateTime.Now.Ticks} by {sender} for {receiver}. Message is {messageType}");
            Console.ForegroundColor = ConsoleColor.White;

            Discharge(receiverEntity, newMessage);
        }
        else // otherwise, calculate the time when it should be sent and queue it for dispatch
        {
            double currentTime = DateTime.Now.Ticks;

            newMessage.DispatchTime = currentTime + delay;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"___ Delayed message recorded at time: {DateTime.Now.Ticks} by {sender} for {receiver}. Message is {messageType}");
            Console.ForegroundColor = ConsoleColor.White;

            _messageQueue.Enqueue(newMessage, newMessage.DispatchTime);
        }
    }

    /// <summary>
    /// Send out any delayed messages. This method is called each time in the game loop.
    /// </summary>
    internal void DispatchDelayedMessages()
    {
        // first get current time
        double currentTime = DateTime.Now.Ticks;

        // now peek at the queue to see if any messages need dispatching.
        // remove all telegrams from the front of the queue that have gone beyond their dispatch time
        while (_messageQueue.Count > 0 && _messageQueue.Peek().DispatchTime < currentTime)
        {
            // dequeue and read message in front of queue
            var nextMessage = _messageQueue.Dequeue();

            // find the recipient
            BaseGameEntity recipient = EntityManager.Instance.GetEntityFromID((int)nextMessage.Receiver);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"___ Queued message ready for dispatch. Sent to {nextMessage.Receiver}. Message is {nextMessage.MessageType}");
            Console.ForegroundColor = ConsoleColor.White;

            // send the message to the recipient
            Discharge(recipient, nextMessage);
        }
    }

    /// <summary>
    /// Used by DispatchMessage or DispatchDelayedMessage
    /// Calls the message handling member of the receiver, with the newly created message.
    /// </summary>
    /// <param name="receiver">The receiver that will handle the message</param>
    /// <param name="message">The message being sent</param>
    private void Discharge(BaseGameEntity receiver, Telegram message)
    {
        if (!receiver.HandleMessage(message))
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Discharged message not handled by receiver entity");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}