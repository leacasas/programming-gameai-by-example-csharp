using WestWorld.States;

namespace WestWorld;

internal class MinersWife : BaseGameEntity<MinersWife>
{
    internal bool IsCooking { get; set; }

    internal MinersWife(int id) : base(id)
    {
        // Init
        Location = LocationType.Home;
        IsCooking = false;

        // Setting FSM
        _stateMachine = new StateMachine<MinersWife>(this)
        {
            CurrentState = DoHouseWorkState.Instance,
            GlobalState = MinersWifeGlobalState.Instance
        };
    }
}