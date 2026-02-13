using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 4.0f;
    [SerializeField] private float turnSpeed = 150f;

    private Rigidbody rb;
    private Animator anim;
    private bool dead = false;
    private CatScenario catScenario;
    private Transform[] waypointLocations;  

    public Rigidbody RB { get { return rb; } }
    public float WalkSpeed { get { return walkSpeed; } }
    public float TurnSpeed { get { return turnSpeed; } }
    public Animator Anim { get { return anim; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // Physics constraints for the Bip001 skeleton
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Debug.Log(FindFirstObjectByType<MenuManager>());
        
        catScenario = FindFirstObjectByType<MenuManager>().GetScenario();
        catScenario.SetupScenario(this);
    }

    void Update()
    {
        if (dead) return; // Stop all input if the cat is dead

        /*
        // Eating Input - Triggers the custom cat_eat animation you created
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("isEating"); // Matches the trigger in your Animator
        }

        // Combat Inputs - Triggers Attack 3 and 4
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack3");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack4");
        }

        // Death Input - Using 'K' as you specified
        if (Input.GetKeyDown(KeyCode.K))
        {
            dead = true;
            anim.SetBool("IsDead", true); // Matches the Bool in your Animator
            rb.linearVelocity = Vector3.zero; // Stop physical movement
        }*/

        catScenario.UpdateScenario(); 
    }

    public void SetSpawnWaypoints(Transform[] waypoints)
    {
        waypointLocations = waypoints; 
    }

    public Transform[] GetSpawnWaypoints()
    {
        return waypointLocations; 
    }

    void FixedUpdate()
    {
        //if (!dead) HandleMovement();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        bool runKeyHeld = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = Mathf.Abs(moveInput) > 0.05f || Mathf.Abs(turnInput) > 0.05f;

        float currentSpeed = (isMoving && runKeyHeld) ? runSpeed : walkSpeed;

        // 1. ROTATION
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // 2. MOVEMENT
        Vector3 movement = transform.forward * moveInput * currentSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // 3. ANIMATION SYNC
        anim.SetBool("isWalking", isMoving);
        anim.SetBool("isRunning", isMoving && runKeyHeld);
    }
}