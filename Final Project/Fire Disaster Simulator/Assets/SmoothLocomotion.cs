using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLocomotion : MonoBehaviour
{
    [Header("Object References")]

    public Transform HMD;

    [Header("Movement Parameters")]
    public float Speed;
    public float JumpHeight;
    public float Gravity;

    [Header("Gounded Checking")]
    public Transform GroundChecker;
    public float GroundDistance;
    public LayerMask GroundMask;

    private CharacterController _controller;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _jumpPressed;
    private bool _isGrounded;
    private Vector3 _velocity;

    public float footStepDelta;
    private float footSteptimer;

    Vector3 worldMove;
    public AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundChecker.localPosition = new Vector3(HMD.localPosition.x, .1f, HMD.localPosition.z);

        // Check to see if the character is grounded. If so, set their y velocity to zero
        _isGrounded = Physics.Raycast(GroundChecker.position, Vector3.down, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore); //(GroundChecker.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        // Transform the local input from the controller into world input relative to the characters facing direction
        Vector3 localMove = new Vector3(_horizontalInput, 0, -_verticalInput);
        Vector3 HMDForward = new Vector3(HMD.forward.x, 0, HMD.forward.z);
        float localAngle = SmoothLocomotion.AngleSigned(Vector3.forward, HMDForward, Vector3.up);//Vector3.Angle(Vector3.forward, HMDForward);
        worldMove = Quaternion.Euler(0, localAngle, 0) * localMove;

        //Debug.Log(localAngle);

        // Move the controller
        _controller.Move(worldMove * Time.deltaTime * Speed);

        //if(move != Vector3.zero)
        //    transform.forward = move;
        if (localMove != Vector3.zero && footSteptimer >= footStepDelta)
        {            
            audioS.Play();
            footSteptimer = 0;
        }
        footSteptimer = footSteptimer + Time.deltaTime;

        // Add a jump to the character
        if (_jumpPressed && _isGrounded)
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);

        _velocity.y += Gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

        // Translate the play area based on the HMD position to keep the HMD at the location 
        _controller.center = new Vector3(HMD.localPosition.x, _controller.center.y, HMD.localPosition.z);
    }

    public void UpdateHorizontalInput(System.Single horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
    public void UpdateVerticalInput(System.Single verticalInput)
    {
        _verticalInput = -verticalInput;
    }

    public void UpdateJumpInput(bool jump)
    {
        _jumpPressed = jump;
    }

    public void IncreaseValueHeight(bool vault)
    {
        _controller.stepOffset = 1f;
    }
    public void ResetValueHeight(bool vault)
    {
        _controller.stepOffset = 0.3f;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawRay(HMD.transform.position, HMD.transform.forward);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawRay(HMD.transform.position, worldMove);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawRay(HMD.transform.position, Vector3.forward);

    //    Gizmos.DrawLine(GroundChecker.position, GroundChecker.position + Vector3.down * GroundDistance);
    //}

    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

}
