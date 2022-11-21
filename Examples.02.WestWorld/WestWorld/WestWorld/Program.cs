namespace WestWorld;

internal class Program
{
    static void Main(string[] args)
    {
        var bobTheMiner = new Miner((int)WestWorldEntity.MinerBob);
        var elsaTheWife = new MinersWife((int)WestWorldEntity.Elsa);

        Console.WriteLine("Hello, West World!");

        for (var i = 0; i < 50; i++)
        {
            bobTheMiner.Update();
            elsaTheWife.Update();
        }
    }
}