using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public static Mover current {get; private set;}
	public static Mover last {get; private set;}
    public MoveDirection MoveDirection { get; set; }

    public float speed;

	void OnEnable()
	{
		if (last == null)
			last = GameObject.Find("Start").GetComponent<Mover>();
		current = this;

		GetComponent<Renderer>().material.color = GetRandomColor();

		transform.localScale = new Vector3 (last.transform.localScale.x, transform.localScale.y, last.transform.localScale.z);
	}

    private Color GetRandomColor()
    {
        return new Color (UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0,1f));
    }

    internal void Stop()
    {
        speed = 0;
        float overflow = GetOverflow();

		float max = MoveDirection == MoveDirection.Z ? last.transform.localScale.z : last.transform.localScale.x;

        if (Mathf.Abs(overflow) >= max)
        {
            last = null;
            current = null;

        }

        float direction = overflow > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
            SplitZ(overflow, direction);
        else
            SplitX(overflow, direction);


        last = this;
    }

    private float GetOverflow()
    {
		if (MoveDirection == MoveDirection.Z)
        	return transform.position.z - last.transform.position.z;
		else{
			return transform.position.x - last.transform.position.x;
		}
    }

    private void SplitX(float overflow, float direction)
    {
        float newXS = last.transform.localScale.x - Mathf.Abs(overflow);
		float fallingXS = transform.localScale.x - newXS;

		float newZP = last.transform.position.x + (overflow / 2);
		transform.localScale = new Vector3(newXS, transform.localScale.y, transform.localScale.z);
		transform.position = new Vector3(newZP, transform.position.y, transform.position.z);

		float edge = transform.position.x + (newXS / 2f * direction);
		float fallingXP = edge + fallingXS / 2f * direction;

		SpawnDrop(fallingXP, fallingXS);
    }
    private void SplitZ(float overflow, float direction)
    {
        float newZS = last.transform.localScale.z - Mathf.Abs(overflow);
		float fallingS = transform.localScale.z - newZS;

		float newZP = last.transform.position.z + (overflow / 2);
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZS);
		transform.position = new Vector3(transform.position.x, transform.position.y, newZP);

		float edge = transform.position.z + (newZS / 2f * direction);
		float fallingZP = edge + fallingS / 2f * direction;

		SpawnDrop(fallingZP, fallingS);
    }

    private void SpawnDrop(float fallingZP, float fallingS)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

		if(MoveDirection == MoveDirection.Z){
			cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingS);
			cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingZP);
		}
		else{
			cube.transform.localScale = new Vector3(fallingS, transform.localScale.y, transform.localScale.z);
			cube.transform.position = new Vector3(fallingZP, transform.position.y, transform.position.z);
		}

		cube.AddComponent<Rigidbody>();
		cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
		Destroy(cube, 2f);
    }

    void Update () {
		if (MoveDirection == MoveDirection.Z)
			transform.position += transform.forward * Time.deltaTime * speed;
		else{
			transform.position += transform.right * Time.deltaTime * speed;
		}
	}
}
