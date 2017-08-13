using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {

    // UFO vertical movement effect
    private float roof = 3f; // The highest position UFO can get to
    private float bottom = 3f; // The lowest position UFO can get to

    // UFO state
    private bool isUp = true;
    private bool isMovHor = true;
    private Vector3 ufoPos;
    public float speed;

    // UFO Position
    //private Vector3 pos1, pos2, pos3;
    //private Vector3 dest; // Destination UFO desires to go

    // Use this for initialization
    void Start()
    {
        roof = transform.position.y + 0.5f;
        bottom = transform.position.y - 0.35f;

        // Postions of UFO correspond with stacks 
        //pos1 = new Vector3(-6.61f, 3.439999f, -2.46f);
        //pos2 = new Vector3(-2.27f, 3.63f, -2.46f);
        //pos3 = new Vector3(2.07f, 3.7f, -2.46f);
        //dest = pos1; // UFO sets stack 1 as first play to come when the game starts
    }
	
	// Update is called once per frame
	void Update () {
        ufoMovVer();
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


}
