using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStupidBrain : CharacterInput
{
    private void Start()
    {
       // Look = new Vector2(0.0f, 0.0f);
       // Move = new Vector2(0.0f, 1.0f);

    }


    public override float GetInputMagnitude()
    {
            return 1.0f;
    }

    public void FixedUpdate()
    {
        RandomPatrol();
        JumpRandom();
    }

    private void JumpRandom()
    {
        float randCondition = UnityEngine.Random.Range(-1.0f, 1.0f);

        if (randCondition > 0.5 && randCondition < 0.51)
            JumpFlag = true;
    }

    float timePatrolAtDirection = 2.0f;
    float timePatrolCounter = 0f;

    bool isForwardDirection = true;

    private void RandomPatrol()
    {
        if (timePatrolCounter <= 0)
        {
            Move = isForwardDirection ? new Vector2(0.0f, 0.0f) : new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
            timePatrolCounter = timePatrolAtDirection;
            isForwardDirection = !isForwardDirection;
        }
        else
        {
            timePatrolCounter -= Time.deltaTime;
        }
    }


    private void Patrol()
    {
        if (timePatrolCounter <= 0)
        {
            Move = isForwardDirection ? new Vector2(0.0f, 1.0f) : new Vector2(0.0f, -1.0f);
            timePatrolCounter = timePatrolAtDirection;
            isForwardDirection = !isForwardDirection;
        }
        else
	    {
            timePatrolCounter -= Time.deltaTime;
        }
    }

}
