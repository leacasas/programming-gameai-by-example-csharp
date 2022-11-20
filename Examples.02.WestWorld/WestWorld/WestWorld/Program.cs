namespace WestWorld;

internal class Program
{
    static void Main(string[] args)
    {
        var miner = new Miner((int)WestWorldEntity.MinerBob);

        Console.WriteLine("Hello, West World!");

        for (var i = 0; i < 20; i++)
        {
            miner.Update();
        }
    }
}