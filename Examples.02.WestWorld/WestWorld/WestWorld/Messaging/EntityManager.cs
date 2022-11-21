namespace WestWorld.Messaging;

internal class EntityManager
{
    private static readonly Lazy<EntityManager> _instance = new(() => new EntityManager());
    private readonly Dictionary<int, BaseGameEntity> _entityMap = new();

    private EntityManager()
    {
    }

    internal static EntityManager Instance
    {
        get { return _instance.Value; }
    }

    internal void RegisterEntity(BaseGameEntity entity)
    {
        if (!_entityMap.ContainsKey(entity.ID))
        {
            _entityMap.Add(entity.ID, entity);
        }
    }

    internal BaseGameEntity GetEntityFromID(int id)
    {
        _ = _entityMap.TryGetValue(id, out BaseGameEntity entity);

        return entity;
    }

    internal void RemoveEntity(BaseGameEntity entity)
    {
        _entityMap.Remove(entity.ID);
    }
}
