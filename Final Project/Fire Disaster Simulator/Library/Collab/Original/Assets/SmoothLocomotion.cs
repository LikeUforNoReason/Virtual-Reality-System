using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    private float _horizontalInput;
    private float _verticalInput;
    private bool _jumpPressed;
    private bool _isgrounded;
    public Transform _groundChecker;
    public float GroundDistance;
    public AudioSource audioS;
   
    private CharacterController myController;
    private Vector3 _velocity;
    private float Gravity;
    public LayerMask Ground;
    private Vector3 RawInput;
    private Vector3 AdjustedInput;
    public GameObject hmd;
    public float speed;
    void Start()
    {
        myController = GetComponent<CharacterController>();
        Gravity = Physics.gravity.y;
    }

    
    private void LateUpdate()
    {

        _isgrounded = Physics.Raycast(_groundChecker.position, Vector3.down, GroundDistance,Ground);

        if (_isgrounded)
        {
            Debug.Log("OnGround");
            _velocity.y = 0f;
        }

        RawInput = new Vector3(-_horizontalInput, _velocity.y, -_verticalInput);

        //offset angle calculation.
        float offsetangle = SignedAngle(hmd.transform.forward, Vector3.forward, Vector3.up);
        // calculation the transformed input.
        AdjustedInput = Quaternion.Euler(0f, -offsetangle, 0f) * RawInput;

        //applying the movement
        myController.Move(AdjustedInput * Time.deltaTime * speed);

        myController.center = hmd.transform.position;

        PlayFootsteps();
    }
    #region Record Controller Input
    public void UpdateHorizontalInput(System.Single horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
    public void UpdateVerticalInput(System.Single verticalInput)
    {
        _verticalInput = verticalInput;
    }
    public void UpdateJumpInput(bool jump)
    {
        _jumpPressed = jump;
    }
    #endregion

    public float SignedAngle(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    private void PlayFootsteps()
    {

        if (_horizontalInput > 0.1f || _verticalInput > 0.1f)
        {
            audioS.enabled = true;
            audioS.loop = true;
        }

        if (_horizontalInput < 0.1f && _verticalInput < 0.1f)
        {
            audioS.enabled = false;
            audioS.loop = false;
        }
    }
}
