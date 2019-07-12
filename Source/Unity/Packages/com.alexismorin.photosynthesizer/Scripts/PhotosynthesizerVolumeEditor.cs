using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (PhotosynthesizerVolume))]
public class PhotosynthesizerVolumeEditor : Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector ();

        PhotosynthesizerVolume script = (PhotosynthesizerVolume) target;
        if (GUILayout.Button ("Regrow")) {
            script.Regrow ();
        }
    }
}