﻿using UnityEngine;
using System.Collections;

public class TaskBase
{
	public virtual IEnumerator runTask()
	{
		yield return new WaitForSeconds (1f);
	}
}