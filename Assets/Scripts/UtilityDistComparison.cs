using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityDistComparison : MonoBehaviour
{
    private Transform _trans;

    private void Start()
    {
        gameObject.TryGetComponent<Transform>(out _trans);
    }

    public float CheckDistance(Transform compare)
    {
        return Vector3.Distance(_trans.position, compare.position);
    }

    public bool CheckDistance(Transform compare, float threshold) // return true if inside threshold or range
    {
        if (Vector3.Distance(_trans.position, compare.position) < threshold)
        {
            return true;
        }

        return false;
    }

    public int CheckDistance(List<Transform> compare) // return index of closest transform
    {
        int index = 0;
        float closestDistance = 50f;

        for (int i = 0; i < compare.Count; i++)
        {
            if (CheckDistance(compare[i], closestDistance))
            {
                closestDistance = CheckDistance(compare[i]);
                index = i;
            }
        }

        return index;
    }

    public List<Transform> CheckDistance(List<Transform> compare, float threshold) // return a list of transforms within a threshold
    {
        for (int i = compare.Count; i > 0; i--)
        {
            if (!CheckDistance(compare[i], threshold))
            {
                compare.Remove(compare[i]);
            }
        }

        return compare;
    }

    public List<GameObject> CheckDistance(List<GameObject> compare, float threshold) // return gameobjects within a threshold, given a list of objects
    {
        List<Transform> temp = ConvertObjToTrans(compare);

        for (int i = compare.Count; i > 0; i--)
        {
            if (!CheckDistance(temp[i], threshold))
            {
                compare.Remove(compare[i]);
            }
        }

        return compare;
    }

    public List<Transform> ConvertObjToTrans(List<GameObject> obj)
    {
        List<Transform> ret = new List<Transform>();

        for (int i = 0; i < obj.Count; i++)
        {
            ret.Add(obj[i].GetComponent<Transform>());
        }

        return ret;
    }
}