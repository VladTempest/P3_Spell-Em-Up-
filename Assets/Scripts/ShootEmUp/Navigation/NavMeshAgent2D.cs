using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshAgent2D : MonoBehaviour
{
	public GameObject target;
   void Start()	{
   		var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
   		agent.updateRotation = false;
   		agent.updateUpAxis = false;

   }
}
