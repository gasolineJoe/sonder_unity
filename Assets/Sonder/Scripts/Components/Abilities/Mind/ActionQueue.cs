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
        private List<Tuple<Action, float>> _queue = new List<Tuple<Action, float>>();

        public Boolean HasActions() {
            return _queue.Count > 0;
        }

        public Tuple<Action, float> GetAction() {
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

        public void AddAction(Action action, float argument) {
            _queue.Add(new Tuple<Action, float>(action, argument));
        }
    }
}