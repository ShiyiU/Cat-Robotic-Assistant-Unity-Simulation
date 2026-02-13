using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menuContainer;
    [SerializeField] GameObject testModeContainer;
    private ScenarioInstructionsFactory instructions;
    private ScenarioInstruction scenarioChoice; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructions = new ScenarioInstructionsFactory(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSandboxMode()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectTestingMode()
    {
        testModeContainer.SetActive(true);
        menuContainer.SetActive(false);
    }

    public void CancelTestingMode()
    {
        testModeContainer.SetActive(false);
        menuContainer.SetActive(true);
    }

    public void GoToTest(int scenarioID)
    {
        switch (scenarioID) 
        {
            case 0:
                scenarioChoice = ScenarioInstruction.FollowCat;
                break;
            case 1:
                scenarioChoice = ScenarioInstruction.EatPlant; 
                break;
        }

        DontDestroyOnLoad(this);
        SceneManager.LoadScene(2); 
    }

    public CatScenario GetScenario()
    {
        Debug.Log("Getting scenario");
        Debug.Log(instructions.GetScenario(scenarioChoice));
        return instructions.GetScenario(scenarioChoice); 
    }
}
