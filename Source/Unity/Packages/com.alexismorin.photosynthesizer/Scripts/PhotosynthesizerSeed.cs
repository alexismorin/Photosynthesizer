using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotosynthesizerSeed : MonoBehaviour {

    [SerializeField]
    GameObject[] plantGameObjects;
    [SerializeField]
    float seedRangeCustomizer = 1f;
    [SerializeField]
    float densityCustomizer = 1f;
    [SerializeField]
    Vector2 rangeScale = new Vector2 (0.7f, 1.3f);
    [SerializeField]
    LayerMask layerControl;

    int weeksGrown = 1;

    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawLine (transform.position, transform.position + (Vector3.up / 2f));
    }

    //   void OnValidate () {
    //      Grow (weeksGrown);
    //   }

    public void Grow (int weeksGrownExternal) {

        weeksGrown = weeksGrownExternal;

        // wipe current seed foliage
        int children = transform.childCount;
        for (int i = children - 1; i >= 0; i--) {
            GameObject.DestroyImmediate (transform.GetChild (i).gameObject);
        }

        // calculate range, density and bounding volume
        float range = seedRangeCustomizer * (float) weeksGrown;
        int density = (int) Mathf.Round (densityCustomizer * (weeksGrown * weeksGrown * weeksGrown * 50));

        for (int i = 0; i < density; i++) {
            Vector3 plantPosition = (Random.insideUnitSphere * range) + transform.position;
            //  Vector3 plantRotation = new Vector3 (0f, Random.Range (0, 360f), 0f);

            RaycastHit hit;

            if (Physics.Raycast (plantPosition, transform.TransformDirection (Vector3.down), out hit, 1f, layerControl, QueryTriggerInteraction.Ignore)) {

                if (Random.Range (0f, 1f) > (1 / range * Vector3.Distance (hit.point, transform.position))) {
                    Vector3 plantRotation = new Vector3 (Quaternion.LookRotation (hit.normal).eulerAngles.x + 90f, Random.Range (0, 360f), Quaternion.LookRotation (hit.normal).eulerAngles.z);
                    GameObject plant = Instantiate (plantGameObjects[Random.Range (0, plantGameObjects.Length)], hit.point, Quaternion.Euler (plantRotation));
                    plant.transform.parent = this.transform;
                    plant.transform.localScale *= Random.Range (rangeScale.x, rangeScale.y);
                }

            }

        }
    }
}