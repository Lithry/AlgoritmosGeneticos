using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Actions {
	public enum Action
	{
		Rigth = 0,
		Left,
		Up,
		Total
	}
	public const float MaxTime = 3.0f;
	public const float MinTime = 0.1f;
    public const float durationOfGen = 30.0f;
}
