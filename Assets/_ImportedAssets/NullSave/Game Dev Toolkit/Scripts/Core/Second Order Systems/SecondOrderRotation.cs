﻿//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using UnityEngine;

namespace NullSave.GDTK
{
	[AutoDoc("Second Order Rotation applies a second order system to provide more realistic rotation.")]
    [AutoDocLocation("second-order-systems")]
	public class SecondOrderRotation : MonoBehaviour
	{

		#region Fields

		[Tooltip("Transform to rotate")] public Transform target;
		[Tooltip("Frequency Hz of the system")] public float frequency = 0.03f;
		[Tooltip("System damping")] public float damping = 0.5f;
		[Tooltip("Initial response of the system")] public float speed = -2;

		private Vector3 xp; // previous input
		private Vector3 y, yd; // state variables
		private float k1, k2, k3; // dynamic constants

		#endregion

		#region Unity Methods

		private void Awake()
		{
			// compute constants
			k1 = damping / (Mathf.PI * frequency);
			k2 = 1 / ((2 * Mathf.PI * frequency) * (2 * Mathf.PI * frequency));
			k3 = speed * damping / (2 * Mathf.PI * frequency);

			// initialize variables
			xp = y = transform.rotation.eulerAngles;
			yd = Vector3.zero;
		}

		private void Update()
		{
			// Estimate velocity
			Vector3 xd = (target.rotation.eulerAngles - xp) / Time.deltaTime;
			xp = target.rotation.eulerAngles;

			y += Time.deltaTime * yd; // integrate position by velocity
            yd += Time.deltaTime * (target.rotation.eulerAngles + k3 * xd - y - k1 * yd) / k2; // integrate velocity by acceleration

            transform.rotation = Quaternion.Euler(y);
		}

		#endregion

	}
}