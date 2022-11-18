using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AIVision : MonoBehaviour
{
	public LayerMask mask;

	public string targetTag = "Untagged";

	private Camera frustum;

	private void Start()
    {
        frustum = GetComponent<Camera>();

        if (frustum)
        {
			frustum.clearFlags = CameraClearFlags.Nothing;
			// TODO: have bug
			frustum.cullingMask &= ~(1 << LayerMask.NameToLayer("Everything"));
			frustum.cullingMask |= 1 << LayerMask.NameToLayer("Nothing");
			frustum.allowHDR = false;
			frustum.allowMSAA = false;
		}
    }

    void Update()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

		foreach (Collider col in colliders)
		{
			if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
			{
                Ray ray = new()
                {
                    origin = transform.position,
                    direction = (col.transform.position - transform.position).normalized
                };

                ray.origin = ray.GetPoint(frustum.nearClipPlane);

				if (Physics.Raycast(ray, out RaycastHit hit, frustum.farClipPlane, mask))
                {
					if (hit.collider.gameObject.CompareTag(targetTag))
					{
						// Call Function OnSee in all scripts in this gameobject when see sometarget.
						SendMessage("OnSee", hit.collider.gameObject);
					}
				}
			}
		}
	}
}
