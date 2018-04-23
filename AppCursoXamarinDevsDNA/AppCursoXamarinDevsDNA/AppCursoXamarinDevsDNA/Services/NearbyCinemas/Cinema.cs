using System;
using System.Collections.Generic;
using System.Text;

namespace AppCursoXamarinDevsDNA.Services.NearbyCinemas
{
    public class Cinema
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }


        public static List<Cinema> GetFakeCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>();

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Renoir Princesa",
                    Adress = "Princesa, 3",
                    Phone = "915 41 41 00"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Golem",
                    Adress = "Calle de Martín de los Heros, 14",
                    Phone = "915 59 38 36"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Renoir Plaza de España",
                    Adress = "Calle de Martín de los Heros, 12",
                    Phone = "915 42 27 02"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cine Capitol",
                    Adress = "Calle Gran Vía, 41",
                    Phone = "915 22 22 29"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Callao",
                    Adress = "Plaza Callao, 3",
                    Phone = "915 22 58 01"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cine Acteón",
                    Adress = "Calle Montera, 31",
                    Phone = "915 22 22 81"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Verdi",
                    Adress = "Calle Bravo Murillo, 28",
                    Phone = "914 47 39 30"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cine Ideal",
                    Adress = "Calle del Dr Cortezo, 6",
                    Phone = "902 22 09 22"
                }
            );

            return cinemas;
        }
    }
}
