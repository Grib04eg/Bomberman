using System.Collections;

public abstract class State
{
    protected readonly AIController controller;

    public State(AIController controller)
    {
        this.controller = controller;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
}