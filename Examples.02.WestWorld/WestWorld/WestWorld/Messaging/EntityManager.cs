namespace WestWorld.Messaging;

internal class EntityManager
{
    private static readonly Lazy<EntityManager> _instance = new(() => new EntityManager());
    private readonly Dictionary<int, AbstractGameEntity> _entityMap = new();

    private EntityManager()
    {
    }

    internal static EntityManager Instance
    {
        get { return _instance.Value; }
    }

    internal void RegisterEntity(AbstractGameEntity entity)
    {
        if (!_entityMap.ContainsKey(entity.ID))
        {
            _entityMap.Add(entity.ID, entity);
        }
    }

    internal AbstractGameEntity GetEntityFromID(int id)
    {
        _ = _entityMap.TryGetValue(id, out AbstractGameEntity entity);

        return entity;
    }

    internal void RemoveEntity(AbstractGameEntity entity)
    {
        _entityMap.Remove(entity.ID);
    }
}
