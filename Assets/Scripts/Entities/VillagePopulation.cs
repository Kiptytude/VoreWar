using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillagePopulation
{
    [OdinSerialize]
    internal List<RacePop> Population = new List<RacePop>();
    [OdinSerialize]
    private List<Unit> NamedRecruitables = new List<Unit>();
    [OdinSerialize]
    internal Village Village;

    public VillagePopulation(Race inRace, int maxPop, Village village)
    {
        Village = village;
        AddRacePop(inRace, maxPop);
    }

    public void AddRacePop(Race inRace, int popChange)
    {
        int x = 0;
        bool foundRace = false;

        if (Config.MultiRaceVillages == false)
            inRace = Village.Race;

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(inRace))
            {
                Population[x].ChangePop(popChange);
                foundRace = true;
            }
            x++;
        }

        if (!foundRace)
        {
            Population.Add(new RacePop(inRace, popChange));
        }


    }

    private void DeleteIfEmpty(int race)
    {
        int pop = Population[race].Population;
        if (pop <= 0)
        {
            Population.RemoveAt(race);
            if (pop != 0)
            {
                RemoveRandomPop(pop);
            }
        }
    }

    internal void ConvertToSingleRace()
    {
        int popCount = 0;
        foreach (var pop in Population)
        {
            popCount += pop.Population;
        }
        Population = new List<RacePop>()
        {
            new RacePop(Village.Race, popCount, NamedRecruitables.Count())
        };
    }

    internal void ConvertToMultiRace()
    {
        var oldNamed = NamedRecruitables.ToList();
        NamedRecruitables.Clear();
        foreach (var recruitable in oldNamed)
        {
            RemoveRacePop(Village.Race, 1);
            AddHireable(recruitable);
        }
    }

    internal string GetPopReport()
    {
        if (Population.Count > 1)
        {
            RacePop highestRace = Population[0];
            int currentHigh = 0;
            foreach (var race in Population)
            {
                if (race.Population > currentHigh)
                {
                    currentHigh = race.Population;
                    highestRace = race;
                }
            }
            return $"{GetTotalPop()} Total - {highestRace.Race} {highestRace.Population}";
        }
        else if (Population.Count == 1)
        {
            return $"{GetTotalPop()} {Population[0].Race}";
        }
        else
        {
            return "Village is empty";
        }

    }

    internal Race GetMostPopulousRace()
    {
        if (Population.Count > 1)
        {
            RacePop highestRace = Population[0];
            int currentHigh = 0;
            foreach (var race in Population)
            {
                if (race.Population > currentHigh)
                {
                    currentHigh = race.Population;
                    highestRace = race;
                }
            }
            return highestRace.Race;
        }
        else if (Population.Count == 1)
        {
            return Population[0].Race;
        }
        else
            return Village.Race;
    }


    public void IncrementRandom()
    {
        if (Population.Count == 0)
        {
            AddRacePop(Village.Race, 1);
        }
        else
        {
            Population[RandomRacePlaceByWeight()].ChangePop(1);
        }
        
    }

    public void DecrementRandom()
    {
        int raceChosen = RandomRacePlaceByNonHireableWeight();
        if (raceChosen == -1)
        {
            RemoveLowestHireable();
        }
        else
        {
            Population[raceChosen].ChangePop(-1);
            DeleteIfEmpty(raceChosen);
        }


    }

    public void AddRandomPop(int popChange)
    {
        for (int x = 0; x < popChange; x++)
        {
            IncrementRandom();
        }
    }

    public void RemoveRandomPop(int popChange)
    {
        for (int x = 0; x < popChange; x++)
        {
            DecrementRandom();
        }
    }

    public int RemoveRacePop(Race race, int popChange)
    {

        if (Config.MultiRaceVillages == false)
            race = Village.Race;
        int x = 0;
        bool foundRace = false;

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(race))
            {
                foundRace = true;
                if (popChange < Population[x].Population)
                {
                    Population[x].ChangePop(-popChange);

                }
                else if (popChange == Population[x].Population)
                {
                    Population.Remove(Population[x]);
                }
                else
                {
                    popChange -= Population[x].Population;
                    Population.Remove(Population[x]);
                    return popChange;
                }


            }
            x++;
        }

        if (!foundRace)
        {
            Debug.Log("No Race Pop to remove");
        }
        return 0;
    }

    private int RandomRacePlaceByWeight()
    {
        int totalPop = 0;
        for (int x = 0; x < Population.Count; x++)
        {
            totalPop += Population[x].Population;
        }
        int randomPop = State.Rand.Next(totalPop);
        for (int x = 0; x < Population.Count; x++)
        {

            randomPop -= Population[x].Population;
            if (randomPop < 0)
            {
                return x;
            }
        }
        return 0;
    }
    public Race RandomRaceByWeight()
    {
        int totalPop = 0;
        for (int x = 0; x < Population.Count; x++)
        {
            totalPop += Population[x].Population;
        }
        int randomPop = State.Rand.Next(totalPop);
        for (int x = 0; x < Population.Count; x++)
        {

            randomPop -= Population[x].Population;
            if (randomPop < 0)
            {
                return Population[x].Race;
            }
        }
        return Population[0].Race;
    }

    private int RandomRacePlaceByNonHireableWeight()
    {
        int totalPop = 0;
        for (int x = 0; x < Population.Count; x++)
        {
            totalPop += Population[x].Population;
        }
        int randomPop = State.Rand.Next(totalPop);
        for (int x = 0; x < Population.Count; x++)
        {

            randomPop -= Population[x].Population;
            if (randomPop < 0)
            {
                return x;
            }
        }
        return -1;
    }

    //public Race RandomRaceByNonHireableWeight()
    //{
    //    int totalPop = 0;
    //    for (int x = 0; x < population.Count; x++)
    //    {
    //        totalPop += population[x].Population - population[x].Hireables;
    //    }
    //    if (totalPop > 0)
    //    {
    //        int randomPop = State.Rand.Next(totalPop);
    //        for (int x = 0; x < population.Count; x++)
    //        {

    //            randomPop -= population[x].Population - population[x].Hireables;
    //            if (randomPop < 0)
    //            {
    //                return population[x].Race;
    //            }
    //        }
    //        return population[0].Race;
    //    }
    //    else if (totalPop < 0)
    //    {
    //        Debug.Log("More hireables than population");
    //        return (Race)9999;
    //    }
    //    else
    //    {
    //        return (Race)9998;
    //    }

    //}

    internal void RemoveHireable(Unit unit)
    {
        int x = 0;
        bool foundRace = false;

        Race race = unit.Race;

        if (Config.MultiRaceVillages == false)
            race = Village.Race;

        NamedRecruitables.Remove(unit);

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(race))
            {
                foundRace = true;
                if (Population[x].Population <= 0)
                {
                    Debug.Log("Hireable not in pop system");
                }
                else
                {
                    Population[x].Hireables--;
                    RemoveRacePop(Population[x].Race, 1);
                }

            }
            x++;
        }

        if (!foundRace)
        {
            Debug.Log("No unit of race for hireable");
        }
    }

    internal void AddHireable(Unit unit)
    {
        int x = 0;
        bool foundRace = false;

        Race race = unit.Race;

        if (Config.MultiRaceVillages == false)
            race = Village.Race;

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(race))
            {
                Population[x].Hireables++;
                Population[x].ChangePop(1);
                NamedRecruitables.Add(unit);
                foundRace = true;

            }
            x++;
        }

        if (!foundRace)
        {
            Population.Add(new RacePop(race, 1, 1));
            NamedRecruitables.Add(unit);
        }
    }

    internal void AddHireableFromCurrentPop(Unit unit)
    {
        int x = 0;
        bool foundRace = false;

        Race race = unit.Race;

        if (Config.MultiRaceVillages == false)
            race = Village.Race;

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(race))
            {
                Population[x].Hireables++;
                NamedRecruitables.Add(unit);
                foundRace = true;

            }
            x++;
        }

        if (!foundRace)
        {
            Population.Add(new RacePop(race, 1, 1));
            NamedRecruitables.Add(unit);
        }
    }

    public int GetTotalPop()
    {
        int totalPop = 0;
        for (int x = 0; x < Population.Count; x++)
        {
            totalPop += Population[x].Population;
        }
        return totalPop;
    }
    public void CheckMaxpop(int Maxpop)
    {
        if (GetTotalPop() > Maxpop)
        {
            RemoveRandomPop(Maxpop - GetTotalPop());
        }

    }


    public void RemoveLowestHireable()
    {
        var rec = NamedRecruitables.OrderBy(s => s.Experience).FirstOrDefault();
        if (rec != null)
        {
            RemoveRacePop(rec.Race, 1);
            NamedRecruitables.Remove(NamedRecruitables.OrderBy(s => s.Experience).First());
        }
            
        
       
    }

    /// <summary>
    /// Does not remove the population, is used to correct population imbalances.
    /// </summary>
    /// <param name="race"></param>
    public void RemoveLowestHireable(Race race)
    {
        if (Config.MultiRaceVillages == false)
            race = Village.Race;
        var rec = NamedRecruitables.Where(s => s.Race == race).OrderBy(s => s.Experience).FirstOrDefault();
        if (rec != null)
        {
            NamedRecruitables.Remove(rec);
        }
           
        
       
    }

    public void CleanHirables()
    {
        if (Population.Count == 0)
        {
            NamedRecruitables.Clear();
            return;
        }
        int endCount = NamedRecruitables.Count;
        for (int x = 0; x < endCount; x++)
        {
            if (NamedRecruitables[x].IsDead)
            {
                var race = NamedRecruitables[x].Race;
                NamedRecruitables.Remove(NamedRecruitables[x]);
                RemoveRacePop(race, 1);                
                endCount--;
                x--;
            }
        }
        for (int x = 0; x < Population.Count; x++)
        {
            if (Population.Count <= x)
                break;
            Population[x].Hireables = NamedRecruitables.Where(s => s.Race == Population[x].Race).Count();
            while (NamedRecruitables.Count > 0 && Population[x].Population < Population[x].Hireables)
            {                
                RemoveLowestHireable(Population[x].Race);
                var count = NamedRecruitables.Where(s => s.Race == Population[x].Race).Count();
                if (count < Population[x].Hireables)
                    Population[x].Hireables = count;
               
            }
        }

    }

    internal void RemoveAllPop()
    {
        Population.Clear();
        NamedRecruitables.Clear();
    }

    public List<Unit> DirectLinkToNamed()
    {
        return NamedRecruitables;
    }

    public List<Unit> GetRecruitables()
    {
        return NamedRecruitables;
    }

    internal int GetRacePop(Race race)
    {
        int x = 0;
        bool foundRace = false;

        while (x < Population.Count && !foundRace)
        {
            if (Population[x].Race.Equals(race))
            {
                foundRace = true;
                return Population[x].Population;
            }
            x++;
        }

        //if (!foundRace)
        //{
        //    Debug.Log("No race Found");
        //}
        return -1;
    }
}