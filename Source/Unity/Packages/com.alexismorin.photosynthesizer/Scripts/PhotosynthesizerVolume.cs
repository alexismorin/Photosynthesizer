using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotosynthesizerVolume : MonoBehaviour {

    [SerializeField]
    int weeksGrown = 3;

    // Seeds in this volume
    List<GameObject> seeds = new List<GameObject> ();

    void Grow () {
        foreach (GameObject seed in seeds) {
            seed.GetComponent<PhotosynthesizerSeed> ().Grow (weeksGrown);
        }
    }

    void FindSeeds () {

        seeds = new List<GameObject> ();

        BoxCollider box = GetComponent<BoxCollider> ();
        GameObject[] allSeeds = GameObject.FindGameObjectsWithTag ("Seed");
        for (int i = 0; i < allSeeds.Length; i++) {
            if (box.bounds.Contains (allSeeds[i].transform.position)) {
                seeds.Add (allSeeds[i]);
            }
        }
    }

    public void Regrow () {
        FindSeeds ();
        Grow ();
    }

    //   void OnValidate () {
    //       FindSeeds ();
    //       Grow ();
    //   }
}