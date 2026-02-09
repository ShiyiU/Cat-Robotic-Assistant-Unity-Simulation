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
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

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
                Vector3 dampingForce = -rb.velocity * damping;
                rb.AddForce(dampingForce, ForceMode.Acceleration);
            }

            // SPEED CLAMP
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
}