using CuteGothicCatcher.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CuteGothicCatcher.Editors
{
    [CustomPropertyDrawer(typeof(QuestConfigData), true)]
    public class QuestConfigDataDrawer : PropertyDrawer
    {
        private readonly Dictionary<string, bool> m_Foldouts = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            string key = property.propertyPath;
            bool isExpanded = m_Foldouts.TryGetValue(key, out bool foldout) && foldout;

            if (!isExpanded) return EditorGUIUtility.singleLineHeight;

            bool isUsed = property.FindPropertyRelative("m_IsUsed").boolValue;
            int lines = (isUsed ? 3 : 2) + 1;

            return EditorGUIUtility.singleLineHeight * lines + EditorGUIUtility.standardVerticalSpacing * lines;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string key = property.propertyPath;

            var questProp = property.FindPropertyRelative("m_QuestPrefab");
            var isUsedProp = property.FindPropertyRelative("m_IsUsed");
            var weightProp = property.FindPropertyRelative("m_Weight");

            string questName = questProp.objectReferenceValue is Quest quest ? quest.Data.Id : "None";

            Rect foldoutRect = new(position.x - 15, position.y, position.width, EditorGUIUtility.singleLineHeight);
            m_Foldouts[key] = EditorGUI.Foldout(foldoutRect, m_Foldouts.TryGetValue(key, out var expanded) && expanded, questName, true);

            if (!m_Foldouts[key]) return;

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            Rect fieldRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(fieldRect, questProp);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            fieldRect.y = position.y;
            EditorGUI.PropertyField(fieldRect, isUsedProp);

            if (isUsedProp.boolValue)
            {
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                fieldRect.y = position.y;
                EditorGUI.PropertyField(fieldRect, weightProp);
            }
        }
    }
}

