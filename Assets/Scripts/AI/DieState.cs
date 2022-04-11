using System.Collections;
using UnityEngine;

class DieState : State
{
    public DieState(AIController controller) : base(controller)
    {

    }

    public override IEnumerator Start()
    {
        controller.SetAnim("die", true);
        yield return null;
    }
}
