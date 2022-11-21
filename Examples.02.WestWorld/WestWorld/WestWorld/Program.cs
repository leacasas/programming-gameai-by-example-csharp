using WestWorld.Entities;
using WestWorld.Messaging;

namespace WestWorld;

internal class Program
{
    static void Main(string[] args)
    {
        // Create and register all entities.
        var bobTheMiner = new Miner((int)WestWorldEntity.MinerBob);
        EntityManager.Instance.RegisterEntity(bobTheMiner);

        var elsaTheWife = new MinersWife((int)WestWorldEntity.Elsa);
        EntityManager.Instance.RegisterEntity(elsaTheWife);

        var joeTheBartender = new Bartender((int)WestWorldEntity.BartenderJoe);
        EntityManager.Instance.RegisterEntity(joeTheBartender);

        Console.WriteLine("Welcome to West World!");

        // Game loop, fixed iterations
        for (var i = 0; i < 50; i++)
        {
            bobTheMiner.Update();
            elsaTheWife.Update();
            joeTheBartender.Update();

            // Dispatch those delayed messages
            MessageDispatcher.Instance.DispatchDelayedMessages();
        }
    }
}