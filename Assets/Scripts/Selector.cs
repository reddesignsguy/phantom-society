using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class Selector : Node
{

    public Selector() : base() { }
    public Selector(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (Node child in children)
        {
            if (child.getState() == NodeState.FAILURE)
                continue;

            NodeState result = child.Evaluate();
            switch (result)
            {
                case NodeState.SUCCESS:
                    _state = NodeState.SUCCESS;
                    return NodeState.SUCCESS;
                case NodeState.RUNNING:
                    _state = NodeState.RUNNING;
                    return NodeState.RUNNING;
                case NodeState.FAILURE:
                    continue;
            }
        }

        _state = NodeState.FAILURE;
        return NodeState.FAILURE;
    }
}
