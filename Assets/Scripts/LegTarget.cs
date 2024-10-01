using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTarget : MonoBehaviour
{
    [SerializeField] private float _speedStep = 5.0f;
    [SerializeField] private AnimationCurve _amplitudeStep;
    private Vector3 _currentPosition;
    public Movement? _movement;
    private Transform transform;

    public bool IsMoving => _movement != null;

    public Vector3 CurrentPosition => _currentPosition;

    private void Awake()
    {
        transform = base.transform;
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (_movement != null)
        {
            var m = _movement.Value;
            m.Progress = Mathf.Clamp01(m.Progress + Time.deltaTime * _speedStep);
            _currentPosition = m.Evaluate(Vector3.up, _amplitudeStep);
            _movement = m.Progress < 1 ? m : null;

        }

        transform.position = _currentPosition;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (_movement == null)
        {
            _movement = new Movement
            {
                Progress = 0,
                FromPosition = _currentPosition,
                ToPosition = targetPosition
            };
        }
        else
        {
            _movement = new Movement
            {
                Progress = _movement.Value.Progress,
                FromPosition = _movement.Value.FromPosition,
                ToPosition = targetPosition
            };
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(CurrentPosition, 0.25f);
    }

    [Serializable]
    public struct Movement
    {
        public float Progress;
        public Vector3 FromPosition;
        public Vector3 ToPosition;

        public Vector3 Evaluate(in Vector3 up, AnimationCurve amplitudeStep)
        {
            return Vector3.Lerp(@FromPosition, ToPosition, Progress) + up * amplitudeStep.Evaluate(Progress);
        }
    }
}
