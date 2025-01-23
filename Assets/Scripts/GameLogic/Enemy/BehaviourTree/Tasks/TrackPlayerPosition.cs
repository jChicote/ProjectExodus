using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Track Player Position")]
    public class TrackPlayerPosition : Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }

}