using UnityEngine;

public abstract class CatScenario
{
    protected bool loopIndefinitely;
    protected int toggleID;
    protected CatMovement catMovement; 

    public abstract void InitializeScenario();

    public abstract void UpdateScenario();

    public abstract void SetupScenario(CatMovement cat); 

    public int GetToggleID()
    {
        return toggleID; 
    } 

    public bool GetLoopBool()
    {
        return loopIndefinitely; 
    }
}
