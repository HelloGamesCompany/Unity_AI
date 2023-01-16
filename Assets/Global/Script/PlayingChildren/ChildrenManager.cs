using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayingChild> _children;

    public List<Transform> hidingSpots;

    public PlayingChild catcher;
    Vector3 catcherNextPos;
   
    public Vector3 GetNearestChildToCatcher()
    {
        Vector3 closestPosition = Vector3.positiveInfinity;
        foreach (PlayingChild child in _children)
        {
            if (child.state != PlayingChild.ChildState.HIDING)
                continue;
            if (Vector3.Distance(child.transform.position, catcher.transform.position) < Vector3.Distance(closestPosition, catcher.transform.position))
                closestPosition = child.transform.position;
        }

        return closestPosition;
    }

    public void MoveCatcherToRandomPos(Vector3 pos)
    {
        catcher.GoToSpot(pos);
        catcherNextPos = pos;
    }

    public bool IsCatcherGoingToPos()
    {
        return catcher.goingToPos;
    }

    public bool IsCatcherNearbyHidingSpot()
    {
        Vector3 catcherPos = catcher.transform.position;
        foreach (PlayingChild child in _children)
        {
            if (child.state != PlayingChild.ChildState.HIDING)
                continue;

            if (Vector3.Distance(child.transform.position, catcherPos) <= 2.5f)
            {
                Debug.Log("Detected distance: " + Vector3.Distance(child.transform.position, catcherPos));
                return true;
            }
        }
        return false;
    }

    public bool IsCatcherOnPos()
    {
        return Vector3.Distance(catcherNextPos, catcher.transform.position) <= 1.0f;
    }

    public void MoveAllChildrenToCenter()
    {
        foreach (PlayingChild child in _children)
        {
            child.GoToCenter();
        }
    }

    public void SetChildrenHidingSpots()
    {
        int[] randomSpots = new int[_children.Count];
        List<int> availableHidingSpots = new List<int>();

        for (int i = 0; i < hidingSpots.Count; ++i)
        {
            availableHidingSpots.Add(i);
        }

        for (int i = 0;  i < _children.Count; ++i)
        {
            randomSpots[i] = Random.Range(0, availableHidingSpots.Count);
            _children[i].hidingSpot = hidingSpots[randomSpots[i]].position;
            availableHidingSpots.RemoveAt(randomSpots[i]);
        }
    }

    public bool AreChildrenInCenter()
    {
        foreach (PlayingChild child in _children)
        {
            if (Vector3.Distance(child.transform.position, child.centerPos.position) > 1.0f)
                return false;
        }
        return true;
    }

    public void ChooseCatcher()
    {
        int randomChild = Random.Range(0, _children.Count - 1);
        foreach (PlayingChild child in _children)
        {
            child.state = PlayingChild.ChildState.HIDING; // Put all children on hiding mode
        }
        _children[randomChild].state = PlayingChild.ChildState.WAITING; // Put catcher on waiting mode.
        catcher = _children[randomChild];
    }

    public void Hide()
    {
        foreach (PlayingChild child in _children)
        {
            if (child.state == PlayingChild.ChildState.HIDING)
                child.GoToHidingSpot();
        }
    }

}
