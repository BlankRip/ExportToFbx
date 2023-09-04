namespace Blank.Attributes
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfPropertyDrawer : PropertyDrawer
    {
        DisableIfAttribute drawIf;

        // Field that is being compared.
        SerializedProperty comparedField;

        // Height of the property.
        private float propertyHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return propertyHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            drawIf = attribute as DisableIfAttribute;
            comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);

            // Get the value of the compared field.
            bool comparedFieldValue = comparedField.boolValue;

            //Object targetObject = comparedField.serializedObject.targetObject;
            //System.Type targetObjectClassType = targetObject.GetType();
            //float numericComparedFieldValue = (int) 0;

            // Is the condition met? Should the field be drawn?
            bool conditionMet = false;

            switch (drawIf.comparisonType)
            {
                case ComparisonType.Equals:
                    if (comparedFieldValue.Equals(drawIf.comparedValue))
                        conditionMet = true;
                    break;

                case ComparisonType.NotEqual:
                    if (!comparedFieldValue.Equals(drawIf.comparedValue))
                        conditionMet = true;
                    break;

                //case ComparisonType.GreaterThan:
                //    if (numericComparedFieldValue > numericComparedValue)
                //        conditionMet = true;
                //    break;

                //case ComparisonType.SmallerThan:
                //    if (numericComparedFieldValue < numericComparedValue)
                //        conditionMet = true;
                //    break;

                //case ComparisonType.SmallerOrEqual:
                //    if (numericComparedFieldValue <= numericComparedValue)
                //        conditionMet = true;
                //    break;

                //case ComparisonType.GreaterOrEqual:
                //    if (numericComparedFieldValue >= numericComparedValue)
                //        conditionMet = true;
                //    break;
            }

            propertyHeight = base.GetPropertyHeight(property, label);
            if (conditionMet)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                if (drawIf.disablingType == DisablingType.ReadOnly)
                {
                    GUI.enabled = false;
                    EditorGUI.PropertyField(position, property, label);
                    GUI.enabled = true;
                }
                else
                {
                    propertyHeight = 0f;
                }
            }
        }
    }
}