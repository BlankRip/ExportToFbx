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
            if(comparedField == null)
            {
                Debug.LogError($"The Compare PropertyName passed in the DisableIfAttribute of the {property.serializedObject.targetObject.GetType()} script is Not Valid, " +
                    $"placed on variable with display name: {property.displayName}.");
                propertyHeight = base.GetPropertyHeight(property, label);
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            // Get the value of the compared field.
            SerializedPropertyType propertyType = comparedField.propertyType;
            object comparedFieldValue = null;
            float numericComparedFieldValue = (int)0;
            float numericComparedValue = (int)0;
            if (propertyType == SerializedPropertyType.Boolean)
            {
                comparedFieldValue = comparedField.boolValue;
            }
            else if (propertyType == SerializedPropertyType.Float)
            {
                comparedFieldValue = comparedField.floatValue;
                numericComparedFieldValue = comparedField.floatValue;
                numericComparedValue = (float)drawIf.comparedValue;
            }
            else if (propertyType == SerializedPropertyType.Integer)
            {
                comparedFieldValue = comparedField.intValue;
                numericComparedFieldValue = comparedField.intValue;
                numericComparedValue = (int)drawIf.comparedValue;
            }

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

                case ComparisonType.GreaterThan:
                    if (numericComparedFieldValue > numericComparedValue)
                        conditionMet = true;
                    break;

                case ComparisonType.LessThan:
                    if (numericComparedFieldValue < numericComparedValue)
                        conditionMet = true;
                    break;

                case ComparisonType.LessOrEqual:
                    if (numericComparedFieldValue <= numericComparedValue)
                        conditionMet = true;
                    break;

                case ComparisonType.GreaterOrEqual:
                    if (numericComparedFieldValue >= numericComparedValue)
                        conditionMet = true;
                    break;
            }

            propertyHeight = base.GetPropertyHeight(property, label);
            if (!conditionMet)
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