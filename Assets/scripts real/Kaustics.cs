﻿using UnityEngine;
using System.Collections;

public class Kaustics : MonoBehaviour
{
	private Projector projector;
	public MovieTexture mt;

	void Start()
	{
		projector = GetComponent<Projector>();
		projector.material.SetTexture("_ShadowTex", mt);
		mt.loop = true;
		mt.Play();
	}
}