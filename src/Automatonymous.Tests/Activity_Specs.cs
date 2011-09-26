﻿// Copyright 2011 Chris Patterson, Dru Sellers
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Automatonymous.Tests
{
    using NUnit.Framework;


    [TestFixture]
    public class When_specifying_an_event_activity
    {
        [Test]
        public void Should_transition_to_the_proper_state()
        {
            Assert.AreEqual(_machine.Running, _instance.CurrentState);
        }

        Instance _instance;
        InstanceStateMachine _machine;

        [TestFixtureSetUp]
        public void Specifying_an_event_activity()
        {
            _instance = new Instance();
            _machine = new InstanceStateMachine();

            _machine.RaiseEvent(_instance, _machine.Initialized);
        }


        class Instance :
            StateMachineInstance
        {
            public State CurrentState { get; set; }
        }


        class InstanceStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public InstanceStateMachine()
            {
                State(() => Running);

                Event(() => Initialized);

                During(Initial,
                    When(Initialized)
                        .TransitionTo(Running));
            }

            public State Running { get; private set; }

            public Event Initialized { get; private set; }
        }
    }

    [TestFixture]
    public class When_specifying_an_event_activity_using_initially
    {
        [Test]
        public void Should_transition_to_the_proper_state()
        {
            Assert.AreEqual(_machine.Running, _instance.CurrentState);
        }

        Instance _instance;
        InstanceStateMachine _machine;

        [TestFixtureSetUp]
        public void Specifying_an_event_activity()
        {
            _instance = new Instance();
            _machine = new InstanceStateMachine();

            _machine.RaiseEvent(_instance, _machine.Initialized);
        }


        class Instance :
            StateMachineInstance
        {
            public State CurrentState { get; set; }
        }


        class InstanceStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public InstanceStateMachine()
            {
                State(() => Running);

                Event(() => Initialized);

                Initially(
                    When(Initialized)
                        .TransitionTo(Running));
            }

            public State Running { get; private set; }

            public Event Initialized { get; private set; }
        }
    }

    [TestFixture]
    public class When_hooking_the_initial_enter_state_event
    {
        [Test]
        public void Should_call_the_activity()
        {
            Assert.AreEqual(_machine.Running, _instance.CurrentState);
        }

        Instance _instance;
        InstanceStateMachine _machine;

        [TestFixtureSetUp]
        public void Specifying_an_event_activity()
        {
            _instance = new Instance();
            _machine = new InstanceStateMachine();

            _machine.RaiseEvent(_instance, _machine.Initialized);
        }


        class Instance :
            StateMachineInstance
        {
            public State CurrentState { get; set; }
        }


        class InstanceStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public InstanceStateMachine()
            {
                State(() => Initializing);
                State(() => Running);

                Event(() => Initialized);

                During(Initializing,
                    When(Initialized)
                        .TransitionTo(Running));

                Anytime(
                    When(Initial.Enter)
                    .TransitionTo(Initializing));
            }

            public State Running { get; private set; }
            public State Initializing { get; private set; }

            public Event Initialized { get; private set; }
        }
    }
}