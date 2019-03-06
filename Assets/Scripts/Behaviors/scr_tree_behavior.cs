﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tree_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public  Vector3                 weak_point_direction                ;           // actual weak point direction, used to compare with director vector

[Space(10)][Header("Gameplay")]
    public  float                   weak_point_direction_max           = 0.50f;     // maxmimal angular ratio that the cut can reach (negative and positive)
  
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    void Start()
    {
        GenerateAppearance();
        InitializeWeakPoint();
        InitializeSelfDestruction(15.00f);
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Generates the appearance of the tree.
    /// </summary>
    public void GenerateAppearance()
    {

        return;
    }

    /// <summary>
    /// Initializes the tree weakpoint position according to weak_point_spawnRange.
    /// </summary>
    public void InitializeWeakPoint()
    {
        weak_point_direction = GenerateCutDirection();
        // Debug.Log(weak_point_direction);
        // Debug.DrawLine(gameObject.transform.position, gameObject.transform.position+weak_point_direction, Color.yellow, 10.00f);
        return;
    }

    /// <summary>
	/// Initialises the object self destruction after a given amount of time.
	/// </summary>
	public void InitializeSelfDestruction(float delay)
	{
        Destroy(gameObject, delay);
        return;
    }

    /// <summary>
	/// Returns a normalized vector that indicates a direction.
	/// </summary>
	public Vector3 GenerateCutDirection()
	{
		Vector3 direction_vector = new Vector3(0,0,0);

		// normalize combined axis inputs' value
		float normalize_factor;  // factor used to normalise the vector
		direction_vector.x = 0.50f;
		direction_vector.y = Random.Range(-weak_point_direction_max, weak_point_direction_max);
		normalize_factor = Mathf.Abs(direction_vector.x) + Mathf.Abs(direction_vector.y);
				
		// return normalized direction vector
		return (direction_vector / normalize_factor);
	}

    /// <summary>
	/// Called by the cutting manager when the tree is cutted by the player.
	/// </summary>
	public void CutTree( cutStateEnum state )
	{
        switch (state)
        {
            case cutStateEnum.Success:
                Debug.Log("SUCCESS");
                CuttingManager.cuttingManagerInstance.UnregisterTree();
                ScoreManager.instance.AddScore(1 + ScoreManager.instance.actual_combo);
                Destroy(this.gameObject);
            break;

            case cutStateEnum.Perfect:
                Debug.Log("PERFECT");
                CuttingManager.cuttingManagerInstance.UnregisterTree();
                ScoreManager.instance.AddCombo(1);
                ScoreManager.instance.AddScore(1 + ScoreManager.instance.actual_combo);
                Destroy(this.gameObject);
                break;
        }

        GameManager.instance.RemainingTrees -= 1;

        return;
    }


// = = =

}
