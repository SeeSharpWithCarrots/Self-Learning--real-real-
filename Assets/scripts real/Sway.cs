using UnityEngine;

public class Sway : MonoBehaviour {

	public float amount = 0.055f;
	public float maxAmount = 0.09f;
	public float smooth = 3;
	Vector3 def;

	Vector2 defAth;

	Vector3 euler;

	GameObject ath;

	void Start () 
	{
		def = transform.localPosition;
		euler = transform.localEulerAngles;
	}

	float _smooth;
	
	void Update () 
	{
		_smooth = smooth;

		float factorX = - Input.GetAxis("Mouse X") * amount;
		float factorY = - Input.GetAxis("Mouse Y") * amount;

// FOR X
		if(factorX > maxAmount) // can't go over a cerain threshold (X)
			factorX = maxAmount;

		if(factorX < -maxAmount)
			factorX = -maxAmount;
// FOR Y
		if(factorY > maxAmount) // can't go over a cerain threshold (X)
			factorY = maxAmount;

		if(factorY < -maxAmount)
			factorY = -maxAmount;

		Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z );
		transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * _smooth);
		

	}
}
