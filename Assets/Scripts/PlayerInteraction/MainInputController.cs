using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInputController : MonoBehaviour
{
   	public PlayerInputController playerInputController;
    public Vector2 courserRaw;
    public Vector2 courserFixed;
    public WorldObject objectundercourser;
    public Camera camera; 
    public WorldData worldData;
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

    void Start()
    {
        worldData = FindObjectOfType<WorldData>();
        camera = GetComponentInChildren<Camera>();
    }

    public Vector2 getMousePositionScreen()
    {
        Vector2 mousePostionScreen = playerInputController.Mouse.Position.ReadValue<Vector2>();
        return mousePostionScreen;
    }


    public bool isRightButtonDown()
    {
        return (!(playerInputController.Mouse.RightButton.ReadValue<float>() == 0));
    }

    public bool isLeftButtonDown()
    {
        return (!(playerInputController.Mouse.LeftButton.ReadValue<float>() == 0));
    }

    public void Update()
    {
        courserRaw = camera.ScreenToWorldPoint(getMousePositionScreen());
        float distance = 4; 
        courserFixed = courserRaw;
        objectundercourser = null;
        foreach (var item in worldData.wayPoints)
        {
            if ((Util.toV2(item.location) - courserRaw).magnitude<distance){
                courserFixed = Util.toV2(item.location);
                objectundercourser = item;
            }
        }
    }
}