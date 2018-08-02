using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HotelController : ApiController
    {
       static List<HotelBooking> DataValues = new List<HotelBooking>()
        {
            new HotelBooking{Id=1,Name="Ambience",NumberOfAvailableRooms=30,LocationCode=101 },
            new HotelBooking{Id=2,Name="Gemini",NumberOfAvailableRooms=40,LocationCode=201 },
            new HotelBooking{Id=3,Name="Aroma",NumberOfAvailableRooms=50,LocationCode=301 },
         };

   [HttpGet] 
       public IEnumerable<HotelBooking> AllHotels()
        {
            return DataValues;
        }
     [HttpGet]
     
        public ResponseResult SearchHotel(int Id)
        {
            ResponseResult responseObject = new ResponseResult();
            Status statusObject = new Status();
            responseObject.StatusObject = statusObject;
           
            try
            {
                HotelBooking ValueObject = new HotelBooking();
                int loop;
                for ( loop = 0; loop < DataValues.Count; loop++)
                {
                    if (DataValues[loop].Id == Id)
                    {
                        ValueObject.Id = Id;
                        ValueObject.Name = DataValues[loop].Name;
                        ValueObject.NumberOfAvailableRooms = DataValues[loop].NumberOfAvailableRooms;
                        ValueObject.LocationCode = DataValues[loop].LocationCode;
                        break;
                    }
                }
                if (loop == DataValues.Count)
                    throw new Exception("File Not Found");
                responseObject.StatusObject.actionStatus = "Sucess";
                responseObject.StatusObject.Code = 200;
                responseObject.StatusObject.responseMessage = "Hotel Found";
                responseObject.hotelObject = ValueObject;
                return responseObject;
            }
            catch (Exception e)
            {
                responseObject.StatusObject.actionStatus = "Failure";
                responseObject.StatusObject.Code = 404;
                responseObject.StatusObject.responseMessage = "No Hotel Found with this ID";
                responseObject.hotelObject = null;
                return responseObject;
            }
        }
        [HttpPost]
        public ResponseResult CreateHotel(HotelBooking AddHotel)
        {
            ResponseResult responseResult=new ResponseResult();
            Status statusObject = new Status();
            responseResult.StatusObject = statusObject;
            try
            {
                DataValues.Add(AddHotel);
                responseResult.StatusObject.actionStatus = "Success";
                responseResult.StatusObject.responseMessage = "Hotel Details Added Successfully";
                responseResult.StatusObject.Code = 201;
                responseResult.hotelObject = AddHotel;
                return responseResult;
            }
            catch(Exception e)
            {
                responseResult.StatusObject.actionStatus = "Failure";
                responseResult.StatusObject.responseMessage = "Hotel Details Not Added";
                responseResult.StatusObject.Code = 500;
                responseResult.hotelObject = null;
                return responseResult;
            }
            
        }
        [HttpDelete]

        public ResponseResult DeleteHotel(int Id)
        {
            ResponseResult responseResult = new ResponseResult();
            Status statusObject = new Status();
            responseResult.StatusObject = statusObject;
            int i;
            try
            {
                HotelBooking obj = new HotelBooking();
                for ( i = 0; i < DataValues.Count; i++)
                {
                    if (DataValues[i].Id == Id)
                    {
                        obj = DataValues[i];
                        break;
                    }
                }
                if (i == DataValues.Count)
                    throw new Exception("File Not Found");
                responseResult.StatusObject.actionStatus = "Success";
                responseResult.StatusObject.Code = 200;
                responseResult.StatusObject.responseMessage = "Hotel Deleted Successful";
                responseResult.hotelObject = obj;
                DataValues.Remove(obj);
                return responseResult;
            }
            catch (Exception)
            {
                responseResult.StatusObject.actionStatus = "Failure";
                responseResult.StatusObject.Code = 404;
                responseResult.StatusObject.responseMessage = "Hotel not Found";
                responseResult.hotelObject = null;
                return responseResult;
            }
        }
        [HttpPut]
        
        public ResponseResult MakeABooking(int id,[FromBody ] int NumberOfRooms)
        {
            ResponseResult responseResult = new ResponseResult();
            Status statusObject = new Status();
            responseResult.StatusObject = statusObject;
            try
            {
                int loop;
                HotelBooking obj = new HotelBooking();
                for (loop = 0; loop < DataValues.Count; loop++)
                {
                    if (DataValues[loop].Id == id)
                    {
                        DataValues[loop].NumberOfAvailableRooms = DataValues[loop].NumberOfAvailableRooms - NumberOfRooms;
                        break;
                    }

                }
                if (loop == DataValues.Count)
                    throw new Exception("Hotel not Found");
                responseResult.StatusObject.actionStatus = "Success";
                responseResult.StatusObject.Code = 200;
                responseResult.StatusObject.responseMessage = "Booking Made Successfully";
                responseResult.hotelObject = DataValues[loop];
                return responseResult;
            }
            catch (Exception e)
            {
                responseResult.StatusObject.actionStatus = "Failure";
                responseResult.StatusObject.Code = 404;
                responseResult.StatusObject.responseMessage = "Booking Failed as Hotel not Found";
                responseResult.hotelObject = null;
                return responseResult;
            }
        }
        //[HttpGet]
        //public int Abc()
        //{
        //    return 4;
        //}

    }
}
