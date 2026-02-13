using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SimulationManager : MonoBehaviour
{
    [SerializeField] Image[] icons; 
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject schematicsMap; 
    [SerializeField] GameObject[] objects;
    [SerializeField] Transform[] spawnTransforms; 
    [SerializeField] private float[] ySpawns; 
    private Vector3[] spawnPositions;
    private int currentSelection;

    private void Start()
    {
        spawnPositions = new Vector3[2]; 
        currentSelection = 0; 
    }

    public void SetSpawnX(int id)
    {
        spawnPositions[currentSelection].x = spawnTransforms[id].position.x; 
    }

    public void SetSpawnY()
    {
        spawnPositions[currentSelection].y = ySpawns[currentSelection];
    }

    public void SetSpawnZ(int id)
    {
        spawnPositions[currentSelection].z = spawnTransforms[id].position.z;
    }

    public void IncrementId()
    {
        currentSelection++; 

        if (currentSelection == objects.Length)
        {
            StartSimulation(); 
        }
    }

    public void DeactivateButton(int id)
    {
        buttons[id].interactable = false;
        icons[currentSelection].rectTransform.position = buttons[id].GetComponent<Image>().rectTransform.position; 
    }

    public void StartSimulation()
    {
        schematicsMap.SetActive(false);

        for(int i = 0; i < 2; i++)
        {
            GameObject entity = Instantiate(objects[i], spawnPositions[i], Quaternion.Euler(0, 90, 0));

            if (i == 1)
            {
                VectorRobotController vRC = entity.GetComponentInChildren<VectorRobotController>();

                GetComponent<ControlManager>().SetVectorCams(vRC.firstPersonCam, vRC.thirdPersonCam);
            } else if (i == 0)
            {
                CatMovement cat = entity.GetComponentInChildren<CatMovement>();

                cat.SetSpawnWaypoints(spawnTransforms);

            }
        }

        ControlManager.Instance.ReinitializeCam(); 
    }

    public void BackToMenu()
    {
        MenuManager menuManager = FindFirstObjectByType<MenuManager>();

        if (menuManager != null)
        {
            Destroy(menuManager.gameObject);
        }
        
        SceneManager.LoadScene(0);
    }
}
