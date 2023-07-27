
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Track))]
public class TrackEditor : Editor
{
    SerializedProperty lanePrefabProp;
    SerializedProperty laneWidthProp;

    SerializedObject so;

    private void OnEnable()
    {
        so = serializedObject;

        lanePrefabProp = so.FindProperty("lanePrefab");
        laneWidthProp = so.FindProperty("laneWidth");
    }


    public override void OnInspectorGUI()
    {

        Track track = target as Track;
        int numberOfLanes = track.transform.childCount;

        so.Update();
        laneWidthProp.floatValue = EditorGUILayout.FloatField(laneWidthProp.floatValue);
        if (so.ApplyModifiedProperties())
        {
            OrganizeLanes(track);
        }

        if (GUILayout.Button("Add Lane"))
        {
            AddLane(track, numberOfLanes);
        }
        if (GUILayout.Button("Remove Lane"))
        {
            RemoveLastLane(track, numberOfLanes);
        }
    }

    private void OrganizeLanes(Track track)
    {
        for (int i = 0; i < track.transform.childCount; i++)
        {
            Transform laneTransform = track.transform.GetChild(i);
            laneTransform.position = track.transform.position + track.transform.right * i * laneWidthProp.floatValue;
        }
    }

    private void RemoveLastLane(Track track, int numberOfLanes)
    {
        if (numberOfLanes > 0)
        {
            GameObject lastLane = track.transform.GetChild(numberOfLanes - 1).gameObject;
            Destroy(lastLane);
        }
    }

    private void AddLane(Track track, int numberOfLanes)
    {
        GameObject prefab = (GameObject)lanePrefabProp.objectReferenceValue;
        GameObject newLane = (GameObject)PrefabUtility.InstantiatePrefab(prefab, track.transform);
        //GameObject newLane = track.transform.GetChild(numberOfLanes).gameObject;
        newLane.transform.position = track.transform.position + track.transform.right * (numberOfLanes) * laneWidthProp.floatValue;
    }

}