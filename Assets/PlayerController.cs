using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float sensitivity = 3.5f;
      [SerializeField] bool lockCursor = true;
    float cameraPitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(lockCursor){
            // Cursor.lockState = CursorLockMode.locked;
            // Cursor.visible = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
     UpdateMouseLook();   
    }

    void UpdateMouseLook(){

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(Vector3.up * mouseDelta.x * sensitivity);

        cameraPitch -= mouseDelta.y * sensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

    }
}
