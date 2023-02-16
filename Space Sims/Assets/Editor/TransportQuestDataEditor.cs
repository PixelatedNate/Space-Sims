using UnityEditor;


[CustomEditor(typeof(TransportQuestData), true)]
public class TransportQuestDataEditor : QuestDataEditor
{

    bool showRequiments;

    public override void OnInspectorGUI()
    {
        TransportQuestData questDataTarget = (TransportQuestData)target;

        base.OnInspectorGUI();

        var m_IntProperty = serializedObject.FindProperty("TargetPlanetData");

        EditorGUILayout.PropertyField(m_IntProperty, includeChildren: true);

        serializedObject.ApplyModifiedProperties();


    }

}


