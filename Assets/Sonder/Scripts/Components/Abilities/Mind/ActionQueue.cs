using System;
using System.Linq;
using Boo.Lang;
using UnityEngine;

namespace Sonder.Scripts.Components.Abilities.Mind {
    public enum Action {
        Walk,
        Use,
        Take
    }

    public class ActionQueue {
        private readonly List<Tuple<Action, object>> _queue = new List<Tuple<Action, object>>();

        public Boolean HasActions() {
            return _queue.Count > 0;
        }

        public Tuple<Action, object> GetAction() {
            return _queue.Count > 0 ? _queue.First() : null;
        }

        public void ActionDone() {
            _queue.Remove(_queue.First());
        }

        public void Interrupt() {
            _queue.Clear();
        }

        private void AddAction(Action action, object argument) {
            _queue.Add(new Tuple<Action, object>(action, argument));
        }

        public void AddWalk(float point) {
            AddAction(Action.Walk, point);
        }

        public void AddUse(Usable usable) {
            AddAction(Action.Use, usable);
        }
    }
}