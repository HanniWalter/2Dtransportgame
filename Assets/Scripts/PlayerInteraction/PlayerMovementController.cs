using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovementController : MonoBehaviour
{
	private PlayerInputController playerInputController;
    public MainInputController mainInputController;


    [SerializeField] private float movespeedKeyboard = 10;

    [SerializeField] private float zoomspeedKeyboard = 10;

	private void Awake()
	{
	} 

    // Start is called before the first frame update
    void Start()
    {
        mainInputController = GetComponent<MainInputController>();
		playerInputController = mainInputController.playerInputController;
        playerInputController.Mouse.RightButton.performed += ctx => RightMouseClick(ctx);
        playerInputController.Mouse.LeftButton.performed += _ => LeftMouseClick();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerInputController.Mouse.Position.ReadValue<Vector2>());
        movement();
    }

    void movement()
    {
        Vector2 movement = playerInputController.Keyboard.wasd.ReadValue<Vector2>()*movespeedKeyboard*Time.deltaTime;
        float zoom = -playerInputController.Keyboard.numplusminus.ReadValue<float>()*zoomspeedKeyboard*Time.deltaTime;
        transform.position += new Vector3(movement.x,movement.y,0);
        gameObject.GetComponentInChildren<Camera>().orthographicSize = Mathf.Clamp(gameObject.GetComponentInChildren<Camera>().orthographicSize + zoom,10,1000);
    }

    private void RightMouseClick(InputAction.CallbackContext ctx)
    {

    }

    private void LeftMouseClick()
    {
    }
}
