using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class Rest : Node
    {
        float _timer = 0.0f;
        float _maxTime;

        public Rest(float maxTime)
        {
            _maxTime = maxTime;
        }
        public override NodeState Evaluate()
        {
            _timer += Time.deltaTime;

            if (_timer < _maxTime)
                return NodeState.RUNNING;
            else
                return success();
        }

        public override void resetState()
        {
            base.resetState();
            _timer = 0.0f;
        }
    }

}