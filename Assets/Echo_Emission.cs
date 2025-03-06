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
    public GameObject lightPrefab;

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
        if(Input.GetButtonDown("Fire1") && rayTimer > rate){
            rayTimer = 0;
            echoRay.SetPosition(0, echoRayOrigin.position);
            Vector3 origin = firstPersonCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(origin, firstPersonCamera.transform.forward, out hit, range))
            {
                echoRay.SetPosition(1, hit.point);
                if (hit.collider.CompareTag("Wall"))
                {
                    Raycast_Hit wallChange = hit.collider.GetComponent<Raycast_Hit>();
                    if (wallChange != null)
                    {
                        wallChange.ChangeColor(Color.red);
                    }
                }
                StartCoroutine(EmitLight(hit.point));
            }
            else
            {
                echoRay.SetPosition(1, origin + (firstPersonCamera.transform.forward * range));
            }
            StartCoroutine(ShootRay());
        }
    }

    IEnumerator EmitLight(Vector3 position) { 
        GameObject tempLight = Instantiate(lightPrefab, position, Quaternion.identity);
        Light lightComponent = tempLight.GetComponent<Light>();

        if (lightComponent != null)
        {
            lightComponent.intensity = 3f;
            lightComponent.range = 5f;
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(tempLight);
    }
    IEnumerator ShootRay()
    {
        echoRay.enabled = true;
        yield return new WaitForSeconds(echoDuration);
        echoRay.enabled = false;
    }
}
