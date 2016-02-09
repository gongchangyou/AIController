using UnityEngine;
using System.Collections;

public class EnemyCar : MonoBehaviour {
	private IStateContext stateContext;
	public IStateContext StateContext{
		get{return stateContext;}
		set{stateContext = value;}
	}
	// Use this for initialization
	void Start () {
		StateContext = new StateContext<EnemyCar> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StateContext == null)
			return;
		if (StateContext.CurrentState == null)
			return;
		StateContext.Update ();
		if (StateContext.CurrentState == null)
			return;
	}

	public virtual CarStatePatrol Patrol(Vector3[] pointList){
		CarStatePatrol state = new CarStatePatrol (this);
		state.pointList = pointList;
		StateContext.SetState (state);
		return state;
	}

}
