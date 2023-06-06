using Zenject;

namespace TapTapTap.Core.FSM
{
    public class Blackboard
    {
        public Entity TargetEntity { get; set; }

        public class Factory : PlaceholderFactory<Blackboard>
        {
        }
    }
}