namespace AI.FSM {
    public interface ITriggerCheckable {
        bool isAggroed { get; set; }
        bool isWithinStrikingDistance { get; set; }
        void SetAggroStatus(bool isAggroed);
        void SetStrikingDistance(bool isWithinStrikingDistance);
    }
}