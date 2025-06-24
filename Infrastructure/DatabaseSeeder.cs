using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly WarehouseContext _dbContext;
        public DatabaseSeeder(WarehouseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            var dbPath = Path.Combine(AppContext.BaseDirectory, "bazadanych.db");
            var dbExists = File.Exists(dbPath);

            if (!dbExists)
            {
                _dbContext.Database.Migrate();
                SeedData(); 
            }
            else
            {

                if (_dbContext.Database.CanConnect())
                {
                    var pending = _dbContext.Database.GetPendingMigrations();
                    if (pending.Any())
                    {
                        _dbContext.Database.Migrate();
                    }


                    if (!_dbContext.DokumentyPrzyjecia.Any()) 
                    {
                        SeedData();
                    }
                }
            }
        }


        private void SeedData()
        {
            if (!_dbContext.Kontrahenci.Any())
            {
                _dbContext.Kontrahenci.AddRange(new List<Kontrahent>
                    {
                        new Kontrahent { Symbol = "qbt", Nazwa = "Jakub Qubity Rosp³och" },
                        new Kontrahent { Symbol = "wsl", Nazwa = "Adam Ma³ysz" },
                    });

                _dbContext.SaveChanges();
            }
            if (!_dbContext.DokumentyPrzyjecia.Any())
            {
                _dbContext.DokumentyPrzyjecia.AddRange(new List<DokumentPrzyjecia>
                    {
                        new DokumentPrzyjecia{ 
                            Data = DateTime.Now, 
                            Symbol = "PZ/1/2024", 
                            Kontrahent = new Kontrahent { Symbol="adc", Nazwa="AdCode" }, 

                            Pozycje = new List<PozycjaDokumentu> {
                                new PozycjaDokumentu{
                                    Towar = new Towar
                                    {
                                        NazwaTowaru = "MSI VECTOR 16HX A13VHG",
                                        JednostkaMiary ="Szt.",
                                    },
                                    Ilosc=1},
                                new PozycjaDokumentu{
                                    Towar= new Towar
                                    {                                
                                        NazwaTowaru = "HP VICTUS 16",
                                        JednostkaMiary ="Szt.", 
                                    },
                                    Ilosc=1},

                            } 
                        },
                    });

                _dbContext.SaveChanges();
            }
        }


    }
}