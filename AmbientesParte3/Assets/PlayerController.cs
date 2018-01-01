using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jSpeed;
    public float gravity;
    public Vector3 moveDirection;

    private Quaternion q_player;
    private Quaternion q_camera;
    private Quaternion q_weapon;

    public float mouseSpeed;

    Camera cam;
    public GameObject weapon;
    CharacterController charCtrl;
    CapsuleCollider cc;
    //ParticleSystem syst;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        cc = GetComponent<CapsuleCollider>();
        //syst = GetComponentInChildren<ParticleSystem>();
        //syst.Stop();
        //Lock Mouse
        Cursor.lockState = CursorLockMode.Locked;
        q_player = charCtrl.transform.localRotation;
        q_camera = cam.transform.localRotation;
        q_weapon = weapon.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;


        //Jumping
        if (charCtrl.isGrounded)
        {
            gravity = 50;
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(new Vector3(0, -jSpeed, 0), ForceMode.Impulse);
            }

        }

        //if (rb.position + (moveDirection * Time.deltaTime) + new Vector3(0, 1f, 0))
        moveDirection.y -= gravity * Time.deltaTime;

        if (charCtrl.isGrounded == false)
        {
            gravity += 10;
        }

        charCtrl.Move(moveDirection * Time.deltaTime);

        ////Shooting
        //if (Input.GetButton("Fire1"))
        //{
        //    syst.Stop();
        //    syst.Play();
        //}

        //Move Camera/View
        float xRot = Input.GetAxis("Mouse Y") * mouseSpeed;
        float yRot = Input.GetAxis("Mouse X") * mouseSpeed;

        q_player *= Quaternion.Euler(0.0f, yRot, 0.0f);
        q_camera *= Quaternion.Euler(-xRot, 0.0f, 0.0f);
        //q_weapon *= Quaternion.Euler(-xRot, yRot, 0.0f);

        transform.localRotation = q_player;
        cam.transform.localRotation = q_camera;
        //weapon.transform.localRotation = q_weapon;
    }
}