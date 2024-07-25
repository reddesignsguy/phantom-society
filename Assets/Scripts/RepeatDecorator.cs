using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RepeatDecorator : Node
    {
        private int _maxCount;
        private int _count;

        public RepeatDecorator() : base() { }
        public RepeatDecorator(int maxCount, List<Node> children) : base(children)
        {
            if (children.Count != 1)
            {
                throw new System.Exception("Repeat Decorator node received > 1 children. Expected only one child.");
            }

            _maxCount = maxCount;
            _count = 0;

        }

        // Repeats the node max count times
        public override NodeState Evaluate()
        {
            // Evaluate the child node -> if it's successful, count += 1
            Node child = children[0];

            if (_count < _maxCount)
            {
                NodeState result = child.Evaluate();

                if (result == NodeState.SUCCESS)
                {
                    _count += 1;
                    recursivelyResetChildStates();
                }

                return NodeState.RUNNING;
            }

            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;

        }

        public override void resetState()
        {
            base.resetState();
            _count = 0;
        }
    }
}
