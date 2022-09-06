using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{    internal class Towns
    {
        private class Town
        {
            public int Id { get; set; }
            public int Capital { get; set; }
            public List<KeyValuePair<int, Town>> listRoadsToTowns;

            public Town(int id, int capital)
            {
                Id = id;
                Capital = capital;
                listRoadsToTowns = new List<KeyValuePair<int, Town>>();
            }

            public void AddRoad(Town town, int roadLenght)
            {
                listRoadsToTowns.Add(new KeyValuePair<int, Town>(roadLenght, town));
                town.listRoadsToTowns.Add(new KeyValuePair<int, Town>(roadLenght, this));
            }
        }

        private List<Town> _capitals = new List<Town>();
        public Towns(string filename)
        {
            string text = File.ReadAllText(filename);
            int[] subText = text.Split(' ').Select(i => int.Parse(i)).ToArray(); ;
            int numberOfTowns = subText[0];
            int numberOfRoads = subText[1];
            int numberOfCapitals = subText[numberOfRoads*3+2];

            AddCapitals(subText, numberOfRoads);

            int[,] roads = new int[3, numberOfRoads];
            
            for (int tempTown = 0; tempTown < numberOfRoads; tempTown++)
            {
                roads[0, tempTown] = subText[tempTown*3+2];
                roads[1, tempTown] = subText[tempTown*3+3];
                roads[2, tempTown] = subText[tempTown*3+4];
            }

            int[] firstTownsId = new int[numberOfRoads];
            int[] secondTownsId = new int[numberOfRoads];
            CopyTableIntoArray(roads, firstTownsId, 0);
            CopyTableIntoArray(roads, secondTownsId, 1);
            int[] allTownsId = new int[numberOfRoads*2];
            Array.Copy(firstTownsId, 0, allTownsId, 0, firstTownsId.Length);
            Array.Copy(secondTownsId,0, allTownsId, numberOfRoads , secondTownsId.Length);
            int capitalNumber=0;

            Dictionary<int, int> addedTownsIdToDistantseFromCapital = new Dictionary<int, int>();

            foreach (Town capital in _capitals)
            {
                addedTownsIdToDistantseFromCapital.Add(capital.Id, 0);
            }

            Dictionary<KeyValuePair<int, int>, int> inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown = new Dictionary<KeyValuePair<int, int>, int>();

            while (allTownsId.Where(i => i != -1).Count() > 0)
            {
                if (inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown.Count() == 0)
                {
                    AddTownInQueue(inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown, allTownsId, roads, _capitals[capitalNumber].Id);
                    capitalNumber++;
                }
                else
                {
                    AddTownFromQueue(addedTownsIdToDistantseFromCapital, 
                        inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown, 
                        allTownsId, 
                        roads,
                        _capitals[capitalNumber - 1].Id);
                }
            }
        }

        private void AddTownInQueue(Dictionary<KeyValuePair<int, int>, int> inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown,
            int[] allTownsId,
            int[,] roads,
            int townId)
        {
            for (int townIndex = 0; townIndex < allTownsId.Length; townIndex++)
            {
                if (allTownsId[townIndex] == townId)
                {
                    if (townIndex < allTownsId.Length / 2)
                    {
                        inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown.Add(new KeyValuePair<int, int>(roads[0, townIndex], roads[1, townIndex]),
                            roads[2, townIndex]);
                    }
                    else
                    {
                        inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown.Add(new KeyValuePair<int, int>(roads[1, townIndex - allTownsId.Length / 2], roads[0, townIndex -  allTownsId.Length / 2]),
                            roads[2, townIndex -  allTownsId.Length / 2]);
                    }
                }
            }
        }

        private void AddTownFromQueue (
            Dictionary<int, int> addedTownsIdToDistantseFromCapital, 
            Dictionary<KeyValuePair<int, int>, int> inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown,
            int[] allTownsId,
            int[,] roads,
            int capitalNumber)
        {
            int roadLenght = 999999;
            int distanceFromCapital = 999999;
            int secondTownId = 99999;
            int firstTownId =  99999;
            KeyValuePair<int,int> choosenPair = new KeyValuePair<int, int>();

            foreach (KeyValuePair<KeyValuePair<int,int>,int> townInQueue in inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown)
            {
                if (distanceFromCapital > townInQueue.Value)
                {
                    distanceFromCapital = townInQueue.Value;
                    secondTownId = townInQueue.Key.Value;
                    firstTownId = townInQueue.Key.Key;
                    choosenPair = new KeyValuePair<int, int>(firstTownId, secondTownId);
                }
            }
            
            Town firstTown = FindTown(firstTownId);

            
            for (int townIdIndex = 0; townIdIndex < allTownsId.Length / 2; townIdIndex++)
            {
                if ((allTownsId[townIdIndex] == firstTownId && allTownsId[townIdIndex + allTownsId.Length / 2] == secondTownId) ||
                    (allTownsId[townIdIndex] == secondTownId && allTownsId[townIdIndex + allTownsId.Length / 2] == firstTownId))
                {
                    allTownsId[townIdIndex] = -1;
                    allTownsId[townIdIndex + allTownsId.Length / 2] = -1;
                    roadLenght = roads[2,townIdIndex];
                    break;
                } 
            }

            if (addedTownsIdToDistantseFromCapital.Where(town => town.Key == secondTownId).Count() != 0)
            {
                firstTown.AddRoad(FindTown(secondTownId), roadLenght);
            }
            else
            {
                firstTown.AddRoad(new Town(secondTownId, capitalNumber), roadLenght);
            }

            if(addedTownsIdToDistantseFromCapital.Where(i => i.Key == secondTownId).Count() != 0)
            {
                if (addedTownsIdToDistantseFromCapital[secondTownId] > distanceFromCapital)
                {
                    addedTownsIdToDistantseFromCapital[secondTownId] = distanceFromCapital;
                }
            } else
            {
                addedTownsIdToDistantseFromCapital.Add(secondTownId, distanceFromCapital);
            }
            
            inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown.Remove(choosenPair);
            AddTownInQueue(inQueueToAddRoadWithDistanceToPaitFirstTownToSecondTown, allTownsId, roads, secondTownId);            
        }
        
        
        
        private Town FindTown(int townId)
        {
            int capitalNumber = 0;
            Town tempTown = _capitals[capitalNumber];
            List<int> alreadyVisited = new List<int>();
            alreadyVisited.Add(tempTown.Id);
            Stack<Town> moveChain = new Stack<Town>();
            moveChain.Push(tempTown);
            

            while (tempTown.Id != townId)
            {
                if (tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).Count() > 0)
                {
                    tempTown = tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).First().Value;
                    moveChain.Push(tempTown);
                    alreadyVisited.Add(tempTown.Id);
                }
                else
                {
                    while (tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).Count() == 0)
                    {
                        if (moveChain.Count == 0)
                        {
                            capitalNumber++;
                            tempTown = _capitals[capitalNumber];
                            moveChain.Push(tempTown);
                            alreadyVisited.Add(tempTown.Id);
                            break;
                        }
                        tempTown = moveChain.Pop();
                    }
                }
            }

            return tempTown;
        }

        private void AddCapitals(int[] subText, int numberOfRoads)
        {
            for (int capital = numberOfRoads*3 + 3; capital < subText.Length; capital++)
            {
                _capitals.Add(new Town(subText[capital], subText[capital]));
            }
        }

        private void CopyTableIntoArray(int[,] sourceArray, int[] outputArray, int numberOfTable)
        {
            for (int element = 0; element < sourceArray.GetLength(1); element++)
            {
                outputArray[element] = sourceArray[numberOfTable, element];
            }
        }

        public void ShowTowns()
        {
            int capitalNumber = 0;
            Town tempTown = _capitals[capitalNumber];
            List<int> alreadyVisited = new List<int>();
            alreadyVisited.Add(tempTown.Id);
            Stack<Town> moveChain = new Stack<Town>();
            moveChain.Push(tempTown);
            bool loopActive = true;
            Console.WriteLine($"This is Capital with Id {_capitals[capitalNumber].Id}");

            while (loopActive)
            {
                if (tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).Count() > 0)
                {
                    tempTown = tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).First().Value;
                    Console.WriteLine($"This is Town with Id {tempTown.Id} and capital {tempTown.Capital}");
                    moveChain.Push(tempTown);
                    alreadyVisited.Add(tempTown.Id);
                }
                else
                {
                    while (tempTown.listRoadsToTowns.Where(i => !alreadyVisited.Contains(i.Value.Id)).Count() == 0)
                    {
                        if (moveChain.Count == 0)
                        {
                            capitalNumber++;

                            if(capitalNumber == _capitals.Count())
                            {
                                loopActive = false;
                                break;
                            }

                            tempTown = _capitals[capitalNumber];

                            Console.WriteLine($"This is Capital with Id {_capitals[capitalNumber].Id}");
                            moveChain.Push(tempTown);
                            alreadyVisited.Add(tempTown.Id);
                            break;
                        }
                        tempTown = moveChain.Pop();
                    }
                }
            }
        }        
    }
}
