using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarWarsUI.Models;

namespace StarWarsUI
{
    partial class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage personResponse = httpClient.GetAsync("https://swapi.co/api/people/1").Result;

            if (personResponse.IsSuccessStatusCode)
            {
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                var person = personResponse.Content.ReadAsAsync<Person>().Result;
                Console.WriteLine(person.Name);

                foreach(string vehicleUrl in person.Vehicles)
                {
                    Console.WriteLine(vehicleUrl);
                    var vehicleResponse = httpClient.GetAsync(vehicleUrl).Result;
                    var vehicle = vehicleResponse.Content.ReadAsAsync<Vehicle>().Result;
                    Console.WriteLine(vehicle.Name);
                }
            }

            SWAPIService service = new SWAPIService();
            Person personTwo = service.GetPersonAsync("https://swapi.co/api/people/11").Result;
            if (personTwo != null)
            {
                Console.WriteLine(personTwo.Name);
                foreach (string vehicleUrl in personTwo.Vehicles)
                {
                    Vehicle vehicle = service.GetVehicleAsync(vehicleUrl).Result;
                    Vehicle genericVehicle = service.GetAsync<Vehicle>(vehicleUrl).Result;

                    Console.WriteLine(vehicle.Name);
                    Console.WriteLine(genericVehicle.Name);
                }
            }

            var skywalkers = service.GetSearchAsync<Person>("people", "skywalker").Result;
            foreach (var person in skywalkers.Results)
            {
                Console.WriteLine(person.Name);
            }
            Console.ReadLine();
        }
    }
}
