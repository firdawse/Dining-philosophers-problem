using PChecker;
using PChecker.Runtime;
using PChecker.Runtime.StateMachines;
using PChecker.Runtime.Events;
using PChecker.Runtime.Exceptions;
using PChecker.Runtime.Logging;
using PChecker.Runtime.Values;
using PChecker.Runtime.Specifications;
using Monitor = PChecker.Runtime.Specifications.Monitor;
using System;
using PChecker.SystematicTesting;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 162, 219, 414, 1998
namespace PImplementation
{
}
namespace PImplementation
{
    internal partial class ePickUp : Event
    {
        public ePickUp() : base() {}
        public ePickUp (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ePickUp();}
    }
}
namespace PImplementation
{
    internal partial class ePutDown : Event
    {
        public ePutDown() : base() {}
        public ePutDown (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ePutDown();}
    }
}
namespace PImplementation
{
    internal partial class ForkGranted : Event
    {
        public ForkGranted() : base() {}
        public ForkGranted (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ForkGranted();}
    }
}
namespace PImplementation
{
    internal partial class ForkReleased : Event
    {
        public ForkReleased() : base() {}
        public ForkReleased (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ForkReleased();}
    }
}
namespace PImplementation
{
    internal partial class ForkBusy : Event
    {
        public ForkBusy() : base() {}
        public ForkBusy (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ForkBusy();}
    }
}
namespace PImplementation
{
    public static partial class GlobalFunctions
    {
        public static void SetupSystem(PInt n, PBool reverseOne, StateMachine currentMachine)
        {
            PMap forks = new PMap();
            PInt i = ((PInt)0);
            PMachineValue left = null;
            PMachineValue right = null;
            PBool rev = ((PBool)false);
            PBool TMP_tmp0 = ((PBool)false);
            PBool TMP_tmp1 = ((PBool)false);
            PMachineValue TMP_tmp2 = null;
            PInt TMP_tmp3 = ((PInt)0);
            PBool TMP_tmp4 = ((PBool)false);
            PBool TMP_tmp5 = ((PBool)false);
            PMachineValue TMP_tmp6 = null;
            PMachineValue TMP_tmp7 = null;
            PInt TMP_tmp8 = ((PInt)0);
            PInt TMP_tmp9 = ((PInt)0);
            PMachineValue TMP_tmp10 = null;
            PMachineValue TMP_tmp11 = null;
            PBool TMP_tmp12 = ((PBool)false);
            PBool TMP_tmp13 = ((PBool)false);
            PMachineValue TMP_tmp14 = null;
            PMachineValue TMP_tmp15 = null;
            PInt TMP_tmp16 = ((PInt)0);
            PBool TMP_tmp17 = ((PBool)false);
            PNamedTuple TMP_tmp18 = (new PNamedTuple(new string[]{"left","right","id","reverse"},null, null, ((PInt)0), ((PBool)false)));
            PInt TMP_tmp19 = ((PInt)0);
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp0 = (PBool)((i) < (n));
                TMP_tmp1 = (PBool)(((PBool)((IPValue)TMP_tmp0)?.Clone()));
                if (TMP_tmp1)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp2 = (PMachineValue)(currentMachine.CreateInterface<I_Fork>( currentMachine));
                ((PMap)forks)[i] = TMP_tmp2;
                TMP_tmp3 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp3;
            }
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp4 = (PBool)((i) < (n));
                TMP_tmp5 = (PBool)(((PBool)((IPValue)TMP_tmp4)?.Clone()));
                if (TMP_tmp5)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp6 = (PMachineValue)(((PMap)forks)[i]);
                TMP_tmp7 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp6)?.Clone()));
                left = TMP_tmp7;
                TMP_tmp8 = (PInt)((i) + (((PInt)(1))));
                TMP_tmp9 = (PInt)((TMP_tmp8) % (n));
                TMP_tmp10 = (PMachineValue)(((PMap)forks)[TMP_tmp9]);
                TMP_tmp11 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp10)?.Clone()));
                right = TMP_tmp11;
                TMP_tmp13 = (PBool)(((PBool)((IPValue)reverseOne)?.Clone()));
                if (TMP_tmp13)
                {
                    TMP_tmp12 = (PBool)((PValues.SafeEquals(i,((PInt)(0)))));
                    TMP_tmp13 = (PBool)(((PBool)((IPValue)TMP_tmp12)?.Clone()));
                }
                rev = TMP_tmp13;
                TMP_tmp14 = (PMachineValue)(((PMachineValue)((IPValue)left)?.Clone()));
                TMP_tmp15 = (PMachineValue)(((PMachineValue)((IPValue)right)?.Clone()));
                TMP_tmp16 = (PInt)(((PInt)((IPValue)i)?.Clone()));
                TMP_tmp17 = (PBool)(((PBool)((IPValue)rev)?.Clone()));
                TMP_tmp18 = (PNamedTuple)((new PNamedTuple(new string[]{"left","right","id","reverse"}, TMP_tmp14, TMP_tmp15, TMP_tmp16, TMP_tmp17)));
                currentMachine.CreateInterface<I_Philosopher>(currentMachine, TMP_tmp18);
                TMP_tmp19 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp19;
            }
        }
    }
}
namespace PImplementation
{
    internal partial class NoDeadlock : Monitor
    {
        private PSet holders = new PSet();
        static NoDeadlock() {
            observes.Add(nameof(ForkGranted));
            observes.Add(nameof(ForkReleased));
        }
        
        public void Anon(Event currentMachine_dequeuedEvent)
        {
            NoDeadlock currentMachine = this;
            PMachineValue p = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_1 = null;
            PInt TMP_tmp1_1 = ((PInt)0);
            PBool TMP_tmp2_1 = ((PBool)false);
            PString TMP_tmp3_1 = ((PString)"");
            PString TMP_tmp4_1 = ((PString)"");
            PString TMP_tmp5_1 = ((PString)"");
            TMP_tmp0_1 = (PMachineValue)(((PMachineValue)((IPValue)p)?.Clone()));
            ((PSet)holders).Add(TMP_tmp0_1);
            TMP_tmp1_1 = (PInt)(((PInt)(holders).Count));
            TMP_tmp2_1 = (PBool)((TMP_tmp1_1) < (((PInt)(7))));
            if (TMP_tmp2_1)
            {
            }
            else
            {
                TMP_tmp3_1 = (PString)(((PString) String.Format("PSpec\\Deadlock.p:8:7")));
                TMP_tmp4_1 = (PString)(((PString) String.Format("Deadlock risk: All 5 philosophers hold a fork!")));
                TMP_tmp5_1 = (PString)(((PString) String.Format("{0} {1}",TMP_tmp3_1,TMP_tmp4_1)));
                currentMachine.Assert(TMP_tmp2_1,"Assertion Failed: " + TMP_tmp5_1);
            }
        }
        public void Anon_1(Event currentMachine_dequeuedEvent)
        {
            NoDeadlock currentMachine = this;
            PMachineValue p_1 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            ((PSet)holders).Remove(p_1);
        }
        [Start]
        [OnEventDoAction(typeof(ForkGranted), nameof(Anon))]
        [OnEventDoAction(typeof(ForkReleased), nameof(Anon_1))]
        class Monitoring : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class Fork : StateMachine
    {
        private PMachineValue holder = null;
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Fork() {
            this.sends.Add(nameof(ForkBusy));
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkBusy));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon_2(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            PMachineValue p_2 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_2 = null;
            Event TMP_tmp1_2 = null;
            PMachineValue TMP_tmp2_2 = null;
            holder = (PMachineValue)(((PMachineValue)((IPValue)p_2)?.Clone()));
            TMP_tmp0_2 = (PMachineValue)(((PMachineValue)((IPValue)holder)?.Clone()));
            TMP_tmp1_2 = (Event)(new ForkGranted(null));
            TMP_tmp2_2 = (PMachineValue)(((PMachineValue)((IPValue)p_2)?.Clone()));
            TMP_tmp1_2.Payload = TMP_tmp2_2;
            currentMachine.SendEvent(TMP_tmp0_2, (Event)TMP_tmp1_2);
            currentMachine.RaiseGotoStateEvent<Held>();
            return;
        }
        public void Anon_3(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            PMachineValue p_3 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PBool TMP_tmp0_3 = ((PBool)false);
            PString TMP_tmp1_3 = ((PString)"");
            PMachineValue TMP_tmp2_3 = null;
            Event TMP_tmp3_2 = null;
            PMachineValue TMP_tmp4_2 = null;
            TMP_tmp0_3 = (PBool)((PValues.SafeEquals(p_3,holder)));
            if (TMP_tmp0_3)
            {
            }
            else
            {
                TMP_tmp1_3 = (PString)(((PString) String.Format("PSrc\\Fork.p:20:7")));
                currentMachine.Assert(TMP_tmp0_3,"Assertion Failed: " + TMP_tmp1_3);
            }
            TMP_tmp2_3 = (PMachineValue)(((PMachineValue)((IPValue)holder)?.Clone()));
            TMP_tmp3_2 = (Event)(new ForkReleased(null));
            TMP_tmp4_2 = (PMachineValue)(((PMachineValue)((IPValue)p_3)?.Clone()));
            TMP_tmp3_2.Payload = TMP_tmp4_2;
            currentMachine.SendEvent(TMP_tmp2_3, (Event)TMP_tmp3_2);
            currentMachine.RaiseGotoStateEvent<Free>();
            return;
        }
        public void Anon_4(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            PMachineValue p_4 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_4 = null;
            Event TMP_tmp1_4 = null;
            PMachineValue TMP_tmp2_4 = null;
            TMP_tmp0_4 = (PMachineValue)(((PMachineValue)((IPValue)p_4)?.Clone()));
            TMP_tmp1_4 = (Event)(new ForkBusy(null));
            TMP_tmp2_4 = (PMachineValue)(((PMachineValue)((IPValue)p_4)?.Clone()));
            TMP_tmp1_4.Payload = TMP_tmp2_4;
            currentMachine.SendEvent(TMP_tmp0_4, (Event)TMP_tmp1_4);
        }
        [Start]
        [OnEventDoAction(typeof(ePickUp), nameof(Anon_2))]
        class Free : State
        {
        }
        [OnEventDoAction(typeof(ePutDown), nameof(Anon_3))]
        [OnEventDoAction(typeof(ePickUp), nameof(Anon_4))]
        class Held : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class Philosopher : StateMachine
    {
        private PMachineValue left_1 = null;
        private PMachineValue right_1 = null;
        private PInt id = ((PInt)0);
        private PBool reverse = ((PBool)false);
        private PInt forksHeld = ((PInt)0);
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Philosopher() {
            this.sends.Add(nameof(ForkBusy));
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkBusy));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon_5(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PNamedTuple init = (PNamedTuple)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_5 = null;
            PMachineValue TMP_tmp1_5 = null;
            PMachineValue TMP_tmp2_5 = null;
            PMachineValue TMP_tmp3_3 = null;
            PInt TMP_tmp4_3 = ((PInt)0);
            PInt TMP_tmp5_2 = ((PInt)0);
            PBool TMP_tmp6_1 = ((PBool)false);
            PBool TMP_tmp7_1 = ((PBool)false);
            TMP_tmp0_5 = (PMachineValue)(((PNamedTuple)init)["left"]);
            TMP_tmp1_5 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp0_5)?.Clone()));
            left_1 = TMP_tmp1_5;
            TMP_tmp2_5 = (PMachineValue)(((PNamedTuple)init)["right"]);
            TMP_tmp3_3 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp2_5)?.Clone()));
            right_1 = TMP_tmp3_3;
            TMP_tmp4_3 = (PInt)(((PNamedTuple)init)["id"]);
            TMP_tmp5_2 = (PInt)(((PInt)((IPValue)TMP_tmp4_3)?.Clone()));
            id = TMP_tmp5_2;
            TMP_tmp6_1 = (PBool)(((PNamedTuple)init)["reverse"]);
            TMP_tmp7_1 = (PBool)(((PBool)((IPValue)TMP_tmp6_1)?.Clone()));
            reverse = TMP_tmp7_1;
            if (reverse)
            {
                currentMachine.RaiseGotoStateEvent<TryRightFirst>();
                return;
            }
            else
            {
                currentMachine.RaiseGotoStateEvent<TryLeftFirst>();
                return;
            }
        }
        public void Anon_6(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_6 = null;
            Event TMP_tmp1_6 = null;
            PMachineValue TMP_tmp2_6 = null;
            TMP_tmp0_6 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
            TMP_tmp1_6 = (Event)(new ePickUp(null));
            TMP_tmp2_6 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_6.Payload = TMP_tmp2_6;
            currentMachine.SendEvent(TMP_tmp0_6, (Event)TMP_tmp1_6);
        }
        public void Anon_7(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_7 = null;
            Event TMP_tmp1_7 = null;
            PMachineValue TMP_tmp2_7 = null;
            TMP_tmp0_7 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
            TMP_tmp1_7 = (Event)(new ePickUp(null));
            TMP_tmp2_7 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_7.Payload = TMP_tmp2_7;
            currentMachine.SendEvent(TMP_tmp0_7, (Event)TMP_tmp1_7);
        }
        public void Anon_8(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_8 = null;
            Event TMP_tmp1_8 = null;
            PMachineValue TMP_tmp2_8 = null;
            TMP_tmp0_8 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
            TMP_tmp1_8 = (Event)(new ePickUp(null));
            TMP_tmp2_8 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_8.Payload = TMP_tmp2_8;
            currentMachine.SendEvent(TMP_tmp0_8, (Event)TMP_tmp1_8);
        }
        public void Anon_9(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_9 = null;
            Event TMP_tmp1_9 = null;
            PMachineValue TMP_tmp2_9 = null;
            TMP_tmp0_9 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
            TMP_tmp1_9 = (Event)(new ePickUp(null));
            TMP_tmp2_9 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_9.Payload = TMP_tmp2_9;
            currentMachine.SendEvent(TMP_tmp0_9, (Event)TMP_tmp1_9);
        }
        public void Anon_10(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_10 = null;
            Event TMP_tmp1_10 = null;
            PMachineValue TMP_tmp2_10 = null;
            TMP_tmp0_10 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
            TMP_tmp1_10 = (Event)(new ePickUp(null));
            TMP_tmp2_10 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_10.Payload = TMP_tmp2_10;
            currentMachine.SendEvent(TMP_tmp0_10, (Event)TMP_tmp1_10);
        }
        public void Anon_11(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_11 = null;
            Event TMP_tmp1_11 = null;
            PMachineValue TMP_tmp2_11 = null;
            TMP_tmp0_11 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
            TMP_tmp1_11 = (Event)(new ePickUp(null));
            TMP_tmp2_11 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_11.Payload = TMP_tmp2_11;
            currentMachine.SendEvent(TMP_tmp0_11, (Event)TMP_tmp1_11);
        }
        public void Anon_12(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_12 = null;
            Event TMP_tmp1_12 = null;
            PMachineValue TMP_tmp2_12 = null;
            TMP_tmp0_12 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
            TMP_tmp1_12 = (Event)(new ePickUp(null));
            TMP_tmp2_12 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_12.Payload = TMP_tmp2_12;
            currentMachine.SendEvent(TMP_tmp0_12, (Event)TMP_tmp1_12);
        }
        public void Anon_13(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_13 = null;
            Event TMP_tmp1_13 = null;
            PMachineValue TMP_tmp2_13 = null;
            TMP_tmp0_13 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
            TMP_tmp1_13 = (Event)(new ePickUp(null));
            TMP_tmp2_13 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_13.Payload = TMP_tmp2_13;
            currentMachine.SendEvent(TMP_tmp0_13, (Event)TMP_tmp1_13);
        }
        public void Anon_14(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_14 = null;
            Event TMP_tmp1_14 = null;
            PMachineValue TMP_tmp2_14 = null;
            PMachineValue TMP_tmp3_4 = null;
            Event TMP_tmp4_4 = null;
            PMachineValue TMP_tmp5_3 = null;
            forksHeld = (PInt)(((PInt)(0)));
            TMP_tmp0_14 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
            TMP_tmp1_14 = (Event)(new ePutDown(null));
            TMP_tmp2_14 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_14.Payload = TMP_tmp2_14;
            currentMachine.SendEvent(TMP_tmp0_14, (Event)TMP_tmp1_14);
            TMP_tmp3_4 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
            TMP_tmp4_4 = (Event)(new ePutDown(null));
            TMP_tmp5_3 = (PMachineValue)(currentMachine.self);
            TMP_tmp4_4.Payload = TMP_tmp5_3;
            currentMachine.SendEvent(TMP_tmp3_4, (Event)TMP_tmp4_4);
        }
        public void Anon_15(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PInt TMP_tmp0_15 = ((PInt)0);
            PBool TMP_tmp1_15 = ((PBool)false);
            TMP_tmp0_15 = (PInt)((forksHeld) + (((PInt)(1))));
            forksHeld = TMP_tmp0_15;
            TMP_tmp1_15 = (PBool)((PValues.SafeEquals(forksHeld,((PInt)(2)))));
            if (TMP_tmp1_15)
            {
                if (reverse)
                {
                    currentMachine.RaiseGotoStateEvent<TryRightFirst>();
                    return;
                }
                else
                {
                    currentMachine.RaiseGotoStateEvent<TryLeftFirst>();
                    return;
                }
            }
        }
        [Start]
        [OnEntry(nameof(Anon_5))]
        class Init : State
        {
        }
        [OnEntry(nameof(Anon_6))]
        [OnEventGotoState(typeof(ForkGranted), typeof(TryRight))]
        [OnEventDoAction(typeof(ForkBusy), nameof(Anon_7))]
        class TryLeftFirst : State
        {
        }
        [OnEntry(nameof(Anon_8))]
        [OnEventGotoState(typeof(ForkGranted), typeof(TryLeft))]
        [OnEventDoAction(typeof(ForkBusy), nameof(Anon_9))]
        class TryRightFirst : State
        {
        }
        [OnEntry(nameof(Anon_10))]
        [OnEventGotoState(typeof(ForkGranted), typeof(Eating))]
        [OnEventDoAction(typeof(ForkBusy), nameof(Anon_11))]
        class TryLeft : State
        {
        }
        [OnEntry(nameof(Anon_12))]
        [OnEventGotoState(typeof(ForkGranted), typeof(Eating))]
        [OnEventDoAction(typeof(ForkBusy), nameof(Anon_13))]
        class TryRight : State
        {
        }
        [OnEntry(nameof(Anon_14))]
        [OnEventDoAction(typeof(ForkReleased), nameof(Anon_15))]
        class Eating : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class TestV1 : StateMachine
    {
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public TestV1() {
            this.sends.Add(nameof(ForkBusy));
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkBusy));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_16(Event currentMachine_dequeuedEvent)
        {
            TestV1 currentMachine = this;
            PInt TMP_tmp0_16 = ((PInt)0);
            PBool TMP_tmp1_16 = ((PBool)false);
            TMP_tmp0_16 = (PInt)(((PInt)(7)));
            TMP_tmp1_16 = (PBool)(((PBool)false));
            GlobalFunctions.SetupSystem(TMP_tmp0_16, TMP_tmp1_16, currentMachine);
        }
        [Start]
        [OnEntry(nameof(Anon_16))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class TestV2 : StateMachine
    {
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public TestV2() {
            this.sends.Add(nameof(ForkBusy));
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkBusy));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_17(Event currentMachine_dequeuedEvent)
        {
            TestV2 currentMachine = this;
            PInt TMP_tmp0_17 = ((PInt)0);
            PBool TMP_tmp1_17 = ((PBool)false);
            TMP_tmp0_17 = (PInt)(((PInt)(7)));
            TMP_tmp1_17 = (PBool)(((PBool)true));
            GlobalFunctions.SetupSystem(TMP_tmp0_17, TMP_tmp1_17, currentMachine);
        }
        [Start]
        [OnEntry(nameof(Anon_17))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    public class tcDeadlockV1 {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestV1)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestV1)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_TestV1)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_TestV1), typeof(TestV1));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(NoDeadlock)] = new List<string>();
            PModule.monitorObserves[nameof(NoDeadlock)].Add(nameof(ForkGranted));
            PModule.monitorObserves[nameof(NoDeadlock)].Add(nameof(ForkReleased));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(NoDeadlock));
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(NoDeadlock));
            PModule.monitorMap[nameof(I_TestV1)] = new List<Type>();
            PModule.monitorMap[nameof(I_TestV1)].Add(typeof(NoDeadlock));
            runtime.RegisterMonitor<NoDeadlock>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(TestV1), "TestV1");
        }
    }
}
namespace PImplementation
{
    public class tcNoDeadlockV2 {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestV2)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestV2)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_TestV2)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_TestV2), typeof(TestV2));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(NoDeadlock)] = new List<string>();
            PModule.monitorObserves[nameof(NoDeadlock)].Add(nameof(ForkGranted));
            PModule.monitorObserves[nameof(NoDeadlock)].Add(nameof(ForkReleased));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(NoDeadlock));
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(NoDeadlock));
            PModule.monitorMap[nameof(I_TestV2)] = new List<Type>();
            PModule.monitorMap[nameof(I_TestV2)].Add(typeof(NoDeadlock));
            runtime.RegisterMonitor<NoDeadlock>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(TestV2), "TestV2");
        }
    }
}
// TODO: NamedModule Dining
namespace PImplementation
{
    public class I_Fork : PMachineValue {
        public I_Fork (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_Philosopher : PMachineValue {
        public I_Philosopher (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_TestV1 : PMachineValue {
        public I_TestV1 (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_TestV2 : PMachineValue {
        public I_TestV2 (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public partial class PHelper {
        public static void InitializeInterfaces() {
            PInterfaces.Clear();
            PInterfaces.AddInterface(nameof(I_Fork), nameof(ForkBusy), nameof(ForkGranted), nameof(ForkReleased), nameof(ePickUp), nameof(ePutDown), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_Philosopher), nameof(ForkBusy), nameof(ForkGranted), nameof(ForkReleased), nameof(ePickUp), nameof(ePutDown), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_TestV1), nameof(ForkBusy), nameof(ForkGranted), nameof(ForkReleased), nameof(ePickUp), nameof(ePutDown), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_TestV2), nameof(ForkBusy), nameof(ForkGranted), nameof(ForkReleased), nameof(ePickUp), nameof(ePutDown), nameof(PHalt));
        }
    }
    
}
namespace PImplementation
{
    public partial class PHelper {
        public static void InitializeEnums() {
            PEnum.Clear();
        }
    }
    
}
#pragma warning restore 162, 219, 414
