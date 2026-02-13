using UnityEngine;
using System.Collections.Generic; 

public enum ScenarioInstruction {
    FollowCat, 
    EatPlant
}

public class ScenarioInstructionsFactory
{
    private Dictionary<ScenarioInstruction, CatScenario> scenarioDict;

    public ScenarioInstructionsFactory()
    {
        scenarioDict = new Dictionary<ScenarioInstruction, CatScenario>();
        scenarioDict[ScenarioInstruction.FollowCat] = new FollowCatScenario();
        scenarioDict[ScenarioInstruction.FollowCat].InitializeScenario();
        scenarioDict[ScenarioInstruction.EatPlant] = new EatPlantScenario();
        scenarioDict[ScenarioInstruction.EatPlant].InitializeScenario();
    }

    public CatScenario GetScenario(ScenarioInstruction instruction)
    {
        return scenarioDict[instruction]; 
    }
}
