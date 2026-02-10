using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject foodButton;
    [SerializeField] private GameObject food; 
    [SerializeField] private GameObject housePlants;
    [SerializeField] private GameObject dishware;
    [SerializeField] private GameObject toaster;
    [SerializeField] private GameObject electricalHazards; 
    [SerializeField] private GameObject smallObjSpawnBoundsObj;
    [SerializeField] private GameObject[] smallObjects; 
    [SerializeField] private GameObject[] windows;
    [SerializeField] private GameObject[] stoveFires;
    [SerializeField] private GameObject[] doors; 
    [SerializeField] private Transform[] windowsClosedPositions;
    [SerializeField] private Transform[] windowsOpenPositions;
    [SerializeField] private float[] openDoorAngle = new float[2];
    private bool[] windowsToggles = new bool[2];
    private bool[] doorToggles = new bool[4];
    private BoxCollider[] spawnboundsColliders; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnboundsColliders = smallObjSpawnBoundsObj.GetComponents<BoxCollider>();

        for(int i = 0; i < windowsToggles.Length; i++)
        {
            windowsToggles[i] = true;
        }

        for (int i = 0; i < doorToggles.Length; i++)
        {
            doorToggles[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetRandomSpawnPointFromBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.max.y;

        return new Vector3(x, y, z);
    }
    
    public void ToggleOutlets()
    {
        electricalHazards.SetActive(!electricalHazards.activeSelf);
    }

    public void ToggleDoors(int id)
    {
        doorToggles[id] = !doorToggles[id];

        if (!doorToggles[id])
        {
            doors[id].transform.rotation = Quaternion.Euler(0, openDoorAngle[id], 0);
        } else
        {
            doors[id].transform.rotation = Quaternion.identity;
        }
    }

    public void ToggleToaster()
    {
        toaster.SetActive(!toaster.activeSelf);
    }

    public void ToggleStoveFire(int id)
    {
        stoveFires[id].SetActive(!stoveFires[id].activeSelf); 
    }

    public void SpawnSmallObject(int id)
    {
        int spawnboxChoice = Random.Range(0, spawnboundsColliders.Length);

        Vector3 spawnPos = GetRandomSpawnPointFromBounds(spawnboundsColliders[spawnboxChoice].bounds);

        Instantiate(smallObjects[id], spawnPos, Quaternion.identity);
    }

    public void ClearSmallObjects()
    {

    }

    public void ToggleWindow(int id)
    {
        windowsToggles[id] = !windowsToggles[id];

        if (!windowsToggles[id])
        {
            windows[id].transform.position = windowsOpenPositions[id].transform.position; 
        } else
        {
            windows[id].transform.position = windowsClosedPositions[id].transform.position;
        }
    }

    public void ToggleFood()
    {
        food.SetActive(!food.activeSelf);
    }

    public void ToggleDishware()
    {
        dishware.SetActive(!dishware.activeSelf);
        foodButton.SetActive(!foodButton.activeSelf);
    }

    public void TogglePlants()
    {
        housePlants.SetActive(!housePlants.activeSelf);
    }
}
