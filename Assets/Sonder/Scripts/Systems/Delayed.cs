using UnityEngine;

public class Delayed
{
    protected float _delay = 0.25f;
    private float _nextUpdateTime = 0;

    protected bool CantUpdate()
    {
        if (Time.time < _nextUpdateTime)
        {
            return true;
        }
        _nextUpdateTime = Time.time + _delay;
        return false;
    }
}