using ChessModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheers.Models
{
    public static class ChessFileReaderWriter
    {
        public static Chess ReadChessFromFile(string pathToFile)
        {
            try
            {
                BinaryReader binaryReader = new BinaryReader(
                    File.Open(pathToFile, FileMode.Open));
                var str = new Chess(binaryReader.ReadString());
                binaryReader.Close();
                return str;
            }
            catch (Exception)
            {
                return new Chess();
            }
        }

        public static bool WriteChessToFile(string pathToFile, Chess chess)
        {
            try
            {
                File.Delete(pathToFile);
                BinaryWriter binaryWriter = new BinaryWriter(
                    File.Open(pathToFile, FileMode.OpenOrCreate));
                binaryWriter.Write(chess.fen);
                binaryWriter.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
