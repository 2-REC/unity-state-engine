/*
  List of states IDs (names & index).
*/
using System.Collections.Generic;

public class StateIds {
    public static int NONE { get; private set; } = 0;

    private static List<string> states = new List<string>();


    public static void Reset() {
        states.Clear();
        NONE = 0;
    }

    public static void Add(string stateId) {
        states.Add(stateId);
        ++NONE; //or: NONE = states.Count;
    }

    public static int Index(string stateId) {
        return states.IndexOf(stateId);
    }

    public static string Name(int index) {
        return states[index];
    }

}
