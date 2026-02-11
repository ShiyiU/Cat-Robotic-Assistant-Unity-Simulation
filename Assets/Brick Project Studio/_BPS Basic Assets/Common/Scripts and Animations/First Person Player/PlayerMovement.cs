using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveForce = 35f;
        public float verticalForce = 35f;
        public float maxSpeed = 8f;
        public float damping = 10f;
        public float rotateRate = 10f; 

        private Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.freezeRotation = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        void FixedUpdate()
        {
            if (ControlManager.Instance.currentCamState != CameraState.FREE_VIEW)
            {
                return; 
            }

            float x = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);
            float z = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? - 1 : 0);

            float y = 0f;
            if (Input.GetKey(KeyCode.Space)) y += 1f;
            if (Input.GetKey(KeyCode.LeftControl)) y -= 1f;

            Vector3 inputDir =
                transform.right * x +
                transform.forward * z +
                Vector3.up * y;

            // APPLY MOVEMENT FORCE
            if (inputDir.sqrMagnitude > 0.001f)
            {
                rb.AddForce(inputDir.normalized * moveForce, ForceMode.Acceleration);
            }
            else
            {
                // ACTIVE DAMPING (kills drift)
                Vector3 dampingForce = -rb.linearVelocity * damping;
                rb.AddForce(dampingForce, ForceMode.Acceleration);
            }

            // SPEED CLAMP
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                TurnVertically(-rotateRate);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                TurnVertically(rotateRate);
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                TurnHorizontally(-rotateRate);
            } else if (Input.GetKey(KeyCode.RightArrow))
            {
                TurnHorizontally(rotateRate);
            }
        }

        private void TurnHorizontally(float amount)
        {
            Vector3 angles = transform.localEulerAngles;

            // Note: Unity stores angles in 0–360 degrees, not -180 to 180
            float yAngle = angles.y;

            // Apply rotation amount
            yAngle += amount * Time.deltaTime;

            // Assign back
            angles.y = yAngle;
            transform.localEulerAngles = angles;
        }

        private void TurnVertically(float amount)
        {
            Vector3 angles = transform.localEulerAngles;

            // Note: Unity stores angles in 0–360 degrees, not -180 to 180
            float zAngle = angles.x;

            // Apply rotation amount
            zAngle += amount * Time.deltaTime;

            // Assign back
            angles.x = zAngle;
            transform.localEulerAngles = angles;
        }


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject);
        }
    }
}