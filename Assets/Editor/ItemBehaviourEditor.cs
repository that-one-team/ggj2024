using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemBehaviour))]
public class ItemBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemBehaviour targ = target as ItemBehaviour;

        if (GUILayout.Button("Update Visuals"))
        {
            UpdateVisuals(targ);
        }

        if (targ.transform.childCount > 0) return;

        if (GUILayout.Button("Update indicator"))
        {
            var indicatorPrefab = Resources.Load("Prefabs/UI/Interact Indicator", typeof(GameObject));
            targ.IndicatorUI = Instantiate(indicatorPrefab, targ.transform) as GameObject;
            UpdateVisuals(targ);
        }
    }

    void UpdateVisuals(ItemBehaviour targ)
    {
        targ.UpdateVisuals(targ.Data);
        targ.transform.localScale = Vector3.one * 0.2f;
        targ.tag = "Interactable";

        serializedObject.ApplyModifiedProperties();
    }
}