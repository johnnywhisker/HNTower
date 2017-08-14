using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {

    // Calling outer classes
    private StackController[] stacks;

    // UFO vertical movement effect
    private float roof = 3f; // The highest position UFO can get to
    private float bottom = 3f; // The lowest position UFO can get to
    private Vector3 primaryPos;
    // UFO state
    private bool isUp = true;
    private Vector3 ufoPos;
    public float speed;
    //public float shakeSpeed;
    public bool isAngry = false;
    public int loopTimes = 5;

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
    }
	
	// Update is called once per frame
	void Update () {
        ufoMovVer();
        UFOGetAngry();
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



}
