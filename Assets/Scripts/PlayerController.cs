using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
	private PlayerInputController playerInputController;
    [SerializeField] private float movespeedKeyboard = 10;
    //[SerializeField] private float movespeedMouse = 20;
    //[SerializeField] private float movespeedBorder = 40;
    //[SerializeField] private float borderMoveActivation = 0.1f;

    [SerializeField] private float zoomspeedKeyboard = 10;
    //[SerializeField] private float zoomspeedMouse = 1;

	private void Awake()
	{
		playerInputController = new PlayerInputController();
	} 

    private void OnEnable()
    {
        playerInputController.Enable();
    }

    private void OnDisable()
    {
        playerInputController.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        Vector2 movement = playerInputController.Keyboard.wasd.ReadValue<Vector2>()*movespeedKeyboard*Time.deltaTime;
        float zoom = -playerInputController.Keyboard.numplusminus.ReadValue<float>()*zoomspeedKeyboard*Time.deltaTime;
        transform.position += new Vector3(movement.x,movement.y,0);
        gameObject.GetComponentInChildren<Camera>().orthographicSize = Mathf.Clamp(gameObject.GetComponentInChildren<Camera>().orthographicSize + zoom,10,1000);

    }
}
