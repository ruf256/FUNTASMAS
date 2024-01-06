using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoEffect : MonoBehaviour
{
    private GameObject effectSphere;
    [SerializeField] private GameObject effectSpherePrefab;
    MeshRenderer effectSphereMesh;

    public void InstanciarEfecto(Vector2 pos)
    {
        if (!effectSphere)
        {
            effectSphere = Instantiate(effectSpherePrefab, pos, Quaternion.identity);
            effectSphere.transform.localScale = new Vector3 (0.3f, 0.3f, 1);

            effectSphereMesh = effectSphere.GetComponent<MeshRenderer>();

            if (GameData.gameData.LoadModoVisual() == 0)
            {
                effectSphereMesh.material.color = new Color(0,0,0,0.8f);
            }
            else if (GameData.gameData.LoadModoVisual() == 1)
            {
                effectSphereMesh.material.color = new Color(0, 0, 0, 0.65f);
                
            }

            effectSphere.SetActive(true);
            StartCoroutine(AnimacionEfecto());
        }
        else
        {
            effectSphere.transform.position = pos;
            effectSphere.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            effectSphere.SetActive(true);
            StartCoroutine(AnimacionEfecto());
        }
    }

    IEnumerator AnimacionEfecto()
    {
        
        while (effectSphere.transform.localScale.magnitude < 150)
        {

            effectSphere.transform.localScale +=
                new Vector3((effectSphere.transform.localScale.x + 12.5f) * Time.deltaTime,(effectSphere.transform.localScale.y + 12.5f) * Time.deltaTime, (effectSphere.transform.localScale.z + 45) * Time.deltaTime);

            effectSphere.transform.rotation = new Quaternion(effectSphere.transform.rotation.x, effectSphere.transform.rotation.y, (effectSphere.transform.rotation.z + 15) * Time.deltaTime, effectSphere.transform.rotation.w);
            if (effectSphere.transform.localScale.magnitude >= 150)
            {
                effectSphere.SetActive(false);
            }
            yield return null;
        }
    }
}
