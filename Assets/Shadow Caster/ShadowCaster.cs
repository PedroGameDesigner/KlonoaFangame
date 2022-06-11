using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCaster : MonoBehaviour {

	public GameObject shadow;
	public LayerMask mask;

    [Range(0.0f, 1.0f)]
    public float fadeoutDistance;
    public float floorMargin;
    public bool alignWithNormal;

    float marginFromCenter = 0;
	Renderer shadowRenderer;

	// Use this for initialization
	void Start () {
		shadowRenderer = shadow.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 100, mask.value)) {

			//Debug.DrawLine (transform.position, hit.point, Color.cyan);
			shadow.transform.position = hit.point + (Vector3.up * floorMargin);

			float dist = Vector3.Distance (transform.position, hit.point);
			//round 2 decimal places
			dist = Mathf.Round(dist * 100f) / 100f;

			if (hit.distance <= 1 + marginFromCenter) {
                float alphaValue = Mathf.Abs(
                    Mathf.Clamp(
                        (dist / fadeoutDistance) - marginFromCenter, 
                        0, 
                        1) - 1
                    );
                shadowRenderer.material.color = new Color (1, 1, 1, alphaValue);
			}

            if (alignWithNormal) {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            } else {
                transform.rotation = Quaternion.identity;
                //transform.Rotate(90, 0, 0);
            }

        }

	}
}
