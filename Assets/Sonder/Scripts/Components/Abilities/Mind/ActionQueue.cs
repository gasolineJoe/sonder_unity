using System;
using System.Linq;
using Boo.Lang;

namespace Sonder.Scripts.Components.Abilities.Mind {
    public enum Action {
        Walk,
        Use,
        Take
    }

    public class ActionQueue {
        private List<Tuple<Action, int>> _queue = new List<Tuple<Action, int>>();

        public Boolean HasActions() {
            return _queue.Count > 0;
        }

        public Tuple<Action, int> GetAction() {
            if (_queue.Count > 0) {
                return _queue.First();
            }
            return null;
        }

        public void ActionDone() {
            _queue.Remove(_queue.First());
        }

        public void Interrupt() {
            _queue.Clear();
        }

        public void AddAction(Action action, int argument) {
            _queue.Add(new Tuple<Action, int>(action, argument));
        }
    }
}