using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Chromosome{
	public float fitness;
	public List<Gen> weights;
}

static public class Actions {
	public enum Action
	{
		Rigth = 0,
		Left,
		Up,
		Total
	}
	public const float MaxTime = 1.5f;
	public const float MinTime = 0.5f;
    
}