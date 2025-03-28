using System.Collections;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Echo_Emission : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Transform echoRayOrigin;
    public float range;
    public float rate = 2f;
    private float echoTimer;
    private float echoDuration = 0.0004f;
    private float rayTimer;
    public Material echoMaterial;
    public Image echoCDBar;

    // public Transform echoCenter;

    void Start()
    {
        // cooldownPanel.sizeDelta = emptyCD;
    }
    // Start is called before the first frame update
    void Awake()
    {
        echoMaterial.SetFloat("_EchoDuration", 0);
        echoCDBar.fillAmount = 0f;
        // echoRay = GetComponent<LineRenderer>();
        // rippleMaterial = GetComponent<Renderer>().material; // Assuming the script is attached to the object with the ripple material
    }

    // Update is called once per frame
    void Update()
    {
        if (echoTimer > 0.0) {echoTimer -= echoDuration;}
        rayTimer += Time.deltaTime;
        echoCDBar.fillAmount += Time.deltaTime / 3;
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
                echoCDBar.fillAmount = 0;
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