using System;
using System.Collections.Generic;
using AI.GOAP;
using UnityEngine;


public class BeliefFacotry {
   private readonly GoapAgent agent;
   private readonly Dictionary<string, AgentBelief> beliefs;

   public BeliefFacotry(GoapAgent agent, Dictionary<string, AgentBelief> beliefs) {
      this.agent = agent;
      this.beliefs = beliefs;
   }

   public void AddBelief(string key, Func<bool> condition) {
      beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(condition)
         .Build());
   }

   public void AddSensorBelief(string key, Sensor sensor) {
      beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(() => sensor.IsTargetInRage)
         .WithLocation(() => sensor.TargetPosition)
         .Build());
   }
   
   public void AddLocationBelief(string key, float distance, Vector3 locationCondition) {
      beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(() => InRangeOf(locationCondition, distance))
         .WithLocation(() => locationCondition)
         .Build());
   }
   
   bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(agent.transform.position, pos) < range;
}


public class AgentBelief
{
   public string name { get; }
   
   private Func<bool> condition = () => false;
   private Func<Vector3> observedLocation = () => Vector3.zero;
   
   
   public Vector3 location => observedLocation();

   public AgentBelief(string name) {
      this.name = name;
   }

   public bool Evaluate() => condition();

   public class Builder {
      private readonly AgentBelief belief;

      public Builder(string name) {
         belief = new AgentBelief(name);
      }

      public Builder WithCondition(Func<bool> condition) {
         belief.condition = condition;
         return this;
      }

      public Builder WithLocation(Func<Vector3> observedLocation) {
         belief.observedLocation = observedLocation;
         return this;
      }

      public AgentBelief Build() {
         return belief;
      }
   }
}
