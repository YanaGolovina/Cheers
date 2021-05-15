using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheers.Models
{
    public class StatsFileReaderWriter
    {
        public static bool TryReadStatsFromFile(string pathToFile, out Tuple<string, int>[] stats)
        {
            try
            {
                BinaryReader binaryReader = new BinaryReader(
                    File.Open(pathToFile, FileMode.Open));
                {
                    List<Tuple<string, int>> output = new List<Tuple<string, int>>();

                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        string name = binaryReader.ReadString();
                        int wins = binaryReader.ReadInt32();

                        output.Add(new Tuple<string, int>(name, wins));
                    }

                    stats = output.ToArray();

                    binaryReader.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                stats = new List<Tuple<string, int>>().ToArray();
                return false;
            }
        }

        public static bool AddStatsToFile(string pathToFile, string winnerName)
        {
            try
            {
                Tuple<string, int>[] stats;
                List<Tuple<string, int>> newStats = new List<Tuple<string, int>>();
                if (TryReadStatsFromFile(pathToFile, out stats))
                {
                    bool nameFound = false;
                    foreach (Tuple<string, int> tuple in stats)
                    {
                        string name = tuple.Item1;
                        if (name.Equals(winnerName))
                        {
                            Tuple<string, int> newStat = new Tuple<string, int>(
                                name, tuple.Item2 + 1);
                            nameFound = true;
                        }
                        else
                        {
                            newStats.Add(tuple);
                        }
                    }
                    if (!nameFound)
                    {
                        newStats.Add(new Tuple<string, int>(winnerName, 1));
                    }
                }
                else
                {
                    try
                    {
                        File.Delete(pathToFile);
                    }
                    catch (Exception) { }
                }

                BinaryWriter binaryWriter = new BinaryWriter(
                        File.Open(pathToFile, FileMode.OpenOrCreate));

                foreach (Tuple<string, int> stat in newStats)
                {
                    binaryWriter.Write(stat.Item1);
                    binaryWriter.Write(stat.Item2);
                }

                binaryWriter.Close();
                return true;
            }

            catch (Exception e) 
            {
                return false;
            }
        }
    }
}
