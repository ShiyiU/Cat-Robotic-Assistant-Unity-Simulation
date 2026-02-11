using UnityEngine;

public class MarkerControls : MonoBehaviour
{
    Camera targetCam;
    Transform targetObj; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCamera(Camera cam)
    {
        targetCam = cam; 
    }

    public void SetTargetObj(GameObject target)
    {
        targetObj = target.transform; 
    }

    private void LateUpdate()
    {
        if (targetCam == null)
        {
            return; 
        }

        transform.LookAt(targetCam.transform);
        transform.position = targetObj.position; 
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // optional: lock rotation axes
    }
}
