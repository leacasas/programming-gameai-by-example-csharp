using WestWorld.Messaging;

namespace WestWorld;

internal class Program
{
    static void Main(string[] args)
    {
        var bobTheMiner = new Miner((int)WestWorldEntity.MinerBob);
        EntityManager.Instance.RegisterEntity(bobTheMiner);

        var elsaTheWife = new MinersWife((int)WestWorldEntity.Elsa);
        EntityManager.Instance.RegisterEntity(elsaTheWife);

        Console.WriteLine("Welcome to West World!");

        for (var i = 0; i < 50; i++)
        {
            bobTheMiner.Update();
            elsaTheWife.Update();

            // Dispatch those delayed messages
            MessageDispatcher.Instance.DispatchDelayedMessages();
        }
    }
}