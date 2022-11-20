namespace WestWorld;

internal class BaseGameEntity
{
    internal int ID { get; set; }

    internal BaseGameEntity(int id)
    {
        ID = id;
    }

    internal virtual void Update() { }
}