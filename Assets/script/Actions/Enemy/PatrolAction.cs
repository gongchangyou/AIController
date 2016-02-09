using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace AISystem.Actions{
	[Category("GameObject")]
	[System.Serializable]
	public class PatrolAction : BaseAction {
		[SerializeField]
		public Vector3[] pointList = {Vector3.zero, Vector3.one};
		public override void OnEnter ()
		{
			EnemyCar enemyCar = owner.GetComponent<EnemyCar> ();
			enemyCar.Patrol (pointList);
			Finish ();
		}
		
	}
}