using WestWorld.States;
using WestWorld.States.BartenderStates;
using WestWorld.States.Global;

namespace WestWorld.Entities;

internal class Bartender : BaseGameEntity<Bartender>
{
    internal int NuggetsEarned { get; set; }

    internal Bartender(int id) : base(id)
    {
        // Init
        Location = LocationType.Saloon;

        // Setting FSM
        _stateMachine = new StateMachine<Bartender>(this)
        {
            CurrentState = CountingMoneyState.Instance,
            GlobalState = BartenderGlobalState.Instance
        };
    }
}