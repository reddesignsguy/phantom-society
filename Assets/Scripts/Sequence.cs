using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {

        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                if (child.getState() == NodeState.SUCCESS)
                    continue;

                NodeState result = child.Evaluate();
                switch (result)
                {
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return NodeState.RUNNING;
                    case NodeState.FAILURE:
                        _state = NodeState.FAILURE;
                        return NodeState.FAILURE;
                }
            }

            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }

}