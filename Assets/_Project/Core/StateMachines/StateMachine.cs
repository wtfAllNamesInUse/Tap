using System;
using System.Collections.Generic;
using Zenject;
using Assert = UnityEngine.Assertions.Assert;

namespace TapTapTap.Core.FSM
{
    public class StateMachine : ITickable, IDisposable
    {
        private State currentState;
        private int stateMask;

        private List<State> states;
        private Queue<State> pendingStates;

        public State CurrentState => currentState;
        public int StateMask => stateMask;
        public Blackboard Blackboard => blackboard;

        private TickableManager tickableManager;
        private Blackboard blackboard;

        [Inject]
        public void Inject(TickableManager tickableManager)
        {
            this.tickableManager = tickableManager;
            this.tickableManager.Add(this);
        }

        public void DoInit(IEnumerable<State> states, Blackboard blackboard)
        {
            this.blackboard = blackboard;

            this.states = new List<State>(states);
            foreach (var state in this.states) {
                state.StateMachine = this;
                state.Blackboard = this.blackboard;
            }

            pendingStates = new Queue<State>();
        }

        public virtual void Dispose()
        {
            tickableManager.Remove(this);
            FinishState(EntityStates.Idle);
            // CurrentState?.OnExit();
        }

        public void Tick()
        {
            currentState?.Tick();
        }

        public State ChangeState(int stateID)
        {
            Assert.IsNotNull(states.Find(p => p.StateID.Equals(stateID)));

            if (currentState != null) {
                stateMask &= ~currentState.StateID;
                currentState.OnExit();
            }

            var idx = states.FindIndex(p => p.StateID.Equals(stateID));

            return SetNewState(states[idx]);
        }

        private State SetNewState(State newState)
        {
            currentState = newState;
            currentState.OnEnter();

            stateMask |= currentState.StateID;

            return currentState;
        }

        public void EnqueueState(int stateID)
        {
            var idx = states.FindIndex(p => p.StateID.Equals(stateID));
            Assert.IsTrue(idx >= 0);

            pendingStates.Enqueue(states[idx]);
        }

        public void FinishState(int defaultStateID)
        {
            var hasPendingState = pendingStates.Count > 0;
            if (hasPendingState) {
                if (currentState != null) {
                    stateMask &= ~currentState.StateID;
                    currentState.OnExit();
                }

                var nextState = pendingStates.Dequeue();
                SetNewState(nextState);
            }
            else {
                ChangeState(defaultStateID);
            }
        }
    }
}