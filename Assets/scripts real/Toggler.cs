using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
	public GameObject Enable; //What to enable
	public GameObject Disable_1; //What to DISABLE
	public GameObject Disable_2;
	//add more as you go on 


	public void VarEnable() //what to enable
	{
		Enable.SetActive(true);
	}

	public void VarDisable_1() //1 to disable
	{
		Disable_1.SetActive(false);
	}

	public void VarDisable_2() //2 to disable
	{
		Disable_2.SetActive(false);
	}
}



