using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Echo_Emission : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Transform echoRayOrigin;
    public float range;
    public float rate;
    private float echoTimer;
    private float echoDuration = 0.0004f;
    private float rayTimer;
    public Material echoMaterial;
    // public Transform echoCenter;

    // Start is called before the first frame update
    void Awake()
    {
        echoMaterial.SetFloat("_EchoDuration", 0);
        // echoRay = GetComponent<LineRenderer>();
        // rippleMaterial = GetComponent<Renderer>().material; // Assuming the script is attached to the object with the ripple material
    }

    // Update is called once per frame
    void Update()
    {
        if (echoTimer > 0.0) {echoTimer -= echoDuration;}
        rayTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && rayTimer > rate)
        {
            rayTimer = 0;
            // echoRay.SetPosition(0, echoRayOrigin.position);

            Vector3 origin = firstPersonCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(origin, firstPersonCamera.transform.forward, out hit, range))
            {
                // Set the ripple center in the material (raycast hit point)
                echoMaterial.SetVector("_Center", hit.point);
                echoTimer = 1f;

                // echoRay.SetPosition(1, hit.point);

                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            }
            else
            {
                // echoRay.SetPosition(1, origin + (firstPersonCamera.transform.forward * range));
                Debug.Log("Ray did not hit anything.");
            }
        }
        echoMaterial.SetFloat("_EchoDuration", echoTimer);
    }
}