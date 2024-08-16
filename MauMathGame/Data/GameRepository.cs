using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauMathGame.Models;

namespace MauMathGame.Data
{
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void Init()
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Game>();

        }

        public List<Game> GetAllGames()
        {
            Init();
            return conn.Table<Game>().ToList();

        }

        public void Add(Game game)
        {

            Init();
            conn = new SQLiteConnection(_dbPath);
            conn.Insert(game);
        }

        public void Delete(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Execute("DELETE FROM game WHERE Id= ?", id);
            
        }


    }
}
