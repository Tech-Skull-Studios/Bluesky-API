/*
 BLUESKY API
 LOVENSE CLASS
 BEHAVIOUR
 v1.0
 LAST EDITED: SATURDAY FEBRUARY 18, 2023
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Bluesky
{
    public abstract class Behaviour : MonoBehaviour
    {
        protected virtual void OnDestroy()
        {
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Type fieldType = field.FieldType;

                if (typeof(IList).IsAssignableFrom(fieldType))
                {
                    if (field.GetValue(this) is IList list)
                        list.Clear();
                }

                if (typeof(IDictionary).IsAssignableFrom(fieldType))
                {
                    if (field.GetValue(this) is IDictionary dictionary)
                        dictionary.Clear();
                }

                if (!fieldType.IsPrimitive)
                    field.SetValue(this, null);
            }
        }
    }
}