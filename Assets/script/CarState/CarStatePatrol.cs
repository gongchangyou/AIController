using UnityEngine;
using System.Collections;

public class CarStatePatrol : StateBase<EnemyCar> {
	float elapseTime = 0.0f;

	public Vector3[] pointList;
	int currentPathIndex;
	int pathLength;
	Vector3 velocity;

	private float speed = 20.0f;
	private float mass = 5.0f;
	private float curSpeed;
	private Vector3 targetPoint;
	private float radius = 2.0f;
	public CarStatePatrol(EnemyCar car) : base( car ){}
	// Use this for initialization
	void Start () {
		currentPathIndex = 0;
		pathLength = pointList.Length;
		velocity = owner.transform.forward;
		curSpeed = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	public override IState Update () {
		targetPoint = pointList [currentPathIndex];

		if (Vector3.Distance (owner.transform.position, targetPoint) < radius) {
			if(currentPathIndex < pathLength - 1) currentPathIndex++;
			else currentPathIndex = 0;
		}

		if (currentPathIndex >= pathLength) {
			Debug.LogError("impossible currentPathIndex=" + currentPathIndex);
			return this;
		}

		velocity += Steer (targetPoint);
		owner.transform.position += velocity;
		owner.transform.rotation = Quaternion.LookRotation (velocity);

		return this;
	}

	private Vector3 Steer(Vector3 target){
		Vector3 desiredVelocity = target - owner.transform.position;
		float dist = desiredVelocity.magnitude;
		desiredVelocity.Normalize ();
		if (dist < 10.0f)
			desiredVelocity *= curSpeed * dist / 10.0f;
		else
			desiredVelocity *= curSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;
		Vector3 acceleration = steeringForce / mass;
		return acceleration;
	}
}
