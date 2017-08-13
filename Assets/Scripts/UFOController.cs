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
    private float pos1, pos2, pos3;
    private float desY; // Destination UFO desires to go

    // Use this for initialization
    void Start()
    {
        roof = transform.position.y + 0.5f;
        bottom = transform.position.y - 0.35f;

        // Postions of UFO correspond with stacks 
        pos1 = -6.6f;
        pos2 = -2.21f;
        pos3 = 2.08f;
        desY = pos1; // UFO sets stack 1 as first play to come when the game starts
    }
	
	// Update is called once per frame
	void Update () {
        ufoMovState();
	}

    // UFO movement
    public void ufoMovState()
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

    public void OnMouseOver()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            
        }
    }
}
