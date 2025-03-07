using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Echo_Emission : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Transform echoRayOrigin;
    public float range;
    public float rate;
    public float echoDuration;
    public GameObject revealLightPrefab; 

    LineRenderer echoRay;
    private float rayTimer;

    // Start is called before the first frame update
    void Awake()
    {
        echoRay = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rayTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && rayTimer > rate)
        {
            rayTimer = 0;
            echoRay.SetPosition(0, echoRayOrigin.position);

            Vector3 origin = firstPersonCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(origin, firstPersonCamera.transform.forward, out hit, range))
            {
                Vector3 hitDiff = hit.point - origin;
                hitDiff.Normalize();

                echoRay.SetPosition(1, hit.point);

                
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                
                IlluminateArea((hit.point - (hitDiff * 0.05f)));
            }
            else
            {
                echoRay.SetPosition(1, origin + (firstPersonCamera.transform.forward * range));
                Debug.Log("Ray did not hit anything.");
            }

            StartCoroutine(ShootRay());
        }
    }

    
    IEnumerator ShootRay()
    {
        echoRay.enabled = true;
        yield return new WaitForSeconds(echoDuration);
        echoRay.enabled = false;
    }

     
    void IlluminateArea(Vector3 hitPoint)
    {
         
        // Vector3 randomOffset = new Vector3(
        //     Random.Range(-1f, 1f),   
        //     Random.Range(-1f, 1f),   
        //     Random.Range(-1f, 1f)    
        // );

         
        // Vector3 lightPosition = hitPoint + randomOffset;
        Vector3 lightPosition = hitPoint;
        GameObject lightObj = Instantiate(revealLightPrefab, lightPosition, Quaternion.identity);
        Light revealLight = lightObj.GetComponent<Light>();
         
        if (revealLight != null) {
            Debug.Log("Light instantiated at: " + lightPosition);

            
            revealLight.color = Color.cyan;  
            revealLight.intensity = 1f;   
            revealLight.range = 75f;      
            revealLight.spotAngle = 360f;   

            StartCoroutine(EchoDim(revealLight, echoDuration * 3));
        }

        Destroy(lightObj, echoDuration * 3);

        IEnumerator EchoDim(Light light, float duration)
        {
            float startIntensity = light.intensity;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float lerpedIntensity = Mathf.Lerp(startIntensity, 0f, timeElapsed / duration);
                light.intensity = lerpedIntensity;
                yield return null;
            }

            light.intensity = 0f;
        }
    }

}
