using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatingPlatform : MonoBehaviour
{
	[SerializeField] private float _rotatingSpeed = 30f;
	[SerializeField] private Vector3 _rotatingAxis = Vector3.up;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Vector3 currentEulerAngles = transform.rotation.eulerAngles;
		Vector3 targetEulerAngles = currentEulerAngles + _rotatingAxis * (_rotatingSpeed * Time.deltaTime);
		Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
		
		_rigidbody.MoveRotation(targetRotation);
	}
}