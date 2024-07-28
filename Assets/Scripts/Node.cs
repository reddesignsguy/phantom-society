using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public Node parent;
        protected List<Node> children = new List<Node>();
        protected NodeState _state;

        private static Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach ( Node child in children)
            {
                _Link(child);
            }
        }

        private void _Link(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
        public virtual void resetState() { setState(default(NodeState)); }

        public static void setData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public static object getData(string key)
        {
            object result = null;
            _dataContext.TryGetValue(key, out result);
            return result;
        }

        public NodeState getState()
        {
            return _state;
        }

        private void setState(NodeState state)
        {
            _state = state;
        }

        protected void recursivelyResetChildStates()
        {
            foreach(Node child in children)
            {
                child.resetState();
                child.recursivelyResetChildStates();
            }
        }

        protected NodeState success()
        {
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
