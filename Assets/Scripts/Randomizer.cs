using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Randomizer : Node
    {
        private Node _currentNode = null;

        public Randomizer() : base() { }
        public Randomizer(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            // If there is no current node in the works, choose a new node at random
            if (_currentNode == null)
            {
                // set random node
                _currentNode = getRandomNode();
                recursivelyResetChildStates();
                return NodeState.RUNNING;
            }

            // Evaluate current node
           NodeState result = _currentNode.Evaluate();
            switch (result)
            {
                // Clear out the current node
                case NodeState.SUCCESS: 
                    _currentNode = null;
                    return success();
                case NodeState.RUNNING:
                    _state = NodeState.RUNNING;
                    return NodeState.RUNNING;
                case NodeState.FAILURE:
                    _state = NodeState.FAILURE;
                    return NodeState.FAILURE;
                default:
                    _state = NodeState.FAILURE;
                    return NodeState.FAILURE;
            }
        }

        private Node getRandomNode()
        {

            // Input: Float [0.0 - 1.0]
            float rand = Random.value;

            // Map the space to the distribution of children
            rand *= children.Count;

            Debug.Log(rand);

            // Get the node index
            int index = Mathf.FloorToInt(rand);

            // Edge: Out of bounds
            if (index >= children.Count)
                index = children.Count - 1;

            return children[index];
        }

        // Clears out the current node
        public override void resetState()
        {
            base.resetState();
            _currentNode = null;
        }
    }

}
