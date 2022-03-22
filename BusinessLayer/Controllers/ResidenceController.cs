using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Repositorys;
using System.Linq;

namespace BusinessLayer.Controllers
{
    public class ResidenceController
    {
        private DataAccesLayer.UnitOfWork unitOfWork;

        public ResidenceController(DataAccesLayer.MyContext context)
        {
            unitOfWork = new DataAccesLayer.UnitOfWork(context);
        }

        public DataAccesLayer.Models.Residence GetByID(int ID)
        {
            return unitOfWork.ResidenceRepository.FirstOrDefault(r => r.ResidenceID == ID);
        }

        public List<DataAccesLayer.Models.Residence> GetAll()
        {
            return unitOfWork.ResidenceRepository.ReturnAll().ToList();
        }
        //Get by city
        public List<DataAccesLayer.Models.Residence> GetByCity()
        {
            //List<DataAccesLayer.Models.Residence> residences = unitOfWork.ResidenceRepository.Find(residence => residence.City == city).ToList();
            List<DataAccesLayer.Models.Residence> residences = unitOfWork.ResidenceRepository.SortByCity().ToList();

            return residences;
        }

        //Sort by price
        public List<DataAccesLayer.Models.Residence> GetByPrice()
        {
            List<DataAccesLayer.Models.Residence> residences = unitOfWork.ResidenceRepository.SortByPrice().ToList();
            return residences;
        }

        //sort min max price

        //Sort by rating
        public List<DataAccesLayer.Models.Residence> GetByRating()
        {
            List<DataAccesLayer.Models.Residence> residences = unitOfWork.ResidenceRepository.SortByRating().ToList();
            return residences;
        }

        //Get by facilies
        public List<DataAccesLayer.Models.Residence> GetByFacilites(bool balcony, bool kitchen, bool wifi, bool pool)
        {
            List<DataAccesLayer.Models.Residence> residences = unitOfWork.ResidenceRepository.ReturnAll().ToList();

            if (balcony)
            {
                residences = residences.Where(r => r.Balcony).ToList();
            }
            if (kitchen)
            {
                residences = residences.Where(r => r.Kitchen).ToList();
            }
            if (wifi)
            {
                residences = residences.Where(r => r.Wifi).ToList();
            }
            if (pool)
            {
                residences = residences.Where(r => r.Pool).ToList();
            }

            return residences;
        }

        public List<DataAccesLayer.Models.Residence> GetByOwner(DataAccesLayer.Models.User user)
        {
            return unitOfWork.ResidenceRepository.Find(r => r.Owner == user).ToList();
        }

        //add new residence/ad
        public void AddNewResidence(DataAccesLayer.Models.User user, string street, string city, string state, int postalCode, string country, bool balcony, bool kitchen, bool wifi, bool pool, int maxGuests, double pricePerDay, string description, string imageUrl)
        {
            DataAccesLayer.Models.Residence residence = new DataAccesLayer.Models.Residence()
            {
                Owner = user,
                Street = street,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country,
                Balcony = balcony,
                Kitchen = kitchen,
                Wifi = wifi,
                Pool = pool,
                MaxGuests = maxGuests,
                PricePerDay = pricePerDay,
                Description = description,
                ImageUrl = imageUrl
            };

            unitOfWork.ResidenceRepository.Add(residence);
            unitOfWork.SaveChanges();
        }

        //remove residence/ad, return true if correct owner else return false
        public bool RemoveResidence(int residenceId, DataAccesLayer.Models.User owner)  //kolla så det är rätt ägare
        {
            DataAccesLayer.Models.Residence residence = unitOfWork.ResidenceRepository.FirstOrDefault(r => r.ResidenceID == residenceId);

            if (residence == null)
            {
                return false;
            }

            else if (residence.Owner == owner)
            {
                unitOfWork.ResidenceRepository.Remove(residence);
                unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Edit residence/ad
        public void EditResidence(DataAccesLayer.Models.Residence residenceToEdit, DataAccesLayer.Models.Residence newResidence)
        {
            residenceToEdit.Balcony = newResidence.Balcony;
            residenceToEdit.City = newResidence.City;
            residenceToEdit.Country = newResidence.Country;
            residenceToEdit.Description = newResidence.Description;
            residenceToEdit.Kitchen = newResidence.Kitchen;
            residenceToEdit.MaxGuests = newResidence.MaxGuests;
            residenceToEdit.Pool = newResidence.Pool;
            residenceToEdit.PostalCode = newResidence.PostalCode;
            residenceToEdit.PricePerDay = newResidence.PricePerDay;
            residenceToEdit.State = newResidence.State;
            residenceToEdit.Street = newResidence.Street;
            residenceToEdit.Wifi = newResidence.Wifi;
            unitOfWork.SaveChanges();
        }
    }
}
