using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {

    // Calling outer classes
    private PlanetController planets;
    private EverythingController gameController;

    // UFO vertical movement effect
    private float roof = 3f; // The highest position UFO can get to
    private float bottom = 3f; // The lowest position UFO can get to
    private Vector3 primaryPos;
    // UFO state
    private bool isUp = true;
    public float lastStackPlanetCoordinate;
    private Vector3 ufoPos;
    private bool isMovingHorizontal = false;
    public float speed;
    //public float shakeSpeed;
    public bool isAngry = false;
    public int loopTimes = 5;
    private int desireStack = 0;
    // Counting Time for shaking
    private float second = 0;

    // Use this for initialization
    void Start()
    {
        isAngry = false;
        roof = transform.position.y + 0.5f;
        bottom = transform.position.y - 0.35f;
        second = Time.realtimeSinceStartup;
        primaryPos = this.transform.localPosition;
        gameController = GameObject.FindObjectOfType<EverythingController>();
    }
	
	// Update is called once per frame
	void Update () {
        ufoMovVer();
        UFOGetAngry();
        if (isMovingHorizontal && Mathf.Abs(transform.localPosition.x - gameController.stacks[desireStack].transform.localPosition.x) > 0.5)
        {
            if (transform.localPosition.x < gameController.stacks[desireStack].transform.localPosition.x)
            {
                float xCoordinate = gameController.stacks[desireStack].transform.localPosition.x;
                Debug.Log(transform.localPosition.y);
                Vector3 pos = new Vector3(xCoordinate, transform.localPosition.y, transform.localPosition.z);
                transform.localPosition = pos;
                transform.localPosition += Vector3.right;
            }
            else
            {
                transform.localPosition += Vector3.left;
            }
        }
        else
        {
            isMovingHorizontal = false;
        }
    }

    // UFO movement
    public void ufoMovVer()
    {
        ufoPos = transform.position;
        if (isUp)
        {
            ufoPos.y += speed;
            if (Mathf.Abs(transform.position.y - roof) < 0.3) // 0.3: nearly accurate ratio for not shaking as fuck
            {
                isUp = false;
            }
        }
        else
        {
            ufoPos.y = ufoPos.y - speed;
            if (Mathf.Abs(transform.position.y - bottom) < 0.3)
                isUp = true;
        }
        transform.position = ufoPos;
    }
    public void UFOGetAngry()
    {
        if (isAngry && second <= 3)
        {
            Vector3 pos = primaryPos;
            Vector3 left = new Vector3(0.2f, 0, 0);
            Vector3 right = new Vector3(-0.2f, 0, 0);
            if (Mathf.Round(second * 800) % 2 == 0)
            {
                Debug.Log("LEFT");
                pos += left;
            }
            else
            {
                Debug.Log("RIGHT");
                pos += right;
            }

            transform.localPosition = pos;
            second = Time.realtimeSinceStartup;
        }
        if (second > 3)
        {
            isAngry = false;
            second = 0;
            transform.localPosition = primaryPos;
        }
    }

    public void MoveTo(int stack)
    {
        isMovingHorizontal = true;
        desireStack = stack;
        if (desireStack == 1 && gameController.stacks[desireStack].planets.Count == 0)
        {
            lastStackPlanetCoordinate = gameController.stacks[desireStack - 1].GetTopPlanet().transform.localPosition.y + gameController.stacks[desireStack - 1].GetTopPlanet().diameter;
        }
        else if (desireStack == 2 && gameController.stacks[desireStack].planets.Count == 0)
        {
            lastStackPlanetCoordinate = gameController.stacks[0].GetTopPlanet().transform.localPosition.y + gameController.stacks[0].GetTopPlanet().diameter;
        }
        else
        {
            lastStackPlanetCoordinate = gameController.stacks[desireStack].GetTopPlanet().transform.localPosition.y + gameController.stacks[desireStack].GetTopPlanet().diameter;
        }
    }

}
