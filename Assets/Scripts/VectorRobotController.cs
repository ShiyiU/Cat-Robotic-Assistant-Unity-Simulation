using UnityEngine;
using System.Collections;
using System.IO;

public class VectorRobotController : MonoBehaviour
{
    [SerializeField] private float headRotateRate; 
    [SerializeField] private float wheelForce;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform robotHead;
    [SerializeField] private Camera robotCamera; 
    [SerializeField] private Rigidbody[] leftWheels;
    [SerializeField] private Rigidbody[] rightWheels;
    private int screenShotIndex = 0;
    private int screenshotWidth = 640;
    private int screenshotHeight = 360; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CaptureRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (ControlManager.Instance.currentCamState != CameraState.VECTOR_3RD_PERSON && ControlManager.Instance.currentCamState != CameraState.VECTOR_1ST_PERSON)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = spawnPos.position;
            transform.rotation = Quaternion.identity;
        }
    }

    private void FixedUpdate()
    {
        if (ControlManager.Instance.currentCamState != CameraState.VECTOR_3RD_PERSON && ControlManager.Instance.currentCamState != CameraState.VECTOR_1ST_PERSON)
        {
            return; 
        }

        if (Input.GetKey(KeyCode.W))
        {
            MoveWheels(leftWheels, wheelForce);
            MoveWheels(rightWheels, wheelForce);
        } else if (Input.GetKey(KeyCode.S))
        {
            MoveWheels(leftWheels, -wheelForce);
            MoveWheels(rightWheels, -wheelForce);
        } else if (Input.GetKey(KeyCode.A))
        {
            MoveWheels(leftWheels, -wheelForce);
            MoveWheels(rightWheels, wheelForce);
        } else if (Input.GetKey(KeyCode.D))
        {
            MoveWheels(leftWheels, wheelForce);
            MoveWheels(rightWheels, -wheelForce);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            AdjustHead(-headRotateRate);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            AdjustHead(headRotateRate);
        }
    }

    private void AdjustHead(float amount)
    {
        Vector3 angles = robotHead.transform.localEulerAngles;

        // Note: Unity stores angles in 0–360 degrees, not -180 to 180
        float zAngle = angles.z;

        // Apply rotation amount
        zAngle += amount * Time.deltaTime;

        // Clamp between 0 and 45
        zAngle = Mathf.Clamp(zAngle, 325f, 359.5f);

        // Assign back
        angles.z = zAngle;
        robotHead.transform.localEulerAngles = angles;
    }

    private void MoveWheels(Rigidbody[] wheels, float force)
    {
        foreach(Rigidbody wheel in wheels)
        {
            wheel.AddTorque(wheel.transform.right * force);
        }
    }

    private IEnumerator CaptureRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("Capturing..");

            // Create a temporary RenderTexture
            RenderTexture rt = new RenderTexture(screenshotWidth, screenshotHeight, 24);
            robotCamera.targetTexture = rt;

            // Render camera to RenderTexture
            robotCamera.Render();

            // Read pixels into Texture2D
            RenderTexture.active = rt;
            Texture2D screenshot = new Texture2D(screenshotWidth, screenshotHeight, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, screenshotWidth, screenshotHeight), 0, 0);
            screenshot.Apply();

            // Reset camera
            robotCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            // Save PNG
            byte[] bytes = screenshot.EncodeToPNG();
            string filename = Path.Combine(Application.persistentDataPath, $"VectorShot_{screenShotIndex}.png");
            File.WriteAllBytes(filename, bytes);
            screenShotIndex++;

            Debug.Log($"Saved screenshot: {filename}");
        }
    }
}
