using UnityEngine;

public enum CameraState
{
    FREE_VIEW, 
    ROOM_VIEW, 
    VECTOR_3RD_PERSON, 
    VECTOR_1ST_PERSON
}

public class ControlManager : MonoBehaviour
{
    [SerializeField] private GameObject standardPostProcessing;
    [SerializeField] private GameObject vectorBotPostProcessing;
    [SerializeField] private GameObject livingRoomViewCanvas;
    [SerializeField] private Camera[] cameras;
    public CameraState currentCamState;
    private int activeCamID; 

    public static ControlManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentCamState = CameraState.FREE_VIEW;
        activeCamID = 0;
    }

    private void ChangeCameraView()
    {
        cameras[activeCamID].enabled = false; 

        switch (activeCamID) 
        {
            case 0:
                activeCamID++;
                currentCamState = CameraState.ROOM_VIEW;
                livingRoomViewCanvas.SetActive(true); 
                break;
            case 1:
                activeCamID++;
                currentCamState = CameraState.VECTOR_1ST_PERSON;
                standardPostProcessing.SetActive(false);
                vectorBotPostProcessing.SetActive(true);
                livingRoomViewCanvas.SetActive(false); 
                break;
            case 2:
                activeCamID++;
                currentCamState = CameraState.VECTOR_3RD_PERSON;
                standardPostProcessing.SetActive(true);
                vectorBotPostProcessing.SetActive(false);
                break;
            case 3:
                activeCamID = 0;
                currentCamState = CameraState.FREE_VIEW;
                break;
        }

        cameras[activeCamID].enabled = true; 
    }

    public Camera GetLivingRoomCamera()
    {
        return cameras[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCameraView(); 
        }
    }
}
