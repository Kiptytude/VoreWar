using System.Collections.Generic;

class WeightedList<T1>
{
    private List<T1> action = new List<T1>();
    private List<int> weight = new List<int>();

    public void Add(T1 action, int weight)
    {
        if (weight <= 0)
            return;
        this.action.Add(action);
        this.weight.Add(weight);
    }

    public T1 GetResult()
    {
        int total = 0;
        int count = weight.Count;
        if (count < 1)
            return default(T1);
        if (count == 1)
            return action[0];
        foreach (int weight in weight)
        {
            total += weight;
        }
        int roll = State.Rand.Next(total);
        int accumulator = 0;
        for (int x = 0; x < count; x++)
        {
            accumulator += weight[x];
            if (roll < accumulator)
                return action[x];
        }
        return default(T1);
    }

    public T1 GetAndRemoveResult()
    {
        T1 act;
        int total = 0;
        int count = weight.Count;
        if (count < 1)
            return default(T1);
        if (count == 1)
        {
            act = action[0];
            action.Clear();
            weight.Clear();
            return act;
        }

        foreach (int weight in weight)
        {
            total += weight;
        }
        int roll = State.Rand.Next(total);
        int accumulator = 0;
        for (int x = 0; x < count; x++)
        {
            accumulator += weight[x];
            if (roll < accumulator)
            {
                act = action[x];
                action.RemoveAt(x);
                weight.RemoveAt(x);
                return act;
            }
        }
        return default(T1);
    }

}
